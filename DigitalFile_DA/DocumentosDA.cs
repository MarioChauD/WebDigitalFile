using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DigitalFile_DA
{
   public class DocumentosDA
    {

        public DataTable getDocumentosOracle(string sNumDocumento)
        {
            DataTable oDatatable = new DataTable();

            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("P_DNI_USUARIO", OracleDbType.Varchar2, 20));
            par[0].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sNumDocumento);

            par.Add(new OracleParameter("K_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));

            DataSet ds = APP.DBUtility.OracleHelper.ExecuteDataset(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_GET_DOCUMENTOS", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;

        }

        public DataTable getDocumentos(string sCodigoUsuario)
        {
            DataTable oDatatable = new DataTable();
            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ListarDocumentos", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;

        }

    }
}
