using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using API_Lucy.Databaze;
using API_Lucy.Modelz;

namespace API_Lucy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PegawaiCamatController : ControllerBase
    {
        private readonly DataContext _context;

        public PegawaiCamatController(DataContext context)
        {
            _context = context;
        }

        // POST: api/PegawaiCamat
        [HttpPost]
        public async Task<ActionResult<Pegawai>> PostPegawai(Pegawai pegawai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pegawais.Add(pegawai);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPegawaiById), new { id = pegawai.Id }, pegawai);
        }

        // GET: api/PegawaiCamat/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pegawai>> GetPegawaiById(Guid id)
        {
            var pegawai = await _context.Pegawais.FindAsync(id);

            if (pegawai == null)
            {
                return NotFound();
            }

            return pegawai;
        }

        // GET: api/PegawaiCamat/Detail/{id}
        [HttpGet("Detail/{id:guid}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var data = await _context.Pegawais.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/PegawaiCamat/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPegawai(Guid id, Pegawai pegawai)
        {
            if (id != pegawai.Id)
            {
                return BadRequest();
            }

            var existingPegawai = await _context.Pegawais.FindAsync(id);
            if (existingPegawai == null)
            {
                return NotFound();
            }

            existingPegawai.Nama = pegawai.Nama;
            existingPegawai.NIP = pegawai.NIP;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PegawaiExists(id))
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

        // DELETE: api/PegawaiCamat/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePegawai(Guid id)
        {
            var pegawai = await _context.Pegawais.FindAsync(id);
            if (pegawai == null)
            {
                return NotFound();
            }

            _context.Pegawais.Remove(pegawai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PegawaiExists(Guid id)
        {
            return _context.Pegawais.Any(e => e.Id == id);
        }
    }
}
