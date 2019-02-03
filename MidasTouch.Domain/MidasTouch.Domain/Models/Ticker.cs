using MidasTouch.Domain.Abstracts;

namespace MidasTouch.Domain.Models
{
  public class Ticker : AThing
  {
    public string Symbol { get; set; }
    public double Beta { get; set; }
    public Stock Stocks { get; set; }
  }
}