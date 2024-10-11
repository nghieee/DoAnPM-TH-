using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Category
{
    public string CateId { get; set; } = null!;

    public string? CateName { get; set; }

    public string? CateImg { get; set; }

    public int? CateProductCount { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
