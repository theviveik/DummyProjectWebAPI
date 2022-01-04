using ServiceModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMarketAPIs.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> FindByName(string UserName);

        Task<UserModel> AddUserAsync(UserModel userModel);
    }
}
