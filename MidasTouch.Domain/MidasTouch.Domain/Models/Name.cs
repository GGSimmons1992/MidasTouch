﻿using MidasTouch.Domain.Abstracts;

namespace MidasTouch.Domain.Models
{
  public class Name : AThing
  {
    public string First { get; set; }
    public string Last { get; set; }
  }
}