using CapaDatos.Core;
using CapaDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DTeatros
    {
        private readonly UnitOfWork _unitOfWork;

        public DTeatros()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Teatros> ObtenerTodosLosTeatros()
        {
            return _unitOfWork.Repository<Teatros>().Consulta().ToList();
        }

        public int Agregar(Teatros teatro)
        {
            _unitOfWork.Repository<Teatros>().Agregar(teatro);
            return _unitOfWork.Guardar();
        }

        public int Editar(Teatros teatro)
        {
            var teatroInDb = _unitOfWork.Repository<Teatros>().Consulta().FirstOrDefault(t => t.TeatroId == teatro.TeatroId);

            if (teatroInDb != null)
            {
                teatroInDb.Nombre = teatro.Nombre;
                teatroInDb.Direccion = teatro.Direccion;
                teatroInDb.CapacidadAsientos = teatro.CapacidadAsientos;
                teatroInDb.Estado = teatro.Estado;
                _unitOfWork.Repository<Teatros>().Editar(teatroInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }

        public int Eliminar(int teatroId)
        {
            var teatroInDb = _unitOfWork.Repository<Teatros>().Consulta().FirstOrDefault(t => t.TeatroId == teatroId);

            if (teatroInDb != null)
            {
                _unitOfWork.Repository<Teatros>().Eliminar(teatroInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }
    }
}
