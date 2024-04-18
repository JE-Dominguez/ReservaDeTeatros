using CapaDatos;
using CapaDatos.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace Capa_Negocios
{
    public class NReservaciones
    {
        DReservaciones dReservaciones;

        public NReservaciones()
        {
            dReservaciones = new DReservaciones();
        }

        public List<Reservaciones> ObtenerTodasLasReservaciones()
        {
            return dReservaciones.ObtenerTodasLasReservaciones();
        }

        public int GuardarReservacion(Reservaciones reservacion)
        {
            if (reservacion.ReservacionId == 0)
            {
                return dReservaciones.Agregar(reservacion);
            }
            else
            {
                return dReservaciones.Editar(reservacion);
            }
        }

        public int EliminarReservacion(int reservacionId)
        {
            return dReservaciones.Eliminar(reservacionId);
        }
    }
}
