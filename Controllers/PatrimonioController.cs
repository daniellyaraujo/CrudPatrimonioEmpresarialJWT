using CrudPatrimonioEmpresarialJWT.Data;
using CrudPatrimonioEmpresarialJWT.Models.Patrimonio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPatrimonioEmpresarialJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatrimonioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatrimonioModel>>> GetPatrimonioAsync()
        {
            return await _context.Patrimonio.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatrimonioModel>> GetPatrimonioAsync(int id)
        {
            var patrimonio = await _context.Patrimonio.FindAsync(id);

            if (patrimonio == null)
            {
                return NotFound();
            }

            return patrimonio;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatrimonioAsync(int id, PatrimonioModel patrimonio)
        {
            if (id != patrimonio.MarcaId)
            {
                return BadRequest();
            }

            _context.Entry(patrimonio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatrimonioExists(id))
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

        [HttpPost]
        public async Task<ActionResult<PatrimonioModel>> PostPatrimonioAsync(PatrimonioModel patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatrimonio", new { id = patrimonio.MarcaId }, patrimonio);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatrimonioModel>> DeletePatrimonioAsync(int id)
        {
            var patrimonio = await _context.Patrimonio.FindAsync(id);
            if (patrimonio == null)
            {
                return NotFound();
            }

            _context.Patrimonio.Remove(patrimonio);
            await _context.SaveChangesAsync();

            return patrimonio;
        }

        private bool PatrimonioExists(int id)
        {
            return _context.Patrimonio.Any(e => e.MarcaId == id);
        }
    }
}
