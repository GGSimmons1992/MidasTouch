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
    public class IdentityHelperTests
    {
        public UserHelper uh { get; set; }
        public IdentityHelper ih { get; set; }
        public Share Share1 { get; set; }
        public Share Share2 { get; set; }
        public User Sut { get; set; }


        public IdentityHelperTests()
        {
            uh = new UserHelper(new InMemoryDbContext());
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
                        First = "Jane",
                        Last = "Doe"
                    }
                }
            };
        }

        [Fact]
        public void Test_GetIdentities()
        {
            uh.SetUser(Sut);
            Assert.NotNull(ih.GetIdentities());
            Assert.False((ih.GetIdentities())[0].Id <= 0);
        }

        [Fact]
        public void Test_GetIdentityByName()
        {
            uh.SetUser(Sut);
            var fname = Sut.Identity.Name.First;
            var lname = Sut.Identity.Name.Last;
            var name = uh._idb.Names.Where(
                n => ((n.First == fname) && (n.Last == lname))).FirstOrDefault();
            Assert.NotNull(ih.GetIdentityByName(name));
            Assert.True((ih.GetIdentityByName(name)).IsValid());
            Assert.False((ih.GetIdentityByName(name)).Id<=0);

        }



    }
}
