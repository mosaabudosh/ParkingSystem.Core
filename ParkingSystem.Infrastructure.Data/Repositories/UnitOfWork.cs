using ParkingSystem.Domain.IRepositories;
using ParkingSystem.Infrastructure.Data.Context;


namespace ParkingSystem.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Fields
        ParkingSystemContext _parkingSystemContext;
        Dictionary<Type, object> _repositoryDictionary;
        #endregion

        #region Constructor
        public UnitOfWork(ParkingSystemContext e_CommerceContext)
        {
            _parkingSystemContext = e_CommerceContext;
            _repositoryDictionary = new Dictionary<Type, object>();
        }

        #endregion

        #region Methods

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            GenericRepository<T> genericRepository;

            genericRepository = GetRepositoryIfExist<T>();

            if (genericRepository == null)
            {
                genericRepository = new GenericRepository<T>(_parkingSystemContext);
                AddRepositoryToDictionary(genericRepository);
            }

            return genericRepository;
        }

        private GenericRepository<T> GetRepositoryIfExist<T>() where T : class
        {
            if (_repositoryDictionary.ContainsKey(typeof(T)))
                return (GenericRepository<T>)_repositoryDictionary[typeof(T)];
            else
                return null;
        }

        private void AddRepositoryToDictionary<T>(GenericRepository<T> genericRepository) where T : class
        {
            _repositoryDictionary.Add(typeof(T), genericRepository);
        }

        #endregion

        #region IUnitOfWork

        public async Task CommitChanges()
        {
            await _parkingSystemContext.SaveChangesAsync();
        }
        #endregion
    }
}
