using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Models
{
  public class Company
  {
    public string Name { get; set; }
    public List<Ticker> MyProperty { get; set; }
  }
}
