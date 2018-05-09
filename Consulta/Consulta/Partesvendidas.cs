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
    public partial class Partesvendidas : Form
    {
        Session sess;
        public Partesvendidas()
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
        private void Partesvendidas_Load(object sender, EventArgs e)
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

                DataTable dTInfoV = new DataTable();
                dTInfoV.Columns.Add("InvoiceNum", typeof(int));
                dTInfoV.Columns.Add("PartNum", typeof(string));
                dTInfoV.Columns.Add("PartDescription", typeof(string));
                dTInfoV.Columns.Add("PrecioUnitario", typeof(decimal));
                dTInfoV.Columns.Add("CantidadVendida", typeof(string));
                dTInfoV.Columns.Add("Total", typeof(decimal));
                DataRow dRow;
                bool More;

            
                ARInvoiceAdapter boAr = new ARInvoiceAdapter(sess); boAr.BOConnect();
                ARInvoiceImpl boInvHead = boAr.BusinessObject as ARInvoiceImpl;
                ARInvoiceDataSet dsInvcHead = new ARInvoiceDataSet();
                DateTime dFechaI = Convert.ToDateTime(dtpDesde.Value.ToShortDateString());
                DateTime dFechaF = Convert.ToDateTime(dtpHasta.Value.ToShortDateString());
                string SQL = "Company = '" + sess.CompanyID.ToString() + "' and InvoiceDate  >= '" + dFechaI.ToString("MM/dd/yyyy") + "' and InvoiceDate <= '" + dFechaF.ToString("MM/dd/yyyy") + "'";

                dsInvcHead = boInvHead.GetRows(SQL , "" , "" , "", "", "", "", "", "", "", "", "" , "", 0 , 1 , out More );




                decimal cantidad =0 ;
                foreach (ARInvoiceDataSet.InvcHeadRow drr in dsInvcHead.InvcHead)
                {
                    
                    if (drr.Posted)
                    {
                        if (drr.InvoiceNum >= 1625498)
                        {
                            if (drr.InvoiceNum <= 1625549)
                            {
                                foreach (ARInvoiceDataSet.InvcDtlRow drs in dsInvcHead.InvcDtl)
                            {
                         
                          cantidad =   drs.DspSellingShipQty;
                        if(drs.CallNum > 0)
                        { 
                            //
                          JobMtlSearchAdapter boJPart = new JobMtlSearchAdapter(sess); boJPart.BOConnect();
                          JobMtlSearchImpl boJobPart = boJPart.BusinessObject as JobMtlSearchImpl;
                          JobMtlListDataSet dsJobPart = new JobMtlListDataSet();
                          dsJobPart = boJobPart.GetList("Company = '" + sess.CompanyID.ToString() + "'  and JobComplete = True and UnitPrice > 0 and CallNum = '"+drs.CallNum+"'", 0, 1, out More);
                                foreach (JobMtlListDataSet.JobMtlListRow dr in dsJobPart.JobMtlList)
                                {
                                        decimal   TotalV = drs.DocUnitPrice * cantidad;
                                    if (drs.CallNum == dr.CallNum)
                                    { 
                                    dRow = dTInfoV.NewRow();
                                    dRow["InvoiceNum"] = drr.InvoiceNum;
                                    dRow["PartNum"] = dr.PartNum;
                                    dRow["PartDescription"] = dr.Description;
                                    dRow["PrecioUnitario"] = drs.DocUnitPrice;
                                    dRow["CantidadVendida"] = cantidad;
                                    dRow["Total"] = TotalV;
                                    dTInfoV.Rows.Add(dRow);
                                               
                                    }
                                }
                            }
                            }
                            }
                        }
                    }
               }
                dgVer.DataSource = dTInfoV;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
