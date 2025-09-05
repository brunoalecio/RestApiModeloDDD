using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Base
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            var obj_banco = _repository.GetById(obj.Id);

            if (obj_banco != null)
            {
                _repository.Remove(obj_banco);
            }
            else
            {
                throw new Exception("Cliente não encontrado!");
            }
        }

        public void Update(TEntity obj)
        {
            if (obj.IsValido())
            {
                _repository.Update(obj);
            }

            if (obj == null)
            {
                throw new Exception("Cliente não encontrado!");
            }
        }
    }
}