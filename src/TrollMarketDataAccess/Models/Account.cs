using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
