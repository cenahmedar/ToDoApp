using Couchbase.Extensions.DependencyInjection;
using ToDo.Core.DataAccess;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete
{
    public class TaskDal : CouchbaseRepository<MTask>, ITaskDal
    {
        public TaskDal(IBucketProvider bucketProvider) : base(bucketProvider)
        {
        }
    }
}
