﻿using System;
using System.Collections.Generic;

namespace Yungching.Domain.Entities;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
