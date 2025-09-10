using RestApiModeloDDD.Application.Interfaces.Mappers;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;

namespace RestApiModeloDDD.Application.Features.Clientes.Commands
{
    public class DeleteClienteCommandHandler
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapperCliente _mapperCliente;

        public DeleteClienteCommandHandler(IServiceCliente serviceCliente, IMapperCliente mapperCliente)
        {
            _serviceCliente = serviceCliente;
            _mapperCliente = mapperCliente;
        }

        public Task<int> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            _serviceCliente.Remove(request.Id);

            return Task.FromResult(request.Id);
        }
    }
}