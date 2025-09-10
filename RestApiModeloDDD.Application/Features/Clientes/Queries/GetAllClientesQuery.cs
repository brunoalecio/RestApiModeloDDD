using MediatR;
using RestApiModeloDDD.Application.Dtos;
using System.Collections.Generic;

// Define que esta consulta é uma requisição que retornará uma lista de ClienteDto
public class GetAllClientesQuery : IRequest<IEnumerable<ClienteDto>>
{
}