using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QueComemosV6.Models
{
    public class Receta
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo NOMBRE es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Tiempo de preparacion")]
        [Required(ErrorMessage = "El campo Tiempo de preparacion, es obligatorio")]
        public int TiempoPreparacion { get; set; }


        public virtual ICollection<Ingrediente> Ingredientes { get; set; }


        [EnumDataType(typeof(TipoReceta))]
        public TipoReceta Tipo { get; set; }

    }
}
