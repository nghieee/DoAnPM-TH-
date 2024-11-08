using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Rating
{
    public string RateId { get; set; } = null!;

    public double? RateNumber { get; set; }

    public DateTime? DayClose { get; set; }

    public int? PercentSale { get; set; }

    public int? ProId { get; set; }

    public int? UserId { get; set; }

    public virtual Product? Pro { get; set; }

    public virtual User? User { get; set; }
}
