using BusinessAccessLayer.Models;

namespace BusinessAccessLayer
{
    public interface IUserService
    {
        Task<UserModel> FindByName(string UserName);

        Task<UserModel> GetByIdAsync(long userId);

        Task<IEnumerable<UserModel>> GetAllAsync();

        bool CheckPassword(UserModel model, string password);

        Task<UserModel> AddUserAsync(UserModel userModel);

        Task<bool> UpdateUserAsync(UserModel userModel);

        Task<bool> DeleteUserAsync(long userId);
    }
}