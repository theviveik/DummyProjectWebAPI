using DataAccessLayer.DataModel;
using DataAccessLayer.Interface;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DummyProjectContext _context;
        public UnitOfWork(DummyProjectContext context)
        {
            _context = context;
            Users = new UserRepository(context);
        }
        public IUserRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
