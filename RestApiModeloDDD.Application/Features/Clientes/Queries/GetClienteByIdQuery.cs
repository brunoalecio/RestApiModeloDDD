using MediatR;
using RestApiModeloDDD.Application.Dtos;

//eu
namespace RestApiModeloDDD.Application.Features.Clientes.Queries
{
    public class GetClienteByIdQuery : IRequest<ClienteDto>
    {
        public int Id { get; set; }
    }
}
