using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;

namespace RestApiModeloDDD.Application.Features.Clientes.Commands
{
    public class UpdateClienteCommandHandler
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapperCliente _mapperCliente;

        public UpdateClienteCommandHandler(IServiceCliente serviceCliente, IMapperCliente mapperCliente)
        {
            _serviceCliente = serviceCliente;
            _mapperCliente = mapperCliente;
        }

        public Task<int> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = _mapperCliente.MapperDtoToEntity(request.ClienteDto);
            _serviceCliente.Update(cliente);

            return Task.FromResult(cliente.Id);
        }
    }
}
