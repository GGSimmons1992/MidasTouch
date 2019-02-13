using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidasTouch.Data.Helpers
{
    public class IdentityHelper
    {
        public MidasTouchDBContext _db { get; set; }
        public InMemoryDbContext _idb { get; set; }

        public IdentityHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public IdentityHelper(InMemoryDbContext idb)
        {
            _idb = idb;
        }

        public long SetIdentity(Identity domidentity)
        {
            if (_db == null) {var _db=_idb;}

            _db.Identities.Add(domidentity);
            return _db.SaveChanges();
        }

        public List<Identity> GetIdentities()
        {
            if (_db == null) { var _db = _idb; }
            return _db.Identities.Include(x => x.Name).ToList();
        }

        public Identity GetIdentityByUser(User user)
        {
            if (_db == null) { var _db = _idb; }
            return _db.Identities.Include(x => x.Name).Where(i => i.Id == user.Identity.Id).FirstOrDefault();
        }

        public Identity GetIdentityByName(Name name)
        {
            if (_db == null) { var _db = _idb; }
            return  _db.Identities.Include(x => x.Name).Where(i => i.Name.Id == name.Id).FirstOrDefault();
        }
    }
}
