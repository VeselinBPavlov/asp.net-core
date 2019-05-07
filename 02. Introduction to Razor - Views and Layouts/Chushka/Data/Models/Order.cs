namespace Chushka.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Order
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ClientId { get; set; }
        public User Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
