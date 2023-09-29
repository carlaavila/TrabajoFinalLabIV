using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalLabIV.Models;

namespace TrabajoFinalLabIV.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<JugadorClub>().HasKey(x => new { x.JugadorId, x.ClubId });

			modelBuilder.Entity<IdentityUser>().ToTable("Usuarios", "Seguridad");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Seguridad");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuariosRoles", "Seguridad");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims", "Seguridad");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuariosClaims", "Seguridad");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuariosLogin", "Seguridad");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuariosToken", "Seguridad");
		}

		public DbSet<Club> Clubes { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Jugador> Jugadores { get; set; }
		public DbSet<JugadorClub> JugadoresClubes { get; set; }
	}
}