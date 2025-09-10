using MediatR;
using MongoDB.Driver;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Features.Clientes.Queries;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Application.Interfaces.Mappers;

public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto>
{
    private readonly IQueryContext _queryContext;
    private readonly IMapperCliente _mapperCliente;

    public GetClienteByIdQueryHandler(IQueryContext queryContext, IMapperCliente mapperCliente)
    {
        _queryContext = queryContext;
        _mapperCliente = mapperCliente;
    }

    // A assinatura do Handle deve corresponder à interface: recebe GetClienteByIdQuery e retorna Task<ClienteDto>
    public async Task<ClienteDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        // A lógica da busca agora usa o Id do request para filtrar
        var cliente = await _queryContext.Clientes
                                       .Find(c => c.Id == request.Id)
                                       .FirstOrDefaultAsync(cancellationToken);

        if (cliente == null)
        {
            return null; // O Controller vai tratar isso como um 404 Not Found
        }

        return _mapperCliente.MapperEntityToDto(cliente);
    }
}