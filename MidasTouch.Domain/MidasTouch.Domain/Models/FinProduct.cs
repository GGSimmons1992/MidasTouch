using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
    public class FinProduct
    {
        public string CompanyName { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }
        public int Shares { get; set; }
        public double Beta { get; set; }
        public double PurchasePrice { get; set; }
        public History History { get; set; }

        public void Flux()
        {
            var Change = Beta - 1;
            var Random = new Random();

            var Delta = Random.NextDouble();
            Price += Price * (2 * Change * Delta - Change);
            History.PriceHistory.Add(DateTime.Now, Price);
        }
    }
}
