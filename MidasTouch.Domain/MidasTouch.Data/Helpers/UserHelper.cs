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
        public virtual MidasTouchDBContext _db { get; set; }
        public virtual PortfolioHelper ph { get; set; }
        public virtual IdentityHelper ih { get; set; }

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

        public User GetUserDependancies(User user)
        {
            user.Portfolio = ph.GetPortfolioByUser(user);
            user.Identity = ih.GetIdentityByUser(user);

            return user;
        }

        public List<User> GetUsers()
        {
            var userlist = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity).ToList();
            if (userlist == null)
            {
                return userlist;
            }
            return GetUserDependancies(userlist);
        }

        public User GetUserByName(Name name)
        {
            var user = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity)
                .Where(u=>u.Identity.Name.Id==name.Id).FirstOrDefault();

            if (user == null)
            {
                return user;
            }

            return GetUserDependancies(user);
        }

    }
}
