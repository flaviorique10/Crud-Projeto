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
        [HttpGet("{num_conta}")]
        public async Task<ActionResult<Investimento>> GetInvestimento(int num_conta)
        {
            var investimento = await _context.Investimento.FindAsync(num_conta);

            if (investimento == null)
            {
                return NotFound();
            }

            return investimento;
        }

        // Criar Investimento
        // POST: api/Investimentos
        [HttpPost]
        public async Task<ActionResult<Investimento>> PostInvestimento(Investimento investimento)
        {
            if (InvestimentoExists(investimento.num_conta))
            {
                return Conflict("Já existe uma agência com este número.");
            }

            _context.Investimento.Add(investimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestimento), new { num_conta = investimento.num_conta }, investimento);
        }

        // Atualizar Investimento
        // PUT: api/Investimentos/5
        [HttpPut("{num_conta}")]
        public async Task<IActionResult> PutInvestimento(int num_conta, Investimento investimento)
        {
            if (num_conta != investimento.num_conta)
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
                if (!InvestimentoExists(num_conta))
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
        [HttpDelete("{num_conta}")]
        public async Task<IActionResult> DeleteInvestimento(int num_conta)
        {
            var investimento = await _context.Investimento.FindAsync(num_conta);
            if (investimento == null)
            {
                return NotFound();
            }

            _context.Investimento.Remove(investimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool InvestimentoExists(int num_conta)
        {
            return _context.Investimento.Any(e => e.num_conta == num_conta);
        }
    }
}