using CrudPatrimonioEmpresarialJWT.Data;
using CrudPatrimonioEmpresarialJWT.Models.Marca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPatrimonioEmpresarialJWT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MarcaController(ApplicationDbContext context)
        {
            _context = context;
        }

       // [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaModel>>> GetMarcaAsync()
        {
            return await _context.Marca.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaModel>> GetMarcaAsync(int id)
        {
            var marca = await _context.Marca.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcaAsync(int id, MarcaModel marca)
        {
            if (id != marca.MarcaId)
            {
                return BadRequest();
            }

            _context.Entry(marca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(id))
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
        public async Task<ActionResult<MarcaModel>> PostMarcaAsync(MarcaModel marca)
        {
            _context.Marca.Add(marca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMarcaAsync), new { id = marca.MarcaId }, marca);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MarcaModel>> DeleteMarcaAsync(int id)
        {
            var marca = await _context.Marca.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marca.Remove(marca);
            await _context.SaveChangesAsync();

            return marca;
        }

        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.MarcaId == id);
        }
    }
}
