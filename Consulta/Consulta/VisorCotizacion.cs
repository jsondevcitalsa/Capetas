using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp.BO;
//
using Ice.Core;
using Erp.Adapters;
using Erp.Proxy.BO;
using Ice.Lib.Framework;
using Epicor.ServiceModel.Channels;

namespace Consulta
{
    public partial class VisorCotizacion : Form
    {
        Session sess;
        public VisorCotizacion()
        {
            InitializeComponent();
            Datos();
        }
        public void Datos()
        {
            sess = new Session(config.Configuracion.Default.usuario, config.Configuracion.Default.contraseña, config.Configuracion.Default.servidor, Session.LicenseType.Default, config.Configuracion.Default.ruta);
        }
        internal Session iniciar(string ID, string Pass, string Server, string patch)
        {
            Session sess = new Session(ID, Pass, Server, Ice.Core.Session.LicenseType.Default, patch);
            return sess;
        }
        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void VisorCotizacion_Load(object sender, EventArgs e)
        {
            if (sess != null)
            {

                MessageBox.Show("Conexion exitosa");
            }
            else

            {
                MessageBox.Show("conexion fallida");
            }
        }
    }
}
