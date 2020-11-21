using ToDo.Core.Entities;

namespace ToDo.Entities.Concrete
{
    public class MTask : EntityBase<MTask>
    {
        public string Content { get; set; }
        public string MemberId { get; set; }
        public bool IsFinish { get; set; }

    }
}
