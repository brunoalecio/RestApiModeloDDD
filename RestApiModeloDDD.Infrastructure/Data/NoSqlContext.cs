using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RestApiModeloDDD.Domain.Entities;

public class NoSqlContext
{
    private readonly IMongoDatabase _database;

    public NoSqlContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
        _database = client.GetDatabase("RestApiDDD_Mongo");
    }

    // Método para pegar uma "tabela" (Collection) de Clientes
    public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");

    // Método para pegar uma "tabela" (Collection) de Produtos
    public IMongoCollection<Produto> Produtos => _database.GetCollection<Produto>("Produtos");
}