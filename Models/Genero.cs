namespace Biblioteca.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Livro>? Livros { get; set; }
    }
}
