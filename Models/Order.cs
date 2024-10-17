using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string? StatusOrder { get; set; }

    public string? Address { get; set; }

    public string? PaymentMethod { get; set; }

    public string? StatusPayment { get; set; }

    public DateTime? TimeOrder { get; set; }

    public DateTime? TimePay { get; set; }

    public DateTime? TimeDelivery { get; set; }

    public string? UserId { get; set; }

    public double? TotalPrice { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
