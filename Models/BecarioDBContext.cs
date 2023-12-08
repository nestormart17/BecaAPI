using BecarioAPI.Models.Estados;
using BecarioAPI.Models.Solicitantes;
using BecarioAPI.Models.Solicitudes;
using BecarioAPI.Models.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models
{
    public class BecarioDBContext : DbContext
    {
        public BecarioDBContext(DbContextOptions<BecarioDBContext> options) : base(options)
        {
        }

        //Agregamos las tablas a la base de datos
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //Agregamos los datos iniciales a la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Agregamos los datos iniciales a la tabla Estados
            modelBuilder.Entity<Estado>().HasData(
            new Estado { IdEstado = 1, Descripcion = "Recibida" },
            new Estado { IdEstado = 2, Descripcion = "En Progreso" },
            new Estado { IdEstado = 3, Descripcion = "Rechazado" },
            new Estado { IdEstado = 4, Descripcion = "Aprobado" },
            new Estado { IdEstado = 5, Descripcion = "Finalizado" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
