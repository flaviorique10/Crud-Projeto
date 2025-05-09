using CrudProjeto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudProjeto.Data;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvestimentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar todos os Investimentos
        // GET: api/Investimentos
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Investimento>>> GetInvestimento()
        {
            return await _context.Investimento.ToListAsync();
        }

        // Mostrar Investimento por num_conta
        // GET: api/Investimentos/5
        [HttpGet("{cpf_cliente}/{num_conta}")]
        public async Task<ActionResult<Investimento>> GetInvestimento(string cpf_cliente, int num_conta)
        {
            var investimento = await _context.Investimento
                .FirstOrDefaultAsync(i => i.cpf_cliente == cpf_cliente && i.num_conta == num_conta);

            if (investimento == null)
            {
                return NotFound();
            }

            return investimento;
        }

        // Update the PostInvestimento method to handle potential null values for cpf_cliente.
        [HttpPost]
        public async Task<ActionResult<Investimento>> PostInvestimento(Investimento investimento)
        {
            if (string.IsNullOrEmpty(investimento.cpf_cliente))
            {
                return BadRequest("O campo 'cpf_cliente' não pode ser nulo ou vazio.");
            }

            if (InvestimentoExists(investimento.cpf_cliente, investimento.num_conta))
            {
                return Conflict("Já existe um investimento para este cliente e conta.");
            }

            _context.Investimento.Add(investimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInvestimento),
                new { cpf_cliente = investimento.cpf_cliente, num_conta = investimento.num_conta },
                investimento
            );
        }

        // Atualizar Investimento
        // PUT: api/Investimentos/5
        [HttpPut("{cpf_cliente}/{num_conta}")]
        public async Task<IActionResult> PutInvestimento(string cpf_cliente, int num_conta, Investimento investimento)
        {
            if (cpf_cliente != investimento.cpf_cliente || num_conta != investimento.num_conta)
            {
                return BadRequest();
            }

            _context.Entry(investimento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestimentoExists(cpf_cliente, num_conta))
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

        // Deletar Investimento
        // DELETE: api/Investimentos/5
        [HttpDelete("{cpf_cliente}/{num_conta}")]
        public async Task<IActionResult> DeleteInvestimento(string cpf_cliente, int num_conta)
        {
            var investimento = await _context.Investimento
                .FirstOrDefaultAsync(i => i.cpf_cliente == cpf_cliente && i.num_conta == num_conta);

            if (investimento == null)
            {
                return NotFound();
            }

            _context.Investimento.Remove(investimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool InvestimentoExists(string cpf_cliente, int num_conta)
        {
            return _context.Investimento
                .Any(e => e.cpf_cliente == cpf_cliente && e.num_conta == num_conta);
        }
    }
}