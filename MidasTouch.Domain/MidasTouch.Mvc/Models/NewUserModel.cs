using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MidasTouch.Domain.Models;

namespace MidasTouch.Mvc.Models
{
    public class NewUserModel:User
    {
        public string Secondary { get; set; }
    }
}
