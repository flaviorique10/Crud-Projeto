using CrudProjeto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudProjeto.Data;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar todos os Clientes
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        // Mostrar Cliente por cpf_cliente
        // GET: api/Clientes/5
        [HttpGet("{cpf_cliente}")]
        public async Task<ActionResult<Cliente>> GetCliente(string cpf_cliente)
        {
            var cliente = await _context.Cliente.FindAsync(cpf_cliente);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // Criar Cliente
        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (ClienteExists(cliente.cpf_cliente))
            {
                return Conflict("Já existe um cliente com este CPF.");
            }

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { cpf_cliente = cliente.cpf_cliente }, cliente);
        }

        // Atualizar Cliente
        // PUT: api/Clientes/5
        [HttpPut("{cpf_cliente}")]
        public async Task<IActionResult> PutCliente(string cpf_cliente, Cliente cliente)
        {
            if (cpf_cliente != cliente.cpf_cliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cpf_cliente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Deletar Cliente
        // DELETE: api/Clientes/5
        [HttpDelete("{cpf_cliente}")]
        public async Task<IActionResult> DeleteCliente(string cpf_cliente)
        {
            var cliente = await _context.Cliente.FindAsync(cpf_cliente);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool ClienteExists(string ? cpf_cliente)
        {
            return _context.Cliente.Any(e => e.cpf_cliente == cpf_cliente);
        }
    }
}