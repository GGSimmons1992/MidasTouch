﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;

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


        public Portfolio GetPortfolioByUser(User domuser)
        {
            if (_db != null)
            {
                var domportfolio = _db.Portfolios.Include(x => x.Shares).Where(p => p.Id == domuser.Portfolio.Id).FirstOrDefault();
                return domportfolio;
            }
            else
            {
                var domportfolio = _idb.Portfolios.Include(x => x.Shares).Where(p => p.Id == domuser.Portfolio.Id).FirstOrDefault();
                return domportfolio;
            }

        }

    }
}
