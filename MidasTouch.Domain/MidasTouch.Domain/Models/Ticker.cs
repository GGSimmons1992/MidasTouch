using MidasTouch.Domain.Abstracts;
using System;
using System.Security.Cryptography;

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
      var Random = RandomNumberGenerator.Create();

      byte[] Data = new byte[1];
      Random.GetBytes(Data);
      var numerator = BitConverter.ToDouble(Data,0);
     
      var Delta = numerator/255;
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