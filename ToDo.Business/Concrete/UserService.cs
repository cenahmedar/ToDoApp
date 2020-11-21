using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using ToDo.Business.Abstract;
using ToDo.Business.Models;

namespace ToDo.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public UserModel User()
        {
            ClaimsPrincipal claims = accessor?.HttpContext?.User as ClaimsPrincipal;
            return JsonConvert.DeserializeObject<UserModel>(claims.FindFirst("user").Value);

        }
    }
}
