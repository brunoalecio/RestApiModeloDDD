
using Microsoft.EntityFrameworkCore;
using RestApiModeloDDD.Infrastructure.Data;
using RestApiModeloDDD.Application;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Application.Mappers;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Services;
using RestApiModeloDDD.Infrastructure.Data.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Repositories.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API - Modelo DDD",
        Version = "v1",
        Description = "API construída seguindo o modelo Domain-Driven Design."
    });
});

// --- config banco ---

var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");

if (databaseProvider == "SqlServer") // Config. para SqlServer - escrita
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<SqlContext>(options =>
        options.UseSqlServer(connectionString)
    );

    builder.Services.AddScoped<IRepositoryCliente, RepositoryCliente>();
    builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();
}
else if (databaseProvider == "MongoDb") // Config. para mongoDb - leitura
{
    builder.Services.AddSingleton<NoSqlContext>();

    builder.Services.AddScoped<IRepositoryCliente, RepositoryClienteMongo>();
    builder.Services.AddScoped<IRepositoryProduto, RepositoryProdutoMongo>();
}
else
{
    throw new Exception("Provedor de banco de dados não configurado ou inválido.");
}

// Application
builder.Services.AddScoped<IApplicationServiceCliente, ApplicationServiceCliente>();
builder.Services.AddScoped<IApplicationServiceProduto, ApplicationServiceProduto>();

// Domain
builder.Services.AddScoped<IServiceCliente, ServiceCliente>();
builder.Services.AddScoped<IServiceProduto, ServiceProduto>();

// Mappers
builder.Services.AddScoped<IMapperCliente, MapperCliente>();
builder.Services.AddScoped<IMapperProduto, MapperProduto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // NOVO: Habilita o Swagger apenas no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); // Mantém o mapeamento para as páginas Razor
// NOVO: Mapeia as rotas para os seus Controllers de API
app.MapControllers();

app.Run();