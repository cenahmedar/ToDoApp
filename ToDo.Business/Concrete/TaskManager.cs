using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Result;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _dal;
        private readonly IUserService _userService;

        public TaskManager(ITaskDal dal, IUserService userService)
        {
            _dal = dal;
            _userService = userService;
        }
        public ObjectResult<MTask> Add(MTask model)
        {
            model.CreateDate = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            model.UpdateDate = DateTime.Now;
            model.MemberId = _userService.User().Id;
            return new ObjectResult<MTask>(Success.success, _dal.Create(model.Id, model));
        }

        public ObjectResult<MTask> Update(MTask model)
        {
            model.UpdateDate = DateTime.Now;
            return new ObjectResult<MTask>(Success.success, _dal.Update(model.Id, model));
        }

        public MResult Delete(string id)
        {
            _dal.Delete(id);
            return new MResult(Success.success);
        }

        public async Task<ObjectResult<MTask>> Get(string id)
        {
            return new ObjectResult<MTask>(Success.success, await _dal.Get(x => x.Id == id));
        }

        public async Task<ListResult<MTask>> GetMyList()
        {
            IList<MTask> result = await _dal.GetList(x => x.MemberId == _userService.User().Id);
            return new ListResult<MTask>(Success.success, result as List<MTask>);
        }

    }
}
