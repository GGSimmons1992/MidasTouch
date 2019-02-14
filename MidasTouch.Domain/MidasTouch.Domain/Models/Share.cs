using System;
using System.Collections.Generic;
using System.Text;
using MidasTouch.Domain.Abstracts;
using MidasTouch.Domain.Models;

namespace MidasTouch.Domain.Models
{
  public class Share : AThing
  {
    public int NumberOfShares { get; set; }
    public string Symbol { get; set; }
    public double Price { get; set; }
    public string State { get; set; }
    public DateTime TimeStamp { get; set; }

    public Share()
    {
        TimeStamp = DateTime.Now;
    }

    public override bool IsValid()
    {
      return
        Validator.ValidateString(this) &&
        Validator.ValidateNumber(this) &&
        Validator.ValidateMoney(this);
    }
  }
}
