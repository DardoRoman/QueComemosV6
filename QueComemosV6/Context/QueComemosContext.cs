using Microsoft.EntityFrameworkCore;
using QueComemosV6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QueComemosV6.Models;

namespace QueComemosAppV6
{
    public class QueComemosContext : DbContext
    {
        public QueComemosContext(DbContextOptions<QueComemosContext> options) : base(options)
        {
        }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<IngredienteUsuario> MisIngredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingrediente>()
                .HasOne(_ => _.Receta)
                .WithMany(_ => _.Ingredientes)
                .HasForeignKey(_ => _.RecetaId);

            modelBuilder.Entity<IngredienteUsuario>()
                .HasOne(_ => _.Usuario)
                .WithMany(_ => _.MisIngredientes)
                .HasForeignKey(_ => _.UsuarioId);
        }

    }
}