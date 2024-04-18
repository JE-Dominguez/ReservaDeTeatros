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
    public partial class V_Menu : Form
    {
        AbrirEnMenu af;
        public V_Menu()
        {
            InitializeComponent();
            af = new AbrirEnMenu();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            af.Abrir(new V_Clientes(), PnlContenedor, true);
        }

        private void BtnTeatros_Click(object sender, EventArgs e)
        {
            af.Abrir(new VTeatros(), PnlContenedor, true);
        }

        private void BtnReservaciones_Click(object sender, EventArgs e)
        {
            af.Abrir(new Vreservaciones(), PnlContenedor, true);
        }

        private void PtbLogo_Click(object sender, EventArgs e)
        {
            af.Abrir(new Vinicio(), PnlContenedor, true);
        }

        private void V_Menu_Load(object sender, EventArgs e)
        {
            af.Abrir(new Vinicio(), PnlContenedor, true);
        }
    }
}
