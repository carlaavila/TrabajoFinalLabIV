namespace TrabajoFinalLabIV.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Propiedad de navegación a Clubes
        public List<Club>? Clubes { get; set; }
    }
}