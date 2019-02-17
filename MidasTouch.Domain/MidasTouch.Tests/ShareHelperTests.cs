using MidasTouch.Data;
using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MidasTouch.Tests
{
    public class ShareHelperTests
    {
        public UserHelper uh { get; set; }
        public ShareHelper sh { get; set; }
        public IdentityHelper ih { get; set; }
        public Share Share1 { get; set; }
        public Share Share2 { get; set; }
        public Share Share3 { get; set; }
        public User User { get; set; }


        public ShareHelperTests()
        {
            uh = new UserHelper(new InMemoryDbContext());
            sh = new ShareHelper(new InMemoryDbContext());
            ih = new IdentityHelper(new InMemoryDbContext());
            Share1 = new Share()
            {
                NumberOfShares = 100,
                Price = 50,
                Symbol = "GOOG"
            };

            Share2 = new Share()
            {
                NumberOfShares = 100,
                Price = 100,
                Symbol = "GOOG"
            };

            Share3 = new Share()
            {
                NumberOfShares = 40,
                Price = 1000,
                Symbol = "MSFT"
            };

            User = new User()
            {
                AccountBalance = (double)5000m,
                Portfolio = new Portfolio()
                {
                    Shares = new List<Share>() { Share1, Share2 }
                },
                Identity = new Identity()
                {
                    Name = new Name()
                    {
                        First = "James",
                        Last = "Bond"
                    }
                }
            };
        }

        [Fact]
        public void Test_SetShares()
        {
            var db = uh._idb;

            uh.SetUser(User);
            Assert.True(sh.SetShare(Share3) > 0);

            var lastshare = db.Shares.Where(s => s.Symbol == Share3.Symbol).LastOrDefault();
            var dataname = db.Names.Where(n => n.First == User.Identity.Name.First).FirstOrDefault();
            var datauser = uh.GetUserByName(dataname);
            datauser.Portfolio.Shares.Add(lastshare);
            Assert.True(db.SaveChanges() > 0);
            Assert.True(datauser.Portfolio.Shares.Count == 3);

        }

        [Fact]
        public void Test_FailSetShares()
        {
            var db = uh._idb;

            uh.SetUser(User);
            Share3.NumberOfShares = 0;
            Assert.False(sh.SetShare(Share3) > 0);
        }

    }
}
