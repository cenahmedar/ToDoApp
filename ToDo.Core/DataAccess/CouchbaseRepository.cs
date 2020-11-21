using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo.Core.Entities;

namespace ToDo.Core.DataAccess
{
    public class CouchbaseRepository<T> : IRepository<T> where T : EntityBase<T>, new()
    {
        private readonly IBucket _bucket;
        private readonly IBucketContext _context;

        public CouchbaseRepository(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("ToDoBucket");
            _context = new BucketContext(_bucket);
        }

        private string CreateKey(string id)
        {
            return string.Format("{0}::{1}", typeof(T).Name.ToLower(), id);
        }

        public T Get(string id)
        {
            var key = CreateKey(id);
            IOperationResult<T> result = _bucket.Get<T>(key);
            if (!result.Success)
                throw result.Exception;
            return result.Value;
        }
        public T Create(string id, T item)
        {
            item.CreateDate = DateTime.Now;
            item.UpdateDate = DateTime.Now;
            //item.Id = AppendKeyIncrement(id);

            var key = CreateKey(item.Id);

            IOperationResult<T> result = _bucket.Insert(key, item);
            if (!result.Success)
                throw result.Exception;

            return item;
        }

        //private string AppendKeyIncrement(string key)
        //{
        //    const string counterKey = "customer::counter";
        //    IOperationResult<ulong> result = _bucket.Increment(counterKey, 1, 1);

        //    return string.Format(key + "-{0}", result.Value);
        //}

        public T Update(string id, T item)
        {
            if (item.Id == null)
            {
                item.CreateDate = DateTime.Now;
                item.Id = id;
            }
            item.UpdateDate = DateTime.Now;

            var key = CreateKey(id);

            IOperationResult<T> result = _bucket.Replace(key, item);

            if (!result.Success)
            {
                throw result.Exception;
            }

            return item;
        }

        public void Delete(string id)
        {
            var key = CreateKey(id);

            IOperationResult result = _bucket.Remove(key);
            if (!result.Success)
            {
                throw result.Exception;
            }

        }

        public async Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()) : _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()).Where(filter);
            return await Task.FromResult(query.ToList());
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            var query = _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()).Where(filter);
            var result = query.SingleOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<int> Count(Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()).Count() :
                _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()).Where(filter).Count();

            return await Task.FromResult(query);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> filter)
        {
            var query = filter == null ? _context.Query<T>().Any(t => t.Type == typeof(T).Name.ToLower()) :
               _context.Query<T>().Where(t => t.Type == typeof(T).Name.ToLower()).Any(filter);

            return await Task.FromResult(query);
        }
    }
}
