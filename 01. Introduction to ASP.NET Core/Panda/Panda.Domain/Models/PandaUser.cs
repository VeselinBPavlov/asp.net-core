namespace Panda.Domain.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            Packages = new HashSet<Package>();
            Receipts = new HashSet<Receipt>();
        }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
