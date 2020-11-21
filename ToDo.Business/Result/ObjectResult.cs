namespace ToDo.Business.Result
{
    public class ObjectResult<TEntity> where TEntity : class
    {
        public Meta Meta { get; set; }
        public TEntity Entity { get; set; }

        public ObjectResult(Meta meta, TEntity entity)
        {
            Meta = meta;
            Entity = entity;
        }

        public ObjectResult(Meta meta)
        {
            Meta = meta;
        }
    }
}
