using CoreRepository.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Test.MocData
{
    public class UserMocData
    {
        private static List<User> users = new List<User>()
        {
            new User(){ Id = Guid.NewGuid() ,  Email = "rahma@test.com" , Active = true ,Role = "Admin" , FullName = "Rahma sadder" , Password = "123"},
            new User(){ Id = Guid.NewGuid() ,  Email = "Mohammad@test.com" , Active = true ,Role = "Auditor" , FullName = "Mohammad test" , Password = "123"},
            new User(){ Id = Guid.NewGuid() ,  Email = "Omer@test.com" , Active = true ,Role = "Management" , FullName = "Omer abd" , Password = "123"}
        };

        public static IQueryable<User> GetUsers()
        {
            return users.AsQueryable();
        }
    }
}
