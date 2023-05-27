using Microsoft.AspNetCore.Mvc;

namespace PrevClientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Lógica para buscar os clientes
            return Ok("Lista de clientes");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Lógica para buscar um cliente específico pelo ID
            return Ok($"Cliente com ID {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            // Lógica para adicionar um novo cliente
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente cliente)
        {
            // Lógica para atualizar um cliente existente
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Lógica para excluir um cliente existente
            return NoContent();
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // Outras propriedades do cliente...
    }
}

