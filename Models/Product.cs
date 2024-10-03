using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Product
{
    public string ProId { get; set; } = null!;

    public string? ProName { get; set; }

    public double? OldPrice { get; set; }

    public double? NewPrice { get; set; }

    public int? UnitPrice { get; set; }

    public string? Descript { get; set; }

    public int? Quantity { get; set; }

    public string? Origin { get; set; }

    public string? Status { get; set; }

    public string? CateId { get; set; }

    public string? BrandId { get; set; }

    public string? ManId { get; set; }

    public double? Rating { get; set; }

    public string? ProImg { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual Manufacturer? Man { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual ICollection<Voucher> Vouchers { get; } = new List<Voucher>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
