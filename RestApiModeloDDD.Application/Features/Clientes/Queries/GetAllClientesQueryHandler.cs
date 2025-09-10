using MediatR;
using MongoDB.Driver;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Interfaces.Mappers;

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<ClienteDto>>
{
    private readonly IQueryContext _queryContext; // Injeta o contexto do Mongo
    private readonly IMapperCliente _mapperCliente;

    public GetAllClientesQueryHandler(IQueryContext queryContext, IMapperCliente mapperCliente)
    {
        _queryContext = queryContext;
        _mapperCliente = mapperCliente;
    }

    public async Task<IEnumerable<ClienteDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        // Busca os dados diretamente da coleção do MongoDB
        var clientes = await _queryContext.Clientes.Find(_ => true).ToListAsync(cancellationToken);

        // Mapeia para DTO para retornar
        return _mapperCliente.MapperListClientesDto(clientes);
    }
}