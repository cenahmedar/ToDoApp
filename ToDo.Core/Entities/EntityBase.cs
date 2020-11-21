using System;

namespace ToDo.Core.Entities
{
    public class EntityBase<T>
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Type { get { return typeof(T).Name.ToLower(); } }
        public string Id { get; set; }

    }
}
