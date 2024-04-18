using CapaDatos;
using CapaDatos.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace Capa_Negocios
{
    public class NClientes
    {
        DClientes dClientes;

        public NClientes()
        {
            dClientes = new DClientes();
        }

        public List<Clientes> ObtenerTodosLosClientes()
        {
            return dClientes.ObtenerTodosLosClientes();
        }


        public int GuardarCliente(Clientes cliente)
        {
            if (cliente.ClienteId == 0)
            {
                return dClientes.Agregar(cliente);
            }
            else
            {
                return dClientes.Editar(cliente);
            }
        }

        public int EliminarCliente(int clienteId)
        {
            return dClientes.Eliminar(clienteId);
        }
    }
}
