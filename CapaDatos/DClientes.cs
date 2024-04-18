using CapaDatos.Core;
using CapaDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DClientes
    {
        private readonly UnitOfWork _unitOfWork;

        public DClientes()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<Clientes> ObtenerTodosLosClientes()
        {
            return _unitOfWork.Repository<Clientes>().Consulta().ToList();
        }

        public int Agregar(Clientes cliente)
        {
            _unitOfWork.Repository<Clientes>().Agregar(cliente);
            return _unitOfWork.Guardar();
        }

        public int Editar(Clientes cliente)
        {
            var clienteInDb = _unitOfWork.Repository<Clientes>().Consulta().FirstOrDefault(c => c.ClienteId == cliente.ClienteId);

            if (clienteInDb != null)
            {
                clienteInDb.Nombre = cliente.Nombre;
                clienteInDb.Apellido = cliente.Apellido;
                clienteInDb.CorreoElectronico = cliente.CorreoElectronico;
                clienteInDb.Telefono = cliente.Telefono;
                clienteInDb.Direccion = cliente.Direccion;
                clienteInDb.Estado = cliente.Estado;
                _unitOfWork.Repository<Clientes>().Editar(clienteInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }

        public int Eliminar(int clienteId)
        {
            var clienteInDb = _unitOfWork.Repository<Clientes>().Consulta().FirstOrDefault(c => c.ClienteId == clienteId);

            if (clienteInDb != null)
            {
                _unitOfWork.Repository<Clientes>().Eliminar(clienteInDb);
                return _unitOfWork.Guardar();
            }
            return 0;
        }
    }
}
