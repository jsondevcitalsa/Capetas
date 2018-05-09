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
   public partial class Form2 : Form
   {
        Session sess;
        public Form2()
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
        private void Form2_Load(object sender, EventArgs e)
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

       private void button1_Click(object sender, EventArgs e)
       {
           try
           {
          
                DataTable dTClientPre = new DataTable();
                dTClientPre.Columns.Add("CustNum", typeof(int));
                dTClientPre.Columns.Add("Name", typeof(string));
                dTClientPre.Columns.Add("CanFacturas", typeof(int));
                dTClientPre.Columns.Add("ValorTotal", typeof(decimal));
                DataRow dRow;
                bool More;


                ARInvoiceAdapter boAr = new ARInvoiceAdapter(sess); boAr.BOConnect();
                ARInvoiceImpl boInvHead = boAr.BusinessObject as ARInvoiceImpl;
                ARInvoiceDataSet dsInvcHead = new ARInvoiceDataSet();
                DateTime dFechaI = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime dFechaF = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());
                string SQL = "Company = '" + sess.CompanyID.ToString() + "' and InvoiceDate  >= '" + dFechaI.ToString("MM/dd/yyyy") + "' and InvoiceDate <= '" + dFechaF.ToString("MM/dd/yyyy") + "' and DocInvoiceAmt > 0 ";

                dsInvcHead = boInvHead.GetRows(SQL, "", "", "", "", "", "", "", "", "", "", "", "", 0, 1, out More);

                bool Existe = false;
                int cantidad = 0;
                decimal suma = 0;

                foreach (ARInvoiceDataSet.InvcHeadRow drI in dsInvcHead.InvcHead)
                {
                    foreach (ARInvoiceDataSet.InvcHeadRow drIh in dsInvcHead.InvcHead)
                    {
                        if (drI.CustNum == drIh.CustNum)
                        {
                            cantidad++;
                            suma += drIh.DocInvoiceAmt;
                        }
                    }

                    if (dTClientPre.Rows.Count - 1 >= 0)
                    {
                        for (int h = 0; h <= dTClientPre.Rows.Count - 1; h++)
                        {
                            if (Convert.ToInt32(dTClientPre.Rows[h]["CustNum"]) == drI.CustNum)
                            {
                                Existe = true; break;
                            }
                        }
                    }
                    if (!Existe)
                    {
                        CustomerAdapter cusAD = new CustomerAdapter(sess); cusAD.BOConnect();
                        CustomerImpl boCustomer = cusAD.BusinessObject as CustomerImpl;
                        CustomerDataSet dsCustomer = new CustomerDataSet();
                        dsCustomer = boCustomer.GetByID(drI.CustNum);
                        foreach (CustomerDataSet.CustomerRow dr in dsCustomer.Customer)
                        {
                            dRow = dTClientPre.NewRow();
                            dRow["CustNum"] = dr.CustNum;
                            dRow["Name"] = dr.Name;
                            dRow["CanFacturas"] = cantidad;
                            dRow["ValorTotal"] = suma;
                            dTClientPre.Rows.Add(dRow);
                        }
                    }
                }
                //GRID
                dgDatos.DataSource = dTClientPre;
           }
           catch (Exception ex)
           {
                MessageBox.Show(ex.Message);
           }
       }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
