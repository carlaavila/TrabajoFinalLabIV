using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalLabIV.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Resumen { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Imagen Escudo")]
        public string? ImagenEscudo { get; set; }
      
        public string? Pais { get; set; }

        // Relación con la clase "Categoria"
        public Categoria? Categoria { get; set; }
        [Display(Name = "Categoria")]
        public int? CategoriaId { get; set; }

        // Relación uno a muchos con la clase "Jugador"
        public List<Jugador>? Jugadores { get; set; }
    }
}

