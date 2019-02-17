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
        public MidasTouchDBContext _db { get; set; }
        public PortfolioHelper ph { get; set; }
        public IdentityHelper ih { get; set; }
        public InMemoryDbContext _idb { get; set; }

        public UserHelper()
        {
            _db = new MidasTouchDBContext();
            ph = new PortfolioHelper();
            ih = new IdentityHelper();
        }

        public UserHelper(InMemoryDbContext idb)
        {
            _idb = idb;
            ph = new PortfolioHelper(idb);
            ih = new IdentityHelper(idb);
        }


        public long SetUser(User domuser)
        {
            if (_db != null)
            {
                _db.Users.Add(domuser);
                return _db.SaveChanges();
            }
            else
            {
                _idb.Users.Add(domuser);
                return _idb.SaveChanges();
            }
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
            if (_db != null)
            {
                var userlist = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity).ToList();
                if (userlist == null)
                {
                    return userlist;
                }
                return GetUserDependancies(userlist);
            }
            else
            {
                var userlist = _idb.Users.Include(x => x.Portfolio).Include(y => y.Identity).ToList();
                if (userlist == null)
                {
                    return userlist;
                }
                return GetUserDependancies(userlist);
            }
            
        }

        public User GetUserByName(Name name)
        {
            if (_db != null)
            {
                var user = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity)
                .Where(u => u.Identity.Name.Id == name.Id).FirstOrDefault();

                if (user == null)
                {
                    return user;
                }

                return GetUserDependancies(user);
            }
            else
            {
                var user = _idb.Users.Include(x => x.Portfolio).Include(y => y.Identity)
                .Where(u => u.Identity.Name.Id == name.Id).FirstOrDefault();

                if (user == null)
                {
                    return user;
                }

                return GetUserDependancies(user);
            }
            
        }

        public User GetUserByEmail(string Email)
        {
            if (_db != null)
            {
                var user = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity)
                .Where(u => u.Identity.Email == Email).FirstOrDefault();

                if (user == null)
                {
                    return user;
                }

                return GetUserDependancies(user);
            }
            else
            {
                var user = _idb.Users.Include(x => x.Portfolio).Include(y => y.Identity)
                .Where(u => u.Identity.Email == Email).FirstOrDefault();

                if (user == null)
                {
                    return user;
                }

                return GetUserDependancies(user);
            }

        }

    }
}
