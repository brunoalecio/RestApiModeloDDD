using Microsoft.EntityFrameworkCore;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        private readonly SqlContext _sqlContext;

        public RepositoryBase(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                _sqlContext.Set<TEntity>().Add(obj);
                _sqlContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw  ex;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _sqlContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _sqlContext.Set<TEntity>().Find(id);
        }

        public void Remove(int id)
        {
            // Primeiro, busca a entidade pelo id
            var obj = GetById(id);

            if (obj != null)
            {
                // Se encontrou, manda remover
                _sqlContext.Set<TEntity>().Remove(obj);
                _sqlContext.SaveChanges();
            }
        }

        public void Update(TEntity obj)
        {
            try
            {

                _sqlContext.Entry(obj).State = EntityState.Modified;
                _sqlContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
    }
}
