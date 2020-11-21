using ToDo.Entities.Concrete;

namespace ToDo.Api.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Member Member { get; set; }
    }
}
