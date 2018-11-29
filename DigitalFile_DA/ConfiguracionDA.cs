using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalFile_BE;
using System.Data.SqlClient;
using DigitalFile_DA;
using Oracle.ManagedDataAccess.Client;

namespace DigitalFile_DA
{
  public  class ConfiguracionDA
    {

        public ConfiguracionBE getConfiguracionOracle()
        {
            ConfiguracionBE oConfiguracionBE = new ConfiguracionBE();

            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("K_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));


            DataSet ds = APP.DBUtility.OracleHelper.ExecuteDataset(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_GET_DIRECTORIOS", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {

                oConfiguracionBE.Directorio_Origen = ds.Tables[0].Rows[0]["VALOR"].ToString();
                oConfiguracionBE.Directorio_Destino = ds.Tables[0].Rows[1]["VALOR"].ToString();

            }

            return oConfiguracionBE;

        }
        public ConfiguracionBE getConfiguracion()
        {
            ConfiguracionBE oConfiguracionBE = new ConfiguracionBE();

            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ListarDirectorios");

            if (ds.Tables[0].Rows.Count > 0)
            {

                oConfiguracionBE.Directorio_Origen = ds.Tables[0].Rows[0]["VALOR"].ToString();
                oConfiguracionBE.Directorio_Destino = ds.Tables[0].Rows[1]["VALOR"].ToString();

            }

            return oConfiguracionBE;

        }

    }
}
