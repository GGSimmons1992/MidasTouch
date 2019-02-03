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
  }
}
