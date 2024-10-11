using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Brand
{
    public string BrandId { get; set; } = null!;

    public string? BrandName { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
