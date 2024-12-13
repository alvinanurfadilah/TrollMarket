using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int AccountId { get; set; }
        public bool? Discontinue { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
