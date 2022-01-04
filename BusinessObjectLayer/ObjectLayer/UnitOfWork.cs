using DataAccessLayer.DataModel;
using DataAccessLayer.Interface;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ShareMarketContext _context;
        public UnitOfWork(ShareMarketContext context)
        {
            _context = context;
            Users = new UserRepository(context);
        }
        public IUserRepository Users { get; private set; }
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
