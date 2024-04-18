using Capa_Negocios;
using CapaDatos.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReservaDeTeatros
{
    public partial class V_Clientes : Form
    {

        NClientes nClientes;
        public V_Clientes()
        {
            InitializeComponent();
            nClientes = new NClientes();
        }
        private void Limpiar()
        {
            TxtID.Text = "";
            TxtNombres.Text = "";
            TxtApellidos.Text = "";
            TxtCorrreo.Text = "";
            TxtTelefono.Text = "";
            TxtdIRECCION.Text = "";
            ChkActivo.Checked = false;
            errorProvider1.Clear();
        }

        private void Cargar()
        {
            var datos = nClientes.ObtenerTodosLosClientes();
            DgvDatos.DataSource = datos;
        }

        private void Guardar()
        {
            string clienteId = TxtID.Text;
            string nombres = TxtNombres.Text;
            string apellidos = TxtApellidos.Text;
            string correo = TxtCorrreo.Text;
            string telefono = TxtTelefono.Text;
            string direccion = TxtdIRECCION.Text;

            if (string.IsNullOrEmpty(nombres) || string.IsNullOrWhiteSpace(nombres))
            {
                errorProvider1.SetError(TxtNombres, "Debe ingresar el nombre del cliente");
                return;
            }

            if (string.IsNullOrEmpty(clienteId) || string.IsNullOrWhiteSpace(clienteId))
            {
                clienteId = "0";
            }

            var cliente = new Clientes();
            if (int.TryParse(clienteId, out int id))
            {
                cliente.ClienteId = id;
            }
            cliente.Nombre = nombres;
            cliente.Apellido = apellidos;
            cliente.CorreoElectronico = correo;
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
            cliente.Estado = ChkActivo.Checked;

            nClientes.GuardarCliente(cliente);
            Cargar();
            Limpiar();
        }

        private void Editar()
        {
            if (DgvDatos.SelectedCells.Count > 0)
            {
                int rowIndex = DgvDatos.SelectedCells[0].RowIndex;
                DataGridViewCheckBoxCell checkBoxCell = DgvDatos.Rows[rowIndex].Cells["Seleccion"] as DataGridViewCheckBoxCell;

                if (checkBoxCell?.Value is true)
                {
                    if (DgvDatos.Rows[rowIndex].Cells["ClienteId"].Value is int id)
                    {
                        ConsultarPorId(id);
                    }
                }
            }
        }
        private void ConsultarPorId(int idCliente)
        {
            var cliente = nClientes.ObtenerTodosLosClientes().FirstOrDefault(c => c.ClienteId == idCliente);
            if (cliente != null)
            {
                TxtID.Text = cliente.ClienteId.ToString();
                TxtNombres.Text = cliente.Nombre;
                TxtApellidos.Text = cliente.Apellido;
                TxtCorrreo.Text = cliente.CorreoElectronico;
                TxtdIRECCION.Text = cliente.Direccion;
                TxtTelefono.Text = cliente.Telefono;
                ChkActivo.Checked = cliente.Estado;
            }
        }
        private void Eliminar()
        {
            if (DgvDatos.SelectedCells.Count > 0)
            {
                int rowIndex = DgvDatos.SelectedCells[0].RowIndex;
                DataGridViewCheckBoxCell checkBoxCell = DgvDatos.Rows[rowIndex].Cells["Seleccion"] as DataGridViewCheckBoxCell;

                if (checkBoxCell?.Value is true)
                {
                    if (DgvDatos.Rows[rowIndex].Cells["ClienteId"].Value is int id)
                    {
                        if (MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            nClientes.EliminarCliente(id);
                            Cargar();
                            Limpiar();
                        }
                    }
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void V_Clientes_Load(object sender, EventArgs e)
        {
            Cargar();
        }
    }
}
