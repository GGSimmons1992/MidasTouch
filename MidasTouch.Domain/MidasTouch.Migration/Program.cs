using MidasTouch.Data.Helpers;
using MidasTouch.Domain.Models;
using System;
using System.Collections.Generic;

namespace MidasTouch.Migration
{
    class Program
    {
        protected Program()
        {
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Why are you here?");
            
        }

        static void CreateTestUser()
        {
            var uh = new UserHelper();
            var Share1 = new Share()
            {
                NumberOfShares = 100,
                Price = 50,
                Symbol = "GOOG"
            };

            var Share2 = new Share()
            {
                NumberOfShares = 100,
                Price = 100,
                Symbol = "GOOG"
            };

            var Sut = new User()
            {
                AccountBalance = (double)5000000m,
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
                    Email="John.Schmidt@MidasTouch.com",
                    Password="607d!"
                }
            };
            uh.SetUser(Sut);
            Console.WriteLine("Finish Set up");
        }

    }
}
