namespace Chushka.Data.Models
{
    using Enums;
    using System.Collections.Generic;

    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User()
        {
            this.Orders = new HashSet<Order>();
        }
    }
}
