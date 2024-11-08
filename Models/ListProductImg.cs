using System;
using System.Collections.Generic;

namespace DoAnPM_TH_.Models;

public partial class ListProductImg
{
    public int Id { get; set; }

    public int? ProId { get; set; }

    public string? ImgUrl { get; set; }

    public virtual Product? Pro { get; set; }
}
