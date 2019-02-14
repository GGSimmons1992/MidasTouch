using MidasTouch.Domain.Abstracts;

namespace MidasTouch.Domain.Models
{
  public class Identity : AThing
  {
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public override bool IsValid()
    {
      return
        (Name.IsValid() && Validator.ValidateString(this));
    }
  }
}