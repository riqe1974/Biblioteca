using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Autor> Autores => Set<Autor>();
        public DbSet<Genero> Generos => Set<Genero>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AutorId);

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Genero)
                .WithMany(g => g.Livros)
                .HasForeignKey(l => l.GeneroId);
        }
    }
}
