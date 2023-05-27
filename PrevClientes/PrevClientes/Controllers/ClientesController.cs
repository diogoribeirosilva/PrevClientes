using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Queries;
using PrevClientes.Application.Queries.Clientes;
using System;
using System.Threading.Tasks;

namespace PrevClientes.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            try
            {
                var query = new ObterTodosClientesQuery();
                var clientes = await _mediator.Send(query);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var command = new CriarClienteCommand(clienteDTO);
                await _mediator.Send(command);

                return Ok("Cliente Cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar cliente: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            try
            {
                var query = new ObterClientePorIdQuery(id); 
                var cliente = await _mediator.Send<ClienteDTO>(query); 

                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter cliente: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var command = new AtualizarClienteCommand { ClienteId = id, ClienteDTO = clienteDTO };
                var result = await _mediator.Send(command);

                if (result)
                    return Ok("Cliente Atualizado com sucesso!");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new RemoverClienteCommand { ClienteId = id };
                var result = await _mediator.Send(command);

                if (result)
                    return Ok("Cliente Removido com sucesso!");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover cliente: {ex.Message}");
            }
        }

    }
}
