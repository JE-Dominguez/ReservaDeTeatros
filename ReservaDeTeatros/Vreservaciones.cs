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
    public partial class Vreservaciones : Form
    {
        NReservaciones nReservaciones;
        NClientes nClientes;
        NTeatros nTeatros;
        public Vreservaciones()
        {
            InitializeComponent();
            nReservaciones = new NReservaciones();
            nClientes = new NClientes();
            nTeatros = new NTeatros();
        }
        private void Limpiar()
        {
            TxtID.Text = "";
            txtprecio.Text = "";
            TxtCapacidad.Text = "";
            cmbcliente.SelectedValue = -1;
            cmbteatros.SelectedValue = -1;
            ChkActivo.Checked = false;
            errorProvider1.Clear();
        }

        private void Cargar()
        {
            var datos = nReservaciones.ObtenerTodasLasReservaciones();
            DgvDatos.DataSource = datos;
        }

        private void Guardar()
        {
            string reservaID = TxtID.Text;
            string PRECIO = txtprecio.Text;
            string capacidad = TxtCapacidad.Text;
            int cliente = Convert.ToInt32(cmbcliente.SelectedValue);
            int teatros = Convert.ToInt32(cmbteatros.SelectedValue);

            if (string.IsNullOrEmpty(PRECIO) || string.IsNullOrWhiteSpace(PRECIO))
            {
                errorProvider1.SetError(txtprecio, "Debe ingresar el precio de la reserva");
                return;
            }

            if (string.IsNullOrEmpty(reservaID) || string.IsNullOrWhiteSpace(reservaID))
            {
                reservaID = "0";
            }

            var reserva = new Reservaciones();
            if (int.TryParse(reservaID, out int id))
            {
                reserva.ReservacionId = id;
            }
            reserva.CantidadEntradas = int.Parse(capacidad);
            reserva.PrecioTotal = decimal.Parse(PRECIO);
            reserva.ClienteId = cliente;
            reserva.TeatroId = teatros;
            reserva.Fecha = DtpFecha.Value;
            reserva.Estado = ChkActivo.Checked;

            nReservaciones.GuardarReservacion(reserva);
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
                    if (DgvDatos.Rows[rowIndex].Cells["ReservacionId"].Value is int id)
                    {
                        ConsultarPorId(id);
                    }
                }
            }
        }
        private void ConsultarPorId(int ReservaId)
        {
            var Reserva = nReservaciones.ObtenerTodasLasReservaciones().FirstOrDefault(c => c.ReservacionId == ReservaId);
            if (Reserva != null)
            {
                TxtID.Text = Reserva.ReservacionId.ToString();
                txtprecio.Text = Reserva.PrecioTotal.ToString();
                TxtCapacidad.Text = Reserva.CantidadEntradas.ToString();
                cmbcliente.SelectedValue = Reserva.ClienteId.ToString();
                cmbteatros.SelectedValue = Reserva.TeatroId.ToString();
                ChkActivo.Checked = Reserva.Estado;
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
                    if (DgvDatos.Rows[rowIndex].Cells["ReservacionId"].Value is int id)
                    {
                        if (MessageBox.Show("¿Está seguro de eliminar este Reservacion?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            nReservaciones.EliminarReservacion(id);
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

        private void Vreservaciones_Load(object sender, EventArgs e)
        {
            Cargar();
            Combo();
            Otros();
        }
        private void Otros()
        {
            cmbcliente.SelectedValue = -1;
            cmbteatros.SelectedValue = -1;
            DtpFecha.Value = DateTime.Now;
        }
        private void Combo()
        {
            try
            {
                var clientes = nClientes.ObtenerTodosLosClientes().Select(m => new { m.ClienteId, Nombre = m.Nombre + " " + m.Apellido }).ToList();
                var teatros = nTeatros.ObtenerTodosLosTeatros().Select(m => new { m.TeatroId, m.Nombre }).ToList();

                if (clientes != null && clientes.Any() && teatros != null && teatros.Any())
                {
                    cmbcliente.DataSource = clientes;
                    cmbcliente.DisplayMember = "Nombre";
                    cmbcliente.ValueMember = "ClienteId";

                    cmbteatros.DataSource = teatros;
                    cmbteatros.DisplayMember = "Nombre";
                    cmbteatros.ValueMember = "TeatroId";
                }
                else
                {
                    MessageBox.Show("No hay clientes o te diatros sponibles. Debe agregar clientes y te diatros .");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al cargar los Medicos o pacientes: {ex.Message}");
            }
        }
    }
}
