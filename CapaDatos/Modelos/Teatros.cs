using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Modelos
{
    public class Teatros
    {
        [Key]
        public int TeatroId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Direccion { get; set; }
        [Required]
        public int CapacidadAsientos { get; set; }

        [Required]
        public bool Estado { get; set; }
    }

}
