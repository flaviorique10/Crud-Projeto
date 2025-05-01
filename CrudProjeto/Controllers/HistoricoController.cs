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
        [HttpGet("{num_conta}")]
        public async Task<ActionResult<Historico>> GetHistorico(int num_conta)
        {
            var historico = await _context.Historico.FindAsync(num_conta);

            if (historico == null)
            {
                return NotFound();
            }

            return historico;
        }

        // Criar Historico
        // POST: api/Historicos
        [HttpPost]
        public async Task<ActionResult<Historico>> PostHistorico(Historico historico)
        {
            if (HistoricoExists(historico.num_conta))
            {
                return Conflict("Já existe uma agência com este número.");
            }

            _context.Historico.Add(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorico), new { num_conta = historico.num_conta }, historico);
        }

        // Atualizar Historico
        // PUT: api/Historicos/5
        [HttpPut("{num_conta}")]
        public async Task<IActionResult> PutHistorico(int num_conta, Historico historico)
        {
            if (num_conta != historico.num_conta)
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
                if (!HistoricoExists(num_conta))
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
        [HttpDelete("{num_conta}")]
        public async Task<IActionResult> DeleteHistorico(int num_conta)
        {
            var historico = await _context.Historico.FindAsync(num_conta);
            if (historico == null)
            {
                return NotFound();
            }

            _context.Historico.Remove(historico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool HistoricoExists(int num_conta)
        {
            return _context.Historico.Any(e => e.num_conta == num_conta);
        }
    }
}