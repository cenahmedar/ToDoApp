using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Api.Models;
using ToDo.Business.Abstract;
using ToDo.Business.Models;
using ToDo.Business.Result;
using ToDo.Entities.Concrete;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IMemberService _memberService;
        private readonly ApplicationSettingsModel _appsettings;

        public AuthController(IMemberService memberService, IOptions<ApplicationSettingsModel> appSettings)
        {
            _memberService = memberService;
            _appsettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ObjectResult<AuthResponse>>> Login([FromBody]LoginModel model)
        {
            var user = await _memberService.Login(model);
            if (user != null)
            {
                var userModel = new UserModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username
                };


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                     new Claim("user", JsonConvert.SerializeObject(userModel))
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        _appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                var res = new AuthResponse()
                {
                    Token = token,
                    Member = user
                };
                return new ObjectResult<AuthResponse>(Success.success, res);
            }
            else
                return new ObjectResult<AuthResponse>(Errors.Custom("Wrong Info."));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ObjectResult<Member>>> Post([FromBody]Member model)
        {
            return await _memberService.Add(model);
        }

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ObjectResult<Member>>> GetProfile()
        {
            return await _memberService.GetProfile();
        }

    }

}