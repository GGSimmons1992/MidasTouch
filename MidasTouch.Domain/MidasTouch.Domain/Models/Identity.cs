using MidasTouch.Domain.Abstracts;

namespace MidasTouch.Domain.Models
{
  public class Identity : AThing
  {
    public Name Name { get; set; }

    public override bool IsValid()
    {
      return
        Name.IsValid();
    }
  }
}