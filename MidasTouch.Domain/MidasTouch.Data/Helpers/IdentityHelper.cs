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
        public virtual MidasTouchDBContext _db { get; set; }

        public IdentityHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public long SetIdentity(Identity domidentity)
        {
            _db.Identities.Add(domidentity);
            return _db.SaveChanges();
        }

        public List<Identity> GetIdentities()
        {
            return _db.Identities.Include(x => x.Name).ToList();
        }

        public Identity GetIdentityByUser(User user)
        {
            return _db.Identities.Include(x => x.Name).Where(i => i.Id == user.Identity.Id).FirstOrDefault();
        }

        public Identity GetIdentityByName(Name name)
        {
            return  _db.Identities.Include(x => x.Name).Where(i => i.Name.Id == name.Id).FirstOrDefault();
        }
    }
}
