using Microsoft.EntityFrameworkCore;
using MidasTouch.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasTouch.Tests.HelperTests.HelperModels
{
    class TestDbContext:MidasTouchDBContext
    {
        protected new void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
