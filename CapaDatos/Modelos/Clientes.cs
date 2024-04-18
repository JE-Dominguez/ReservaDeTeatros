using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Modelos
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(150)]
        public string CorreoElectronico { get; set; }
        [Required]
        [MaxLength(100)]
        public string Telefono { get; set; }
        [Required]
        [MaxLength(100)]
        public string Direccion { get; set; }

        [Required]
        public bool Estado { get; set; }
    }

}
