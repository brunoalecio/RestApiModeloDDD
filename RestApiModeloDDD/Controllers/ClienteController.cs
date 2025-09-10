using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Features.Clientes.Queries;
using RestApiModeloDDD.Application.Features.Clientes.Commands;

namespace RestApiModeloDDD.API.Controllers
{
    // Rota padrão para APIs: "api/nomeDoController"
    [Route("api/[controller]")]
    [ApiController]
    // Herda de ControllerBase, que é mais leve e apropriado para APIs
    public class ClientesController : ControllerBase
    {
        // A única dependência agora é o "carteiro" MediatR
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            // Cria e envia a consulta para o MediatR
            var query = new GetAllClientesQuery();
            var clientes = await _mediator.Send(query);
            return Ok(clientes);
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var query = new GetClienteByIdQuery { Id = id };
            var cliente = await _mediator.Send(query);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }
            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
                return BadRequest("Dados do cliente inválidos.");

            var command = new CreateClienteCommand { ClienteDto = clienteDto };
            var novoClienteId = await _mediator.Send(command);

            // Retorna um status 201 Created com a localização do novo recurso
            return CreatedAtAction(nameof(Get), new { id = novoClienteId }, clienteDto);
        }

        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClienteDto clienteDto)
        {
            if (id != clienteDto.Id)
                return BadRequest("O ID da rota não corresponde ao ID do corpo da requisição.");

            var command = new UpdateClienteCommand { ClienteDto = clienteDto };
            await _mediator.Send(command);

            return NoContent(); // Retorna 204 No Content, indicando sucesso sem corpo de resposta
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteClienteCommand { Id = id };
            await _mediator.Send(command);

            return NoContent(); // Retorna 204 No Content
        }
    }
}