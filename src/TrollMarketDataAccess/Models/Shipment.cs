using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public bool? IsService { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
