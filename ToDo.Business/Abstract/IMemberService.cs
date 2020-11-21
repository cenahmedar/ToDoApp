using System.Threading.Tasks;
using ToDo.Business.Models;
using ToDo.Business.Result;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Abstract
{
    public interface IMemberService
    {
        Task<Member> Login(LoginModel model);
        Task<ObjectResult<Member>> Add(Member model);
        Task<ObjectResult<Member>> GetProfile();

    }
}
