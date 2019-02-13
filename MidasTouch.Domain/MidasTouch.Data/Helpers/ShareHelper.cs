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
            if (_db == null) { var _db = _idb; }
            _db.Shares.Add(share);
            return _db.SaveChanges();
        }

        public List<Share> GetShares()
        {
            if (_db == null) { var _db = _idb; }
            return _db.Shares.ToList();
        }

        public List<Share> GetSharesByUser(User domuser)
        {
            if (_db == null) { var _db = _idb; }
            var domportfolio = ph.GetPortfolioByUser(domuser);
            var pshares = domportfolio.Shares;

            return pshares;
        }

    }
}
