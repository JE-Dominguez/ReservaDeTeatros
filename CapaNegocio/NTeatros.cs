
using CapaDatos;
using CapaDatos.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace Capa_Negocios
{
    public class NTeatros
    {
        DTeatros dTeatros;

        public NTeatros()
        {
            dTeatros = new DTeatros();
        }

        public List<Teatros> ObtenerTodosLosTeatros()
        {
            return dTeatros.ObtenerTodosLosTeatros();
        }

        public int GuardarTeatro(Teatros teatro)
        {
            if (teatro.TeatroId == 0)
            {
                return dTeatros.Agregar(teatro);
            }
            else
            {
                return dTeatros.Editar(teatro);
            }
        }

        public int EliminarTeatro(int teatroId)
        {
            return dTeatros.Eliminar(teatroId);
        }
    }
}
