using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueComemosAppV6;
using QueComemosV6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QueComemosV6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QueComemosContext _context;

        public HomeController(QueComemosContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var recetas = _context.Receta.ToList();
            var usuarios = _context.Usuarios.ToList();
            string reporte = "";

            foreach (Usuario usuario in usuarios)
            {
                reporte += usuario.Nombre;
                var puede = true;
                foreach (Receta receta in recetas)
                {
                    puede = VerSiPuede(usuario, receta);

                    if (puede)
                    {
                        reporte += " Puede hacer: " + receta.Nombre + ".\n";
                    }
                    else
                    {
                        reporte += "NO PUEDE : " + receta.Nombre + ".\n";
                    }
                }

            }

            ViewBag.reporte = reporte;

            return View();
        }

        private bool VerSiPuede(Usuario usuario, Receta receta)
        {
           var Alcanzan = false;

            ICollection<Ingrediente> ingredientesReceta = receta.Ingredientes;
            ICollection<IngredienteUsuario> ingredientesUsuario = usuario.MisIngredientes;

            foreach (Ingrediente i in ingredientesReceta)
            {
                foreach (IngredienteUsuario u in ingredientesUsuario)
                {
                    
                    Alcanzan = i.Nombre == u.Nombre && u.Cantidad >= i.Cantidad;
                    
                }
            }


            return Alcanzan;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
