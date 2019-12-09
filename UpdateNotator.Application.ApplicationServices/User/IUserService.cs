using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UpdateNotator.Application.ApplicationServices.User
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> CreateUser(UserDto user);

        Task<ServiceResult<UserDto>> GetUser(string username, string password);

        Task<ServiceResult<UserDto>> GetUser(string username);
    }
}
