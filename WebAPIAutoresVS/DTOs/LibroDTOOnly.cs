namespace WebAPIAutoresVS.DTOs
{
    public class LibroDTOOnly
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<AutorDTO> Autores { get; set; }
    }
}
