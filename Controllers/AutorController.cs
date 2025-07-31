using Biblioteca.Data;
using Biblioteca.DTOs;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AutorController : ControllerBase
{
    private readonly AppDbContext _context;

    public AutorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AutorDto>>> GetAll()
    {
        var autores = await _context.Autores
            .Select(a => new AutorDto { Id = a.Id, Nome = a.Nome })
            .ToListAsync();
        return Ok(autores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AutorDto>> GetById(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();
        return Ok(new AutorDto { Id = autor.Id, Nome = autor.Nome });
    }

    [HttpPost]
    public async Task<ActionResult> Create(AutorDto dto)
    {
        var autor = new Autor { Nome = dto.Nome };
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = autor.Id }, autor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, AutorDto dto)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();

        autor.Nome = dto.Nome;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();

        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
