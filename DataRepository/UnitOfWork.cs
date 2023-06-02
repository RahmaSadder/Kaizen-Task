using CoreRepository;
using CoreRepository.IRepositries;
using CoreRepository.Models;
using DataRepository.Context;
using DataRepository.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IBaseRepository<Item> Items { get; private set; }
        public IBaseRepository<User> Users { get; private set; }
        public IBaseRepository<Role> Roles { get; private set; }
        public IBaseRepository<Warehouse> Warehouses { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Items = new BaseRepository<Item>(_context);
            Users = new BaseRepository<User>(_context);
            Roles = new BaseRepository<Role>(_context);
            Warehouses = new BaseRepository<Warehouse>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
