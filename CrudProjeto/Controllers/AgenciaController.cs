using CrudProjeto.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudProjeto.Data;

namespace CrudProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciaController : ControllerBase 
    {
        private readonly ApplicationDbContext _context;

        public AgenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar todos os Agencias
        // GET: api/Agencias
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Agencia>>> GetAgencia()
        {
            return await _context.Agencia.ToListAsync();
        }

        // Mostrar Agencia por num_agencia
        // GET: api/Agencias/5
        [HttpGet("{num_agencia}")]
        public async Task<ActionResult<Agencia>> GetAgencia(int num_agencia)
        {
            var agencia = await _context.Agencia.FindAsync(num_agencia);

            if (agencia == null)
            {
                return NotFound();
            }

            return agencia;
        }

        // Criar Agencia
        // POST: api/Agencias
        [HttpPost]
        public async Task<ActionResult<Agencia>> PostAgencia(Agencia agencia)
        {
            if (AgenciaExists(agencia.num_agencia))
            {
                return Conflict("Já existe uma agência com este número.");
            }

            _context.Agencia.Add(agencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgencia), new { num_agencia = agencia.num_agencia }, agencia);
        }

        // Atualizar Agencia
        // PUT: api/Agencias/5
        [HttpPut("{num_agencia}")]
        public async Task<IActionResult> PutAgencia(int num_agencia, Agencia agencia)
        {
            if (num_agencia != agencia.num_agencia)
            {
                return BadRequest();
            }

            _context.Entry(agencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenciaExists(num_agencia))
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

        // Deletar Agencia
        // DELETE: api/Agencias/5
        [HttpDelete("{num_agencia}")]
        public async Task<IActionResult> DeleteAgencia(int num_agencia)
        {
            var agencia = await _context.Agencia.FindAsync(num_agencia);
            if (agencia == null)
            {
                return NotFound();
            }

            _context.Agencia.Remove(agencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verificar se o Cliente existe
        private bool AgenciaExists(int num_agencia)
        {
            return _context.Agencia.Any(e => e.num_agencia == num_agencia);
        }
    }
}