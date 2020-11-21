using System.Collections.Generic;

namespace ToDo.Business.Result
{
    public class ListResult<TEntity> where TEntity : class
    {
        public Meta Meta { get; set; }
        public List<TEntity> Entities { get; set; }

        public ListResult(Meta meta, List<TEntity> entities)
        {
            Meta = meta;
            Entities = entities;
        }

        public ListResult(Meta meta)
        {
            Meta = meta;
        }
    }
}
