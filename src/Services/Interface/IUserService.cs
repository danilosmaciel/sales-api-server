
using SalesApi.src.Data.Dtos;

namespace SalesApi.src.Services.Interface;

public interface IUserService{
    public Task<ReadUserDto> Authenticate(UserLoginDto dto);
    public Task<ResponseCreateUserDto> RegisterUser(CreateUserDto dto);

}