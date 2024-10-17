using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Manufacturer
{
    public string ManId { get; set; } = null!;

    public string? ManName { get; set; }

    public string? ManCountry { get; set; }

    public virtual ICollection<Product> ProductManCountries { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductMen { get; set; } = new List<Product>();
}
