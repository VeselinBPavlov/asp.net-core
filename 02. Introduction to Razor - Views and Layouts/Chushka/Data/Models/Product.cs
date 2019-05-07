namespace Chushka.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Enums.Type Type { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Product()
        {
            this.Orders = new HashSet<Order>();
        }
    }
}
