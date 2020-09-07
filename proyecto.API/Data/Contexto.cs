using Microsoft.EntityFrameworkCore;
using proyecto.API.Models;

namespace proyecto.API.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options){ }

        public DbSet<Valores> Valores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}