using CrudProjeto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudProjeto.Data;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoricoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar todos os Historicos
        // GET: api/Historicos
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Historico>>> GetHistorico()
        {
            return await _context.Historico.ToListAsync();
        }

        // Mostrar Historico por num_conta
        // GET: api/Historicos/5
        [HttpGet("{cpf_cliente}/{num_conta}")]
        public async Task<ActionResult<Historico>> GetHistorico(string cpf_cliente, int num_conta)
        {
            var historico = await _context.Historico
                .FirstOrDefaultAsync(h => h.cpf_cliente == cpf_cliente && h.num_conta == num_conta);

            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        // Update the PostHistorico method to handle potential null values for cpf_cliente
        [HttpPost]
        public async Task<ActionResult<Historico>> PostHistorico(Historico historico)
        {
            // Valida se o cliente existe
            if (!_context.Cliente.Any(c => c.cpf_cliente == historico.cpf_cliente))
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Valida se a conta existe
            if (!_context.Conta.Any(c => c.num_conta == historico.num_conta))
            {
                return BadRequest("Conta não encontrada.");
            }

            _context.Historico.Add(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorico), new { historico.cpf_cliente, historico.num_conta }, historico);
        }

        // Atualizar Historico
        // PUT: api/Historicos/5
        [HttpPut("{cpf_cliente}/{num_conta}")]
        public async Task<IActionResult> PutHistorico(string cpf_cliente, int num_conta, Historico historico)
        {
            if (cpf_cliente != historico.cpf_cliente || num_conta != historico.num_conta)
            {
                return BadRequest();
            }

            _context.Entry(historico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoExists(cpf_cliente, num_conta))
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

        // Deletar Historico
        // DELETE: api/Historicos/5
        [HttpDelete("{cpf_cliente}/{num_conta}")]
        public async Task<IActionResult> DeleteHistorico(string cpf_cliente, int num_conta)
        {
            // Busca o histórico pela chave composta
            var historico = await _context.Historico
                .FirstOrDefaultAsync(h => h.cpf_cliente == cpf_cliente && h.num_conta == num_conta);

            if (historico == null)
            {
                return NotFound();
            }

            _context.Historico.Remove(historico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool HistoricoExists(string cpf_cliente, int num_conta)
        {
            return _context.Historico.Any(e => e.cpf_cliente == cpf_cliente && e.num_conta == num_conta);
        }
    }
}