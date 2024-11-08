using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class OrderDetail
{
    public string OrderDetailId { get; set; } = null!;

    public int? Quantity { get; set; }

    public string? Address { get; set; }

    public string? PaymentMethod { get; set; }

    public double? OldPrice { get; set; }

    public double? NewPrice { get; set; }

    public double? TotalPrice { get; set; }

    public string? OrderId { get; set; }

    public int? ProId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Pro { get; set; }
}
