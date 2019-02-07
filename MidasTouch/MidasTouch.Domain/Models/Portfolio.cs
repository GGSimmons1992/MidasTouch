using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class Portfolio: AThing
  {
    public List<Share> Shares { get; set; }
    public double Value
        { get
            {
                double c = 0;
                foreach (var item in Shares)
                {
                    c += (item.NumberOfShares * item.Price);
                }
                return c;
            }
        }

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
