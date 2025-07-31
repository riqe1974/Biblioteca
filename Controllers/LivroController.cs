using Biblioteca.Data;
using Biblioteca.DTOs;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LivroController : ControllerBase
{
    private readonly AppDbContext _context;

    public LivroController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LivroDto>>> GetAll()
    {
        var livros = await _context.Livros
            .Select(l => new LivroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId
            }).ToListAsync();

        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LivroDto>> GetById(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        return Ok(new LivroDto
        {
            Id = livro.Id,
            Titulo = livro.Titulo,
            AutorId = livro.AutorId,
            GeneroId = livro.GeneroId
        });
    }

    [HttpPost]
    public async Task<ActionResult> Create(LivroDto dto)
    {
        if (!await _context.Autores.AnyAsync(a => a.Id == dto.AutorId))
            return BadRequest("Autor inválido");

        if (!await _context.Generos.AnyAsync(g => g.Id == dto.GeneroId))
            return BadRequest("Gênero inválido");

        var livro = new Livro
        {
            Titulo = dto.Titulo,
            AutorId = dto.AutorId,
            GeneroId = dto.GeneroId
        };

        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, LivroDto dto)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        livro.Titulo = dto.Titulo;
        livro.AutorId = dto.AutorId;
        livro.GeneroId = dto.GeneroId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return NotFound();

        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
