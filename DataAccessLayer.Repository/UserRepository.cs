using DataAccessLayer.DataModel;
using DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(DummyProjectContext context) : base(context)
        {
        }
    }
}
