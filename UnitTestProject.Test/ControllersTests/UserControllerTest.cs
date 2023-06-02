using CoreRepository;
using DataRepository;
using FluentAssertions;
using HappyCompany_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Test.MocData;
using Xunit;

namespace UnitTestProject.Test.ControllersTests
{
    public class UserControllerTest : BaseUnitTest
    {

        [Fact]
        public async Task GetUsers_ShouldReturn200Status()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();

            _context.User.AddRange(UserMocData.GetUsers().ToList());
            _context.SaveChanges();
            IUnitOfWork _mangaer = new UnitOfWork(_context);
            var userTest = new UserContraller(configuration, _mangaer);
            var result = (OkObjectResult)userTest.GetUsers(1, 3);

            result.StatusCode.Should().Be(200);
        }
    }
}
