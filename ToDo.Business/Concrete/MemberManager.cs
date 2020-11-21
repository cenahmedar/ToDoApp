using System;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Helpers;
using ToDo.Business.Models;
using ToDo.Business.Result;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class MemberManager : IMemberService
    {
        private readonly IMemberDal _dal;
        private readonly IUserService _userService;

        public MemberManager(IMemberDal dal, IUserService userService)
        {
            _dal = dal;
            _userService = userService;
        }
        public async Task<ObjectResult<Member>> Add(Member model)
        {
            if (await _dal.Any(x => x.Username == model.Username))
            {
                return new ObjectResult<Member>(Errors.Custom("Username Already exists"));
            }
            model.CreateDate = DateTime.Now;
            model.Password = Functions.MD5(model.Password);
            model.Id = Guid.NewGuid().ToString();
            return new ObjectResult<Member>(Success.success, _dal.Create(model.Id, model));
        }

        public async Task<ObjectResult<Member>> GetProfile()
        {
            return new ObjectResult<Member>(Success.success, await _dal.Get(x => x.Id == _userService.User().Id));
        }

        public async Task<Member> Login(LoginModel model)
        {
            return await _dal.Get(x => x.Username == model.Username && x.Password == Functions.MD5(model.Password));
        }
    }
}
