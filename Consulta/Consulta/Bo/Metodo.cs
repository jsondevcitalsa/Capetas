using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Ice.Proxy.Lib;
using Erp.Adapters;
using Erp.BO;
using Erp.Proxy.BO;


using Ice.Lib.Framework;

namespace Consulta.Bo
{
    class Metodo
    {
        public DataTable consultas(object sess, string Referencia, string Where, string campos, ref string Error)
        {
            DataTable dTable = new DataTable();
            try
            {
                BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>(sess as Ice.Core.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
                string whereClause = Where;
                System.Data.DataSet dsPartRevSearchAdapter = _bor.GetList(Referencia, whereClause, campos);
                if (dsPartRevSearchAdapter.Tables[0].Rows.Count - 1 >= 0)
                {
                    dTable = dsPartRevSearchAdapter.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return dTable;
        }

        public DataTable ConInvoice(object sess, string Referencia, string Where, string campos, ref string Error)
        {
            DataTable dtInvoice = new DataTable();
            try
            {
                BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>(sess as Ice.Core.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
                string whereClause = Where;
                System.Data.DataSet dsInvoiceSearchAdapter = _bor.GetList(Referencia, whereClause, campos);
                if (dsInvoiceSearchAdapter.Tables[0].Rows.Count - 1 >= 0)
                {
                    dtInvoice = dsInvoiceSearchAdapter.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return dtInvoice;
        }
        public DataTable Part(object sess, string Referencia, string Where, string campos, ref string Error)
        {
            DataTable dtPart = new DataTable();
            try
            {
                BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>(sess as Ice.Core.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
                string whereClause = Where;
                System.Data.DataSet dsPartSearchAdapter = _bor.GetList(Referencia, whereClause, campos);
                if (dsPartSearchAdapter.Tables[0].Rows.Count - 1 >= 0)
                {
                    dtPart = dsPartSearchAdapter.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return dtPart;
        }


        public DataTable InfVent(object sess, string Referencia, string Where, string campos, ref string Error)
        {
            DataTable dtinfventas = new DataTable();
            try
            {
                BOReaderImpl _bor = WCFServiceSupport.CreateImpl<Ice.Proxy.Lib.BOReaderImpl>(sess as Ice.Core.Session, Epicor.ServiceModel.Channels.ImplBase<Ice.Contracts.BOReaderSvcContract>.UriPath);
                string whereClause = Where;
                System.Data.DataSet dtinfventasSearchAdapter = _bor.GetList(Referencia, whereClause, campos);
                if (dtinfventasSearchAdapter.Tables[0].Rows.Count - 1 >= 0)
                {
                    dtinfventas = dtinfventasSearchAdapter.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return dtinfventas;
        }



    }
}
