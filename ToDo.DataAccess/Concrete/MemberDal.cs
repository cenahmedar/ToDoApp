using Couchbase.Extensions.DependencyInjection;
using ToDo.Core.DataAccess;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrete;

namespace ToDo.DataAccess.Concrete
{
    public class MemberDal : CouchbaseRepository<Member>, IMemberDal
    {
        public MemberDal(IBucketProvider bucketProvider) : base(bucketProvider)
        {
        }
    }
}
