using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Modelos
{
    public class Reservaciones
    {
        [Key]
        public int ReservacionId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Clientes Clientes { get; set; }
        [Required]
        public int TeatroId { get; set; }

        [ForeignKey("TeatroId")]

        public virtual Teatros Teatros { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int CantidadEntradas { get; set; }
        [Required]
        public decimal PrecioTotal { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
