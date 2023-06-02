using CoreRepository.IRepositries;
using CoreRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Item> Items { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Role> Roles { get; }
        IBaseRepository<Warehouse> Warehouses { get; }
        int Complete();
    }
}
