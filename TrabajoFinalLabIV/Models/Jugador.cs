namespace TrabajoFinalLabIV.Models
{
	public class Jugador
	{
		public int Id { get; set; }
		public string Apellido { get; set; }
		public string Nombres { get; set; }
		public string Biografia { get; set; }
		public string? Foto { get; set; }

		// Propiedad de navegación para el club al que pertenece el jugador
		public Club? Club { get; set; }
		public int? ClubId { get; set; }
	}
}
