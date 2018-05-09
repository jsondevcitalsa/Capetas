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
    public partial class cotizacion : Form
    {
        Session sess;
        public cotizacion()
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

        private void cotizacion_Load(object sender, EventArgs e)
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
            DataTable dTCoti = new DataTable();
            dTCoti.Columns.Add("PartNum", typeof(string));
            dTCoti.Columns.Add("PartDesc", typeof(string));
            dTCoti.Columns.Add("CantVen", typeof(int));
            dTCoti.Columns.Add("CantVenconf", typeof(decimal));
            DataRow dRow;
            bool More;

            QuoteAdapter adquo = new QuoteAdapter(sess);adquo.BOConnect();
            QuoteImpl imquo = adquo.BusinessObject as QuoteImpl;
            QuoteDataSet dsQuote = new QuoteDataSet();
            DateTime dFechaI = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
            DateTime dFechaF = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());
            string SQL = "Company = '" + sess.CompanyID.ToString() + "' and EntryDate >= '" + dFechaI.ToString("MM/dd/yyyy") + "' and EntryDate <= '" + dFechaF.ToString("MM/dd/yyyy") + "'";
            dsQuote = imquo.GetRows(SQL, "", "", "", "", "", "", "", "", "", "", "", "", "" , "" , "" , "" , 0, 1, out More);

            decimal cantidad2 = 0;
            int cantidad = 0;
            bool Existe = false;
            foreach (QuoteDataSet.QuoteDtlRow drqu in dsQuote.QuoteDtl)
            {
               
                foreach (QuoteDataSet.QuoteDtlRow drqu2 in dsQuote.QuoteDtl)
                {
                    if (drqu.PartNum == drqu2.PartNum)
                    {
                        cantidad++;
                        cantidad2 += drqu.SellingExpectedQty;
                    }                                   
                }
                 if (dTCoti.Rows.Count - 1 >= 0)
                {
                    for (int h = 0; h <= dTCoti.Rows.Count - 1; h++)
                    {
                        if (Convert.ToString(dTCoti.Rows[h]["PartNum"]) == drqu.PartNum)
                        {
                            Existe = true;
                            break;
                        }
                    }
                }
                if (!Existe)
                {
                    PartAdapter adpa = new PartAdapter(sess); adpa.BOConnect();
                    PartImpl impart = adpa.BusinessObject as PartImpl;
                    PartDataSet dsPart = new PartDataSet();
                    dsPart = impart.GetByID(drqu.PartNum);
                    foreach (PartDataSet.PartRow fecot in dsPart.Part)
                    {
                        dRow = dTCoti.NewRow();
                        dRow["PartNum"] = drqu.PartNum;
                        dRow["PartDesc"] = fecot.PartDescription;
                        dRow["CantVen"] = cantidad;
                        dRow["CantVenconf"] = cantidad2;
                        dTCoti.Rows.Add(dRow);
                    }
                }
            }
            dgDatos.DataSource = dTCoti;




        }
    }
}
