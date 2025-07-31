namespace Biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
    }
}
