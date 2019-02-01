namespace MidasTouch.Domain.Models
{
  public class Ticker
  {
    public string Symbol { get; set; }
    public double Beta { get; set; }
    public Stock Stocks { get; set; }
  }
}