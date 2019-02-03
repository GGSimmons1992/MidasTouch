using MidasTouch.Domain.Abstracts;

namespace MidasTouch.Domain.Models
{
  public class Stock : AThing
  {
    public int NumberOfStocks { get; set; }
    public double Price { get; set; }
  }
}