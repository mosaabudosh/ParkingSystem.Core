

namespace ParkingSystem.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task CommitChanges();
    }
}
