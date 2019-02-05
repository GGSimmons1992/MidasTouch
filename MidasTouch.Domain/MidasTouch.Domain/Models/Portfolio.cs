using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class Portfolio: AThing
  {
    public List<Share> Shares { get; set; }
    public double Value { get; set; }

    public Portfolio()
    {
      Shares = new List<Share>();
    }

    public override bool IsValid()
    {
      return
        Validator.ValidateMoney(this) &&
        (Shares != null);
    }
  }
}
