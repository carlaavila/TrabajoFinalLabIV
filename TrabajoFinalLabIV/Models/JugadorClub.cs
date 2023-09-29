namespace TrabajoFinalLabIV.Models
{
	public class JugadorClub
	{
		public int Id { get; set; }
		public int JugadorId { get; set; }
		public int ClubId { get; set; }
		public Jugador Jugador { get; set; }
		public Club Club { get; set; }
	}
}
