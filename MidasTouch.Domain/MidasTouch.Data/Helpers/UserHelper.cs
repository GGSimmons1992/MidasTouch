using Microsoft.EntityFrameworkCore;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidasTouch.Data.Helpers
{
    public class UserHelper
    {
        private MidasTouchDBContext _db { get; set; }
        private PortfolioHelper ph { get; set; }
        private IdentityHelper ih { get; set; }

        public UserHelper()
        {
            _db = new MidasTouchDBContext();
            ph = new PortfolioHelper();
            ih = new IdentityHelper();
        }

        public long SetUser(User domuser)
        {
            _db.Users.Add(domuser);
            return _db.SaveChanges();
        }

        public List<User> GetUserDependancies(List<User> userlist)
        {
            foreach (var user in userlist)
            {
                user.Portfolio = ph.GetPortfolioByUser(user);
                user.Identity = ih.GetIdentityByUser(user);
            }

            return userlist;
        }

        public List<User> GetUsers()
        {
            var userlist = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity).ToList();

            return GetUserDependancies(userlist);
        }

    }
}
