using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Manufacturer
{
    public string ManId { get; set; } = null!;

    public string? ManName { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
