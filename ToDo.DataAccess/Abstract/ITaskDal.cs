using ToDo.Core.DataAccess;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Abstract
{
    public interface ITaskDal : IRepository<MTask>
    {
    }
}
