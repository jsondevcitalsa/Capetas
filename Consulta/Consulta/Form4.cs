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


using Erp.UI.App.CustomerEntry;

namespace Consulta
{
    public partial class Form4 : Form
    {
        Session sess;
        public Form4()
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

        private void Form4_Load(object sender, EventArgs e)
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

                DataTable dTQuote = new DataTable();
                dTQuote.Columns.Add("InvoiceNum", typeof(int));
                dTQuote.Columns.Add("OrderNum", typeof(int));
                dTQuote.Columns.Add("QuoteNum", typeof(int));
                dTQuote.Columns.Add("QuoteComment", typeof(string));
                DataRow dRow;
                bool More;

                ARInvoiceAdapter boAr = new ARInvoiceAdapter(sess); boAr.BOConnect();
                ARInvoiceImpl boInvHead = boAr.BusinessObject as ARInvoiceImpl;
                ARInvoiceDataSet dsInvcHead = new ARInvoiceDataSet();
                DateTime dFechaI = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime dFechaF = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());
                string SQL = "Company = '" + sess.CompanyID.ToString() + "' and InvoiceDate  >= '" + dFechaI.ToString("MM/dd/yyyy") + "' and InvoiceDate <= '" + dFechaF.ToString("MM/dd/yyyy") + "' and DocInvoiceAmt > 0 ";
                
                dsInvcHead = boInvHead.GetRows(SQL, "", "", "", "", "", "", "", "", "", "", "", "", 0, 1, out More);

                int orden = 0;
                foreach (ARInvoiceDataSet.InvcHeadRow drI in dsInvcHead.InvcHead)
                {
                    
                    SalesOrderAdapter sods = new SalesOrderAdapter(sess); sods.BOConnect();
                    SalesOrderImpl bosalOrd = sods.BusinessObject as SalesOrderImpl;
                    SalesOrderDataSet dssalesord = new SalesOrderDataSet();
                    dssalesord = bosalOrd.GetByID(drI.OrderNum);

                    foreach (SalesOrderDataSet.OrderDtlRow dro in dssalesord.OrderDtl)
                    {
                        if (dro.QuoteNum > 0)
                        {

                            QuoteAdapter qadp = new QuoteAdapter(sess); qadp.BOConnect();
                            QuoteImpl dquo = qadp.BusinessObject as QuoteImpl;
                            QuoteDataSet dsquo = new QuoteDataSet();
                            dsquo = dquo.GetByID(dro.CustNum);
                            
                            foreach (QuoteDataSet.QuoteHedRow drquo in dsquo.QuoteHed)
                            {
                                dRow = dTQuote.NewRow();
                                dRow["InvoiceNum"] = drI.InvoiceNum;
                                dRow["OrderNum"] = drI.OrderNum;
                                dRow["QuoteNum"] = drquo.QuoteNum;
                                dRow["QuoteComment"] = drquo.QuoteComment;
                                dTQuote.Rows.Add(dRow);
                                break;
                            }
                        }
                        else
                        { break; }
                    }
                }
                dgDatos.DataSource = dTQuote;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}
    }
}
