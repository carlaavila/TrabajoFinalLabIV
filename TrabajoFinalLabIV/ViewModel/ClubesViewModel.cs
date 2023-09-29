using Microsoft.AspNetCore.Mvc.Rendering;
using TrabajoFinalLabIV.Models;

namespace TrabajoFinalLabIV.ViewModel
{
	public class ClubesViewModel
	{
		public List<Club> Clubes { get; set; }
		public SelectList? ListCategorias { get; set; }
		public int? CategoriaId { get; set; }
		public string? Nombre { get; set; }
		public string? Pais { get; set; }
		public PaginadorViewModel Paginador { get; set; }
	}

	
}
