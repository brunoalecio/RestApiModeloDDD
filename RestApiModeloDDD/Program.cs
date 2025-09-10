using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Application.Mappers;
using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Services;
using RestApiModeloDDD.Infrastructure.Data;
using RestApiModeloDDD.Infrastructure.Data.Repositories;

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

// --- MediatR ---
// Escaneia o projeto Application e registra todos os Handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateClienteCommand).Assembly));

// --- INFRAESTRUTURA DE ESCRITA (SQL Server) ---
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SqlContext>(options =>
    options.UseSqlServer(sqlConnectionString));

// --- INFRAESTRUTURA DE LEITURA (MongoDB) ---
builder.Services.AddSingleton<NoSqlContext>();

// --- REGISTRO DOS REPOSITÓRIOS (APENAS PARA ESCRITA) ---
builder.Services.AddScoped<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddScoped<IRepositoryProduto, RepositoryProduto>();

// --- REGISTRO DOS SERVIÇOS DE DOMÍNIO (APENAS PARA ESCRITA) ---
builder.Services.AddScoped<IServiceCliente, ServiceCliente>();
builder.Services.AddScoped<IServiceProduto, ServiceProduto>();

// --- REGISTRO DOS MAPPERS (USADO POR AMBOS) ---
builder.Services.AddScoped<IMapperCliente, MapperCliente>();
builder.Services.AddScoped<IMapperProduto, MapperProduto>();

builder.Services.AddScoped<IQueryContext, NoSqlContext>();

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