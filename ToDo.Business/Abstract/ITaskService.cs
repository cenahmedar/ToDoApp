using System.Threading.Tasks;
using ToDo.Business.Result;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Abstract
{
    public interface ITaskService
    {
        ObjectResult<MTask> Add(MTask model);
        ObjectResult<MTask> Update(MTask model);
        Task<ObjectResult<MTask>> Get(string id);
        Task<ListResult<MTask>> GetMyList();
        MResult Delete(string id);

    }
}
