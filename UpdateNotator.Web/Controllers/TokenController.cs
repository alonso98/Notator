using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UpdateNotator.Application.ApplicationServices.User;
using UpdateNotator.Domain.Core.Users;
using UpdateNotator.Web.Auth;
using UpdateNotator.Web.Models;

namespace UpdateNotator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IUserService userService;
        private IJwtSigningEncodingKey signingEncodingKey;

        public TokenController(IUserService userService, IJwtSigningEncodingKey signingEncodingKey)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.signingEncodingKey = signingEncodingKey ?? throw new ArgumentNullException(nameof(signingEncodingKey));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]UserCredentialsForToken userCredentials)
        {
            // Проверяем пользователя
            var result = await userService.GetUser(userCredentials.UserName, userCredentials.Password);
            if (!result.IsSuccessed)
                throw result.Exception;

            var user = result.Data;

            // Создаем утверждения для токена.
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // 3. Генерируем JWT.
            int timeOfLiveToken = 30;
            var expireDate = DateTime.Now.AddMinutes(timeOfLiveToken);
            var token = new JwtSecurityToken(
                issuer: "Unotator",
                audience: "UnotatorClient",
                claims: claims,
                expires: expireDate,
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = jwtToken, expireDate = expireDate });
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public string Get()
        {
            return User.Identity.Name;
        }
    }
}