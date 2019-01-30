using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
    public class Portfolio
    {
        public Dictionary<string, int> Stocks { get; set; }
        public double Value { get; set; }

        public Portfolio()
        {
            Stocks = new Dictionary<string, int>();
        }
    }
}
