namespace Biblioteca.DTOs
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}
