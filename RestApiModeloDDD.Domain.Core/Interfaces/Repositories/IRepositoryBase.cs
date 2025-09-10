namespace RestApiModeloDDD.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
