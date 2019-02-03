using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Domain.Abstracts
{
  public class AThing
  {
    public int Id { get; set; }

    public virtual bool IsValid()
    {
      return this.Id > 0;
    }
  }
}
