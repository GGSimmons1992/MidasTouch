using MidasTouch.Domain.Abstracts;
using System;
using System.Collections.Generic;

namespace MidasTouch.Domain.Models
{
  public class History: AThing
  {
    public List<Share> HistoricalData { get; set; }
  }
}