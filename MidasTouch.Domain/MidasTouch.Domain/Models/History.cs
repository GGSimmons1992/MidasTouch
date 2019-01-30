using System;
using System.Collections.Generic;

namespace MidasTouch.Domain.Models
{
    public class History
    {
        public Dictionary<DateTime, double> PriceHistory { get; set; }
    }
}