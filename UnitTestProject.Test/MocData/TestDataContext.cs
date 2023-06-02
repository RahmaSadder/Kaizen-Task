using DataRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Test.MocData
{
    public class TestDataContext : DataContext
    {
        public TestDataContext(DbContextOptions<DataContext> context) : base(context)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
        }
    }
}
