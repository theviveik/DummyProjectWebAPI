namespace DataAccessLayer.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();

        Task<int> CompleteAsync();
    }
}
