using MediatR;
using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

// Define que esta classe é a manipuladora (Handler) para o CreateClienteCommand
public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, int>
{
    private readonly IServiceCliente _serviceCliente;
    private readonly IMapperCliente _mapperCliente;

    public CreateClienteCommandHandler(IServiceCliente serviceCliente, IMapperCliente mapperCliente)
    {
        _serviceCliente = serviceCliente;
        _mapperCliente = mapperCliente;
    }

    public Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        request.ClienteDto.Id = 0; // Garante que é uma criação
        var cliente = _mapperCliente.MapperDtoToEntity(request.ClienteDto);
        _serviceCliente.Add(cliente);

        return Task.FromResult(cliente.Id);
    }
}