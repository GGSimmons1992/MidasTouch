using MidasTouch.Domain.Abstracts;
using System;

namespace MidasTouch.Domain.Models
{
  public class Ticker : AThing
  {
    public string Symbol { get; set; }
    public double Beta { get; set; }
    public Stock Stocks { get; set; }

    public void Flux()
    {
      var Change = Beta - 1;
      var Random = new Random();

      var Delta = Random.NextDouble();
      Stocks.Price += Stocks.Price * (2 * Change * Delta - Change);
    }

    public override bool IsValid()
    {
      return
        Validator.ValidateString(this) &&
        Validator.ValidateMoney(this) &&
        Stocks.IsValid();
    }
  }
}