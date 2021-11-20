using QueComemosV6.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QueComemosV6.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Display(Name = "Ingrese su nombre:")]
        [Required(ErrorMessage = "El campo NOMBRE es obligatorio")]
        public string Nombre { get; set; }

        public virtual ICollection<IngredienteUsuario> MisIngredientes { get; set; }

    }
}
