using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var recetas = _context.Receta.Include(x => x.Ingredientes).ToList();
            var usuarios = _context.Usuarios.Include(x => x.MisIngredientes).ToList();
            var reporte = new List<String>();
            
                foreach (Usuario usuario in usuarios)
            {
                reporte.Add(usuario.Nombre);
                var ingredientes = usuario.MisIngredientes;
                var cantidad = ingredientes.Count;
                var puede = true;
                foreach (Receta receta in recetas)
                {
                    puede = VerSiPuede(usuario, receta);

                    if (puede)
                    {
                        reporte.Add(" Puede hacer: " + receta.Nombre);
                    }
                    else
                    {
                        reporte.Add("NO PUEDE : " + receta.Nombre);
                    }
                }

            }

            ViewBag.reporte = reporte;

            return View();
        }

        private bool VerSiPuede(Usuario usuario, Receta receta)
        {

            var ingredientesReceta = receta.Ingredientes.ToList();
            var ingredientesUsuario = usuario.MisIngredientes.ToList();

            int idxR = 0;

            var NoSePuede = false;

            while (!NoSePuede && idxR < ingredientesReceta.Count)
            {
                Ingrediente ir = ingredientesReceta[idxR];
                int idxU = 0;
                var Tiene = false;
                var Alcanzan = false;
                while (!Alcanzan && idxU < ingredientesUsuario.Count && !Tiene)
                {
                    IngredienteUsuario iu = ingredientesUsuario[idxU];

                    Tiene = ir.Nombre == iu.Nombre;

                        if (Tiene)
                    {
                        Alcanzan = ir.Cantidad <= iu.Cantidad;
                    }
                   
                    idxU++;
                }

                if (!Tiene || !Alcanzan)
                {
                    NoSePuede = true;
                }

                idxR++;
            }

            return !NoSePuede;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> RecetasDelDia()
        {

            var recetas = _context.Receta.ToList();
            var RecetasDelDia = new List<String>();

            foreach (Receta receta in recetas)
                {
                RecetasDelDia.Add(receta.Nombre);
                }

            ViewBag.RecetasDelDia = RecetasDelDia;

            return View();
        }
    }
}
