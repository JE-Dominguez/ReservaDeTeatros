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
    public partial class VTeatros : Form
    {
        NTeatros nTeatrso;
        public VTeatros()
        {
            InitializeComponent();
            nTeatrso = new NTeatros();
        }
        private void Limpiar()
        {
            TxtID.Text = "";
            TxtNombres.Text = "";
            TxtCapacidad.Text = "";
            TxtdIRECCION.Text = "";
            ChkActivo.Checked = false;
            errorProvider1.Clear();
        }

        private void Cargar()
        {
            var datos = nTeatrso.ObtenerTodosLosTeatros();
            DgvDatos.DataSource = datos;
        }

        private void Guardar()
        {
            string teatrosId = TxtID.Text;
            string nombres = TxtNombres.Text;
            string capacidad = TxtCapacidad.Text;
            string direccion = TxtdIRECCION.Text;

            if (string.IsNullOrEmpty(nombres) || string.IsNullOrWhiteSpace(nombres))
            {
                errorProvider1.SetError(TxtNombres, "Debe ingresar el nombre del teatros");
                return;
            }

            if (string.IsNullOrEmpty(teatrosId) || string.IsNullOrWhiteSpace(teatrosId))
            {
                teatrosId = "0";
            }

            var teatros = new Teatros();
            if (int.TryParse(teatrosId, out int id))
            {
                teatros.TeatroId = id;
            }
            teatros.Nombre = nombres;
            teatros.CapacidadAsientos = int.Parse(capacidad);
            teatros.Direccion = direccion;
            teatros.Estado = ChkActivo.Checked;

            nTeatrso.GuardarTeatro(teatros);
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
                    if (DgvDatos.Rows[rowIndex].Cells["TeatroId"].Value is int id)
                    {
                        ConsultarPorId(id);
                    }
                }
            }
        }
        private void ConsultarPorId(int idteatros)
        {
            var teatros = nTeatrso.ObtenerTodosLosTeatros().FirstOrDefault(c => c.TeatroId == idteatros);
            if (teatros != null)
            {
                TxtID.Text = teatros.TeatroId.ToString();
                TxtNombres.Text = teatros.Nombre;
                TxtCapacidad.Text = teatros.CapacidadAsientos.ToString();
                TxtdIRECCION.Text = teatros.Direccion;
                ChkActivo.Checked = teatros.Estado;
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
                    if (DgvDatos.Rows[rowIndex].Cells["TeatroId"].Value is int id)
                    {
                        if (MessageBox.Show("¿Está seguro de eliminar este teatros?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            nTeatrso.EliminarTeatro(id);
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

        private void VTeatros_Load(object sender, EventArgs e)
        {
            Cargar();
        }
    }
}
