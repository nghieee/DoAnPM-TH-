using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Brand
{
    public string BrandId { get; set; } = null!;

    public string? BrandName { get; set; }

    public string? BrandOrigin { get; set; }

    public virtual ICollection<Product> ProductBrandOrigins { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductBrands { get; set; } = new List<Product>();
}
