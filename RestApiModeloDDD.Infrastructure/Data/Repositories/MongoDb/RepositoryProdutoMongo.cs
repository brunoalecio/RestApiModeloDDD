using MongoDB.Driver;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories.MongoDb
{
    public class RepositoryProdutoMongo : IRepositoryProduto
    {
        private readonly IMongoCollection<Produto> _collection;

        public RepositoryProdutoMongo(NoSqlContext context)
        {
            _collection = context.Produtos;
        }

        public void Add(Produto entity)
        {
            // Lógica para gerar o próximo ID (simplificada para o aprendizado)
            // Em um projeto de produção, usaríamos uma coleção de "contadores".
            var ultimoProduto = _collection.Find(Builders<Produto>.Filter.Empty)
                                          .SortByDescending(c => c.Id)
                                          .Limit(1)
                                          .FirstOrDefault();
            entity.Id = (ultimoProduto?.Id ?? 0) + 1;

            _collection.InsertOne(entity);
        }

        public IEnumerable<Produto> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Produto GetById(int id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefault();
        }

        public void Remove(Produto entity)
        {
            _collection.DeleteOne(c => c.Id == entity.Id);
        }

        public void Update(Produto entity)
        {
            _collection.ReplaceOne(c => c.Id == entity.Id, entity);
        }
    }
}
