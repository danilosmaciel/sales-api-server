
using Microsoft.AspNetCore.Mvc;
using SalesApi.src.Data.Dtos;
using SalesApi.src.Services.Interface;

namespace SalesApi.src.Controllers;

[ApiController]
[Route("api/user")]
public class UserController: ControllerBase{


    private IUserService _service;

    public UserController(IUserService service){
        _service = service;
    }


    [HttpPost("create")]
    public async Task<IActionResult> UserAdd(CreateUserDto dto){
        await _service.RegisterUser(dto);
        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Authenticate(UserLoginDto dto){
        var response = await _service.Authenticate(dto);
        return Ok(response);
    }

}