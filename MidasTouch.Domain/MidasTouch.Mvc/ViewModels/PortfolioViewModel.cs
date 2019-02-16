using System.Collections.Generic;
using MidasTouch.Domain.Models;

namespace MidasTouch.Mvc.ViewModels
{
    public class PortfolioViewModel
    {
        public IEnumerable<User> UserPortfolio { get; set; }
    }
}
