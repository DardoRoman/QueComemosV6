using QueComemosAppV6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QueComemosV6.Models
{
    public class IngredienteUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo NOMBRE es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo CANTIDAD es obligatorio")]
        public int Cantidad { get; set; }


        [EnumDataType(typeof(TipoIngredienteUsuario))]
        public TipoIngrediente Tipo { get; set; }
    }
}
