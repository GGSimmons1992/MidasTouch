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
        private MidasTouchDBContext _db { get; set; }

        public ShareHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public long SetShare(Share share)
        {
            _db.Shares.Add(share);
            return _db.SaveChanges();
        }

        public List<Share> GetShares()
        {
            return _db.Shares.ToList();
        }
    }
}
