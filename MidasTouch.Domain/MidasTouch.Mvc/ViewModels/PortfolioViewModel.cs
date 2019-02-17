using System;
using System.Collections.Generic;
using MidasTouch.Domain.Models;

namespace MidasTouch.Mvc.ViewModels
{
    public class PortfolioViewModel
    {

        public int Id { get; set; }
        public int NumberofShares { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string State { get; set; }
        public DateTime TimeStamp { get; set; }


        public List<Share> UserPortfolio { get; set; }
    }
}
