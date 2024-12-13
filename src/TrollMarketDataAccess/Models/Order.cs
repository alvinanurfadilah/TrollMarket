using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int AccountId { get; set; }
        public int ShipmentId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}
