using CrudProjeto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudProjeto.Data;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar todos os Contas
        // GET: api/Contas
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Conta>>> GetConta()
        {
            return await _context.Conta.ToListAsync();
        }

        // Mostrar Conta por num_conta
        // GET: api/Contas/5
        [HttpGet("{num_conta}")]
        public async Task<ActionResult<Conta>> GetConta(int num_conta)
        {
            var conta = await _context.Conta.FindAsync(num_conta);

            if (conta == null)
            {
                return NotFound();
            }

            return conta;
        }

        // Criar Conta
        // POST: api/Contas
        [HttpPost]
        public async Task<ActionResult<Conta>> PostConta(Conta conta)
        {
            if (ContaExists(conta.num_conta))
            {
                return Conflict("Já existe uma agência com este número.");
            }

            _context.Conta.Add(conta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConta), new { num_conta = conta.num_conta }, conta);
        }

        // Atualizar Conta
        // PUT: api/Contas/5
        [HttpPut("{num_conta}")]
        public async Task<IActionResult> PutConta(int num_conta, Conta conta)
        {
            if (num_conta != conta.num_conta)
            {
                return BadRequest();
            }

            _context.Entry(conta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaExists(num_conta))
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

        // Deletar Conta
        // DELETE: api/Contas/5
        [HttpDelete("{num_conta}")]
        public async Task<IActionResult> DeleteConta(int num_conta)
        {
            var conta = await _context.Conta.FindAsync(num_conta);
            if (conta == null)
            {
                return NotFound();
            }

            _context.Conta.Remove(conta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool ContaExists(int num_conta)
        {
            return _context.Conta.Any(e => e.num_conta == num_conta);
        }
    }
}