namespace BlazorApp.DAL.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public long RoleId { get; set; }
        public virtual Role Role { get; private set; }

    }
}
