using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class Voucher
{
    public string VoucherId { get; set; } = null!;

    public string? VoucherCode { get; set; }

    public DateTime? DayClose { get; set; }

    public int? PercentSale { get; set; }

    public int? ProId { get; set; }

    public virtual Product? Pro { get; set; }
}
