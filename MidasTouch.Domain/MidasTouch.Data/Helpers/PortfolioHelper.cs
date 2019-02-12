using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidasTouch.Data.Helpers
{
    public class PortfolioHelper
    {
        public virtual MidasTouchDBContext _db { get; set; }

        public PortfolioHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public long SetPortfolio(Portfolio domportfolio)
        {
            _db.Portfolios.Add(domportfolio);
            return _db.SaveChanges();
        }

        public List<Portfolio> GetPortfolios()
        {
            var portfolioList = _db.Portfolios.Include(x => x.Shares).ToList();
            return portfolioList;
        }

        public Portfolio GetPortfolioByUser(User domuser)
        {
            var domportfolio = _db.Portfolios.Include(x => x.Shares).Where(p=>p.Id==domuser.Portfolio.Id).FirstOrDefault();
            return domportfolio;
        }

    }
}
