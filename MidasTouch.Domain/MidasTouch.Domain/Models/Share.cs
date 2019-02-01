using System;
using System.Collections.Generic;
using System.Text;
using MidasTouch.Domain.Models;

namespace MidasTouch.Domain.Models
{
  public class Share
  {
    public int NumberOfShares { get; set; }
    public string Symbol { get; set; }
    public double Price { get; set; }

    public void Flux()
    {
      var Change = FinProduct.Beta - 1;
      var Random = new Random();

      var Delta = Random.NextDouble();
      Price += Price * (2 * Change * Delta - Change);
      History.HistoricalData.Add(DateTime.Now, Price);
    }
  }
}
