using Biblioteca.Models;
using Biblioteca.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GeneroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var generos = await _context.Generos.ToListAsync();
            return Ok(generos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = genero.Id }, genero);
        }
    }
}