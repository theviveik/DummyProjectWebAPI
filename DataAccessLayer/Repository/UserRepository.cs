using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceModel.Models;
using ShareMarketAPIs.DataModel;
using ShareMarketAPIs.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMarketAPIs.Repository.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly ShareMarketContext _shareMarketContext;
        private readonly IMapper _mapper;

        UserRepository(ShareMarketContext shareMarketContext, IMapper mapper)
        {
            _shareMarketContext = shareMarketContext;
            _mapper = mapper;
        }
       public async Task<List<UserModel>> GetAll()
        {
            var result = await _shareMarketContext.Users.ToListAsync();

            return _mapper.Map<List<UserModel>>(result);
        }

        public async Task<UserModel> FindByName(string UserName)
        {
            var result = await _shareMarketContext.Users.FirstOrDefaultAsync();

            return _mapper.Map<UserModel>(result);
        }

        public async Task<UserModel> AddUserAsync(UserModel userModel)
        {
            var users = new Users()
            {
                Password = userModel.Password,
                UserName = userModel.UserName,
                UserType = userModel.UserType,
                Email = "vivek.sharma3@conduent.com",

            };
            _shareMarketContext.Users.Add(users);


            await _shareMarketContext.SaveChangesAsync();

            return _mapper.Map<UserModel>(users);
        }
    }
}
