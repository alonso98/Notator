using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpdateNotator.Application.ApplicationServices.User;

namespace UpdateNotator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromForm]UserDto user)
        {
            var result = await userService.CreateUser(user);
            if (result.IsSuccessed) return CreatedAtAction(nameof(CreateUser), result.Data);

            throw result.Exception;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUser()
        //{

        //}
    }
}