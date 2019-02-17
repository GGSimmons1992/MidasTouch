using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidasTouch.Data.Helpers
{
    public class ShareHelper
    {
        public MidasTouchDBContext _db { get; set; }
        public PortfolioHelper ph { get; set; }
        public InMemoryDbContext _idb { get; set; }

        public ShareHelper()
        {
            _db = new MidasTouchDBContext();
            ph = new PortfolioHelper();
        }

        public ShareHelper(InMemoryDbContext idb)
        {
            _idb = idb;
            ph = new PortfolioHelper(idb);
        }

        public long SetShare(Share share)
        {
            if (share.NumberOfShares == 0)
            {
                return 0;
            }

            if (_db != null)
            {
                _db.Shares.Add(share);
                return _db.SaveChanges();
            }
            else
            {
                _idb.Shares.Add(share);
                return _idb.SaveChanges();
            }
           
        }


    }
}
