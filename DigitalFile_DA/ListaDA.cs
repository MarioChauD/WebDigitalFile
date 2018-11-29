using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalFile_BE;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DigitalFile_DA
{
    public class ListaDA
    {

        public DataTable getListaTipoEmpleado()
        {
            DataTable oDatatable = new DataTable();
            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("K_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));

            DataSet ds = APP.DBUtility.OracleHelper.ExecuteDataset(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_GET_TIPO_EMPLEADO", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;



        }

        public DataTable getListaComboBox(string sTipo)
        {
            DataTable oDatatable = new DataTable();
            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@Tipo_Listado", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sTipo);


            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ListarGeneral", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;


        }

    }
}
