using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class FinProduct: AThing
  {
    public string CompanyName { get; set; }
    public string Ticker { get; set; }
    public int Shares { get; set; }
    public double Beta { get; set; }
    public History History { get; set; }

    public override bool IsValid()
    {

      return
    }
  }
}
