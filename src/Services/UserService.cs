

using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SalesApi.src.Data.Dtos;
using SalesApi.src.Services.Interface;

namespace SalesApi.src.Services;

public class UserService: IUserService{

    private IMapper _mapper;
    private UserManager<User> _userManager;

    private SignInManager<User> _signInManager;

    private TokenService _tokenService;

     public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService){
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<ResponseCreateUserDto> RegisterUser(CreateUserDto dto)
    {
        var response = new ResponseCreateUserDto();
        User user = _mapper.Map<User>(dto);

        var hasUser = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

        if(hasUser != null){
            response.StatusCode = 406;
            response.Message = "Nome de usuário já cadastrado!";
            return response;
        } 

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        if(!result.Succeeded){
            response.StatusCode = 406;
            response.Message = "Ocorreum um erro na criação do usuário!";
            return response;
        }

        return response;
    }

    public async Task<ReadUserDto> Authenticate(UserLoginDto dto) {
        var responseDto = new ReadUserDto();

        var resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!resultado.Succeeded){
            responseDto.errormessage = "Login ou senha incorretos, verifique e tente novamente!";
            return responseDto;
        }

        var userReg = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

        if (userReg == null){
            responseDto.errormessage = "Erro ao autenticar o usuário!";
            return responseDto;
        }

        var token = _tokenService.GenerateToken(userReg);
        responseDto.username = userReg.UserName!;
        responseDto.token = token;

        return responseDto;    
    }

    public async Task Logout(UserLoginDto dto) {
       User user = _mapper.Map<User>(dto);
       await _signInManager.SignOutAsync();
    }

}