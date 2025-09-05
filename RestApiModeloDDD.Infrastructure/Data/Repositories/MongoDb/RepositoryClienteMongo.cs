using MongoDB.Driver;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    // A classe assina o mesmo contrato IRepositoryCliente
    public class RepositoryClienteMongo : IRepositoryCliente
    {
        private readonly IMongoCollection<Cliente> _collection;

        // Recebe o NoSqlContext em vez do SqlContext
        public RepositoryClienteMongo(NoSqlContext context)
        {
            _collection = context.Clientes;
        }

        public void Add(Cliente obj)
        {
            // Lógica para gerar o próximo ID (simplificada para o aprendizado)
            // Em um projeto de produção, usaríamos uma coleção de "contadores".
            var ultimoCliente = _collection.Find(Builders<Cliente>.Filter.Empty)
                                          .SortByDescending(c => c.Id)
                                          .Limit(1)
                                          .FirstOrDefault();
            obj.Id = (ultimoCliente?.Id ?? 0) + 1;

            _collection.InsertOne(obj);
        }

        public Cliente GetById(int id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void Update(Cliente obj)
        {
            _collection.ReplaceOne(c => c.Id == obj.Id, obj);
        }

        public void Remove(Cliente obj)
        {
            _collection.DeleteOne(c => c.Id == obj.Id);
        }
    }
}