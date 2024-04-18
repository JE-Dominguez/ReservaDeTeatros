using CapaDatos.Core;
using CapaDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DReservaciones
    {
        private readonly UnitOfWork _unitOfWork;

        public DReservaciones()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Reservaciones> ObtenerTodasLasReservaciones()
        {
            return _unitOfWork.Repository<Reservaciones>().Consulta().ToList();
        }

        public int Agregar(Reservaciones reservacion)
        {
            _unitOfWork.Repository<Reservaciones>().Agregar(reservacion);
            return _unitOfWork.Guardar();
        }

        public int Editar(Reservaciones reservacion)
        {
            var reservacionInDb = _unitOfWork.Repository<Reservaciones>().Consulta().FirstOrDefault(r => r.ReservacionId == reservacion.ReservacionId);

            if (reservacionInDb != null)
            {
                reservacionInDb.ClienteId = reservacion.ClienteId;
                reservacionInDb.TeatroId = reservacion.TeatroId;
                reservacionInDb.Fecha = reservacion.Fecha;
                reservacionInDb.CantidadEntradas = reservacion.CantidadEntradas;
                reservacionInDb.PrecioTotal = reservacion.PrecioTotal;
                reservacionInDb.Estado = reservacion.Estado;
                _unitOfWork.Repository<Reservaciones>().Editar(reservacionInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }

        public int Eliminar(int reservacionId)
        {
            var reservacionInDb = _unitOfWork.Repository<Reservaciones>().Consulta().FirstOrDefault(r => r.ReservacionId == reservacionId);

            if (reservacionInDb != null)
            {
                _unitOfWork.Repository<Reservaciones>().Eliminar(reservacionInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }
    }
}
