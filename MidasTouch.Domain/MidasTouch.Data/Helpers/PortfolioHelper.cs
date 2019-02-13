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
        public MidasTouchDBContext _db { get; set; }
        public InMemoryDbContext _idb { get; set; }

        public PortfolioHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public PortfolioHelper(InMemoryDbContext idb)
        {
            _idb = idb;
        }

        public long SetPortfolio(Portfolio domportfolio)
        {
            if (_db == null) { var _db = _idb; }
            _db.Portfolios.Add(domportfolio);
            return _db.SaveChanges();
        }

        public List<Portfolio> GetPortfolios()
        {
            if (_db == null) { var _db = _idb; }
            var portfolioList = _db.Portfolios.Include(x => x.Shares).ToList();
            return portfolioList;
        }

        public Portfolio GetPortfolioByUser(User domuser)
        {
            if (_db == null) { var _db = _idb; }
            var domportfolio = _db.Portfolios.Include(x => x.Shares).Where(p=>p.Id==domuser.Portfolio.Id).FirstOrDefault();
            return domportfolio;
        }

    }
}
