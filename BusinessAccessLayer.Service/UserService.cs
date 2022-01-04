
using AutoMapper;
using BusinessAccessLayer.Models;
using DataAccessLayer.DataModel;
using DataAccessLayer.Interface;

namespace BusinessAccessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserModel> FindByName(string UserName)
        {
            var result = await _userRepository.FindAsync(
                (Users users) =>
                    users.UserName == UserName
                );
            if (result != null && result.Count() > 0)
            {
                var user = result.FirstOrDefault();
                var password = Utility.Decrypt(user.Password);
                user.Password = password;
                return _mapper.Map<UserModel>(user);
            }
            return _mapper.Map<UserModel>(result?.FirstOrDefault());
        }
                
        public async Task<UserModel> GetByIdAsync(long userId)
        {
            var existUser = await _userRepository.GetByIdAsync(userId);
            if (existUser != null)
            {
                var password = Utility.Decrypt(existUser.Password);
                existUser.Password = password;
            }
            return _mapper.Map<UserModel>(existUser);
        }


        public bool CheckPassword(UserModel model, string password)
        {
            return !string.IsNullOrEmpty(password) && model.Password == password;
        }

        public async Task<UserModel> AddUserAsync(UserModel userModel)
        {
            userModel.UserType = userModel.UserType ?? "users";
            var password = Utility.Encrypt(userModel.Password);
            userModel.Password = password;

            var user = _mapper.Map<Users>(userModel);
            
            await _userRepository.AddAsync(user);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> UpdateUserAsync(UserModel userModel)
        {
            var existUser = await _userRepository.FindAsync(
                (Users users) =>
                    users.Id != userModel.UserId && users.UserName == userModel.UserName
                );
            if (existUser == null || existUser?.Count() == 0)
            {
                var password = Utility.Encrypt(userModel.Password);
                userModel.Password = password;

                var user = _mapper.Map<Users>(userModel);
                _userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserAsync(long userId)
        {
            var userExist = await _userRepository.GetByIdAsync(userId);

            if (userExist != null)
            {
                var user = _mapper.Map<Users>(userExist);
                _userRepository.Remove(user);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}
