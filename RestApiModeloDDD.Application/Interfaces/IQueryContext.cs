using MongoDB.Driver;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Application.Interfaces
{
    // Este é o contrato que a camada de Aplicação precisa para fazer consultas.
    public interface IQueryContext
    {
        IMongoCollection<Cliente> Clientes { get; }
        IMongoCollection<Produto> Produtos { get; }
    }
}