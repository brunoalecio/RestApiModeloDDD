using MediatR;
using RestApiModeloDDD.Application.Dtos;

// Define que este comando é uma requisição (IRequest) e que ele retornará um int (o ID do novo cliente)
public class CreateClienteCommand : IRequest<int>
{
    public ClienteDto ClienteDto { get; set; }
}