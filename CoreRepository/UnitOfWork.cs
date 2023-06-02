using RepositoryCore.IRepositries;
using RepositoryCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCore
{
    internal interface UnitOfWork:IDisposable
    {
        IBaseRepository<Item> Items { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Role> Roles { get; }
        IBaseRepository<Warehouse> Warehouses { get; }
        int Complete();
    }
}
