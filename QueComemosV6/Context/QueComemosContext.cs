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
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<IngredienteUsuario> MisIngredientes { get; set; }
        public DbSet<Receta> Receta { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}