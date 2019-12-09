using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Users;
using UpdateNotator.Infrastructure.Helpers.Security;

namespace UpdateNotator.Application.ApplicationServices.User
{
    public class UserService : BaseService, IUserService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private ILogger<IUserService> logger;

        public UserService(IUnitOfWork db, 
                           IMapper mapper,
                           ILogger<IUserService> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ServiceResult<UserDto>> CreateUser(UserDto userDto)
        {
            try
            {
                logger.LogTrace("Start", userDto);
                var encodedPassword = SecurityHelper.CreatePasswordHash(userDto.Password);

                var user = Domain.Core.Users.User.Create(email: userDto.Email,
                                                      username: userDto.UserName,
                                                      password: encodedPassword.Password,
                                                          salt: encodedPassword.Salt);

                await db.Users.Add(user);
                await db.Save();

                var resultUser = await db.Users.GetById(user.Id);
                return Result(mapper.Map<UserDto>(resultUser));
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<UserDto>(ex);
            }
            finally
            {
                logger.LogTrace("End");
            }
        }

        public async Task<ServiceResult<UserDto>> GetUser(string username, string password)
        {
            try
            {
                logger.LogTrace("Start");
                var user = await db.Users.FirstOrDefault(new UserByLoginSpecification(username));
                user = user ?? throw new UserNotFoundException();

                var passwordInfo = SecurityHelper.CreatePasswordHash(password, user.Salt);

                if (user.Password.Equals(passwordInfo.Password))
                    return Result(mapper.Map<UserDto>(user));

                throw new UserNotFoundException();
            }
            catch (UserNotFoundException ex)
            {
                logger.LogInformation(ex, ex.Message);
                return Result<UserDto>(ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<UserDto>(ex);
            }
            finally
            {
                logger.LogTrace("End");
            }
        }

        public async Task<ServiceResult<UserDto>> GetUser(string username)
        {
            try
            {
                logger.LogTrace("Start");
                var user = await db.Users.FirstOrDefault(new UserByLoginSpecification(username));

                if (user != null)
                    return Result(mapper.Map<UserDto>(user));

                throw new UserNotFoundException();
            }
            catch (UserNotFoundException ex)
            {
                logger.LogInformation(ex, ex.Message);
                return Result<UserDto>(ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<UserDto>(ex);
            }
            finally
            {
                logger.LogTrace("End");
            }
        }
    }
}
