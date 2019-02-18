using System.Collections.Generic;
using System.Linq;
using MidasTouch.Data;
using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using Xunit;

namespace MidasTouch.Tests
{
    public class UserHelperTest
    {
        public UserHelper uh { get; set; }
        public Share Share1 { get; set; }
        public Share Share2 { get; set; }
        public User Sut { get; set; }


        public UserHelperTest()
        {
            uh = new UserHelper(new InMemoryDbContext());
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

            Sut = new User()
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
                        First = "John",
                        Last = "Schmidt"

                    },
                    Email = "pootietang@midastouch.com"
                }
            };
        }

        [Fact]
        public void Test_SetUser()
        {
            Assert.True(uh.SetUser(Sut) > 0);
        }

        [Fact]
        public void Test_GetUser()
        {
            uh.SetUser(Sut);
            var userlist = uh.GetUsers();
            Assert.NotNull(userlist);

            Assert.True((userlist[0]).IsValid());
            Assert.False((userlist[0].Portfolio.Shares[0].Id <= 0));
        }

        [Fact]
        public void Test_GetUserByName()
        {
            uh.SetUser(Sut);
            var userlist = uh.GetUsers();
            var name = (userlist.FirstOrDefault(u => u.Identity.Name.First == Sut.Identity.Name.First)).Identity.Name;
            Assert.NotNull(name);
            var datauser = uh.GetUserByName(name);

            var datashare50 = (datauser.Portfolio.Shares).FirstOrDefault(s => s.Price == 50);
            Assert.NotNull(datashare50);
            Assert.True(datauser.Portfolio.Value == Sut.Portfolio.Value);

        }

        [Fact]
        public void Test_GetUserByEmail()
        {
            uh.SetUser(Sut);
            var datauser = uh.GetUserByEmail(Sut.Identity.Email);
            Assert.NotNull(datauser);
            var datashare50 = (datauser.Portfolio.Shares).FirstOrDefault(s => s.Price == 50);
            Assert.NotNull(datashare50);
            Assert.True(datauser.Portfolio.Value == Sut.Portfolio.Value);

        }
    }
}
