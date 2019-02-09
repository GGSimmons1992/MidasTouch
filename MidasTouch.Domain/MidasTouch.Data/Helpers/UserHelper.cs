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

        public UserHelper()
        {
            _db = new MidasTouchDBContext();
        }

        public long SetUser(User domuser)
        {
            _db.Users.Add(domuser);
            return _db.SaveChanges();
        }

        public List<User> GetUsers()
        {
            var userlist = _db.Users.Include(x => x.Portfolio).Include(y => y.Identity).ToList();
            return userlist;
        }

    }
}
