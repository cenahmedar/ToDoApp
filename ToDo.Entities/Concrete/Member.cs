using ToDo.Core.Entities;

namespace ToDo.Entities.Concrete
{
    public class Member : EntityBase<Member>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
