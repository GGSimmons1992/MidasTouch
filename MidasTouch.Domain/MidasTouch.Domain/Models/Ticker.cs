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

    public override bool IsValid()
    {
      return
        Validator.ValidateString(this) &&
        Validator.ValidateMoney(this) &&
        Stocks.IsValid();
    }
  }
}