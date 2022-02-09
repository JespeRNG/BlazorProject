using System.Collections.Generic;

namespace BlazorApp.DAL.Entities
{
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; private set; } = new HashSet<User>();
    }
}
