using Ice.Core;
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
using Erp.Adapters;
using Erp.Proxy.BO;

namespace Consulta
{
    public partial class Form3 : Form
    {
        Session sess;
        public Form3()
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

        private void Form3_Load(object sender, EventArgs e)
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

        private void btnCon_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dTInfoFac = new DataTable();
                dTInfoFac.Columns.Add("Plant", typeof(string));
                dTInfoFac.Columns.Add("CantFacturas", typeof(int));
                dTInfoFac.Columns.Add("ToatalVendido", typeof(decimal));
             
                DataRow dRow;
                bool More;

                ARInvoiceAdapter boAr = new ARInvoiceAdapter(sess); boAr.BOConnect();
                ARInvoiceImpl boInvHead = boAr.BusinessObject as ARInvoiceImpl;
                ARInvoiceDataSet dsInvcHead = new ARInvoiceDataSet();
                DateTime dFechaI = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime dFechaF = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());
                string SQL = "Company = '" + sess.CompanyID.ToString() + "' and InvoiceDate  >= '" + dFechaI.ToString("MM/dd/yyyy") + "' and InvoiceDate <= '" + dFechaF.ToString("MM/dd/yyyy") + "'";

                dsInvcHead = boInvHead.GetRows(SQL, "", "", "", "", "", "", "", "", "", "", "", "", 0, 1, out More);

                int contador = 0;
                decimal sumatoria = 0;
                bool existe = false;
                string planta = ""; 
                foreach (ARInvoiceDataSet.InvcHeadRow drI in dsInvcHead.InvcHead)
                {
                    foreach (ARInvoiceDataSet.InvcHeadRow drIh in dsInvcHead.InvcHead)
                    {
                        if (drI.Plant == drIh.Plant)
                        {
                            contador++;
                            sumatoria += drIh.DocInvoiceAmt;
                            planta = drI.Plant;
                        }
                    }
                    if (dTInfoFac.Rows.Count - 1 >= 0 )
                    {
                        for (int i = 0; i < dTInfoFac.Rows.Count - 1; i++)
                        {
                            if (Convert.ToString(dTInfoFac.Rows[i]["Plant"]) == drI.Plant)
                            {
                                existe = true;
                                break;
                            }
                        }
                    }
                    if (!existe)
                    {
                        foreach (ARInvoiceDataSet.InvcHeadRow RRR in dsInvcHead.InvcHead)
                        {
                            dRow = dTInfoFac.NewRow();
                            dRow["Plant"] = planta;
                            dRow["CantFacturas"] = contador;
                            dRow["ToatalVendido"] = sumatoria;
                            dTInfoFac.Rows.Add(dRow);
                            break;
                        }
                    }
                }
                dgMostrar.DataSource = dTInfoFac;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
