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
    public class ColaboradorDA
    {

        public ColaboradorBE getColaboradorOracle(string sCodUsuario)
        {
            ColaboradorBE oColaboradorBE = new ColaboradorBE();

            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("P_COD_USUARIO", OracleDbType.Varchar2, 20));
            par[0].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sCodUsuario);

            par.Add(new OracleParameter("K_CURSOR", OracleDbType.RefCursor,ParameterDirection.Output));
            

            DataSet ds = APP.DBUtility.OracleHelper.ExecuteDataset(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_GET_USUARIO", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {

                oColaboradorBE.Codigo = ds.Tables[0].Rows[0]["Codigo_SAP"].ToString();
                oColaboradorBE.Nombre_Completo = ds.Tables[0].Rows[0]["Nombre_Completo"].ToString();
                oColaboradorBE.TipoDocIdentidad = ds.Tables[0].Rows[0]["Tipo_Doc_Identidad"].ToString();
                oColaboradorBE.NumeroDocIdentidad = ds.Tables[0].Rows[0]["Numero_Doc_Identidad"].ToString();
                oColaboradorBE.TipoColaborador = ds.Tables[0].Rows[0]["Tipo_Colaborador"].ToString();
                oColaboradorBE.Usuario = sCodUsuario;
                oColaboradorBE.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                oColaboradorBE.TipoColaborador = ds.Tables[0].Rows[0]["Tipo_Colaborador"].ToString();
                oColaboradorBE.TipoValidacion = ds.Tables[0].Rows[0]["Tipo_Validacion"].ToString();
                oColaboradorBE.PerfilUsuario = ds.Tables[0].Rows[0]["Perfil_Usuario"].ToString();
                oColaboradorBE.CambiarPassword = ds.Tables[0].Rows[0]["Estado_Cambio_Password"].ToString();

            }

            return oColaboradorBE;

        }

        public ColaboradorBE getColaborador(string sCodUsuario)
        {
            ColaboradorBE oColaboradorBE = new ColaboradorBE();

            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@Usuario", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCodUsuario);

            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ValidaUsuario", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {

                oColaboradorBE.Codigo = ds.Tables[0].Rows[0]["Codigo_SAP"].ToString();
                oColaboradorBE.Nombre_Completo = ds.Tables[0].Rows[0]["Nombre_Completo"].ToString();
                oColaboradorBE.TipoDocIdentidad = ds.Tables[0].Rows[0]["Tipo_Doc_Identidad"].ToString();
                oColaboradorBE.NumeroDocIdentidad = ds.Tables[0].Rows[0]["Numero_Doc_Identidad"].ToString();
                oColaboradorBE.TipoColaborador = ds.Tables[0].Rows[0]["Tipo_Colaborador"].ToString();
                oColaboradorBE.Usuario = sCodUsuario;
                oColaboradorBE.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                oColaboradorBE.TipoColaborador = ds.Tables[0].Rows[0]["Tipo_Colaborador"].ToString();
                oColaboradorBE.TipoValidacion = ds.Tables[0].Rows[0]["Tipo_Validacion"].ToString();
                oColaboradorBE.PerfilUsuario = ds.Tables[0].Rows[0]["Perfil_Usuario"].ToString();
                oColaboradorBE.CambiarPassword = ds.Tables[0].Rows[0]["Estado_Cambio_Password"].ToString();

            }

            return oColaboradorBE;

        }

        public DataTable getUsuariosOracle(string sCodigoUsuario, string sNombreUsuario, string sTipoColaborador, string sNumeroDocumento, string sUsuario)
        {
            DataTable oDatatable = new DataTable();
            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("P_COD_USUARIO", OracleDbType.Varchar2, 20));
            par[0].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new OracleParameter("P_NOMBRE_USUARIO", OracleDbType.Varchar2, 300));
            par[1].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sNombreUsuario);

            par.Add(new OracleParameter("P_TIPO_COLABORADOR", OracleDbType.Varchar2, 5));
            par[2].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sTipoColaborador);

            par.Add(new OracleParameter("P_NUM_DOCUMENTO", OracleDbType.Varchar2, 5));
            par[3].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sNumeroDocumento);

            par.Add(new OracleParameter("P_USUARIO", OracleDbType.Varchar2, 20));
            par[4].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sUsuario);


            par.Add(new OracleParameter("K_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output));

            DataSet ds = APP.DBUtility.OracleHelper.ExecuteDataset(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_LISTAR_USUARIOS", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;

        }

        public DataTable getUsuarios(string sCodigoUsuario, string sNombreUsuario, string sTipoColaborador, string sNumeroDocumento, string sUsuario)
        {
            DataTable oDatatable= new DataTable();
            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new SqlParameter("@NombreUsuario", SqlDbType.NVarChar, 300));
            par[1].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sNombreUsuario);

            par.Add(new SqlParameter("@TipoColaborador", SqlDbType.NVarChar, 5));
            par[2].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sTipoColaborador);

            par.Add(new SqlParameter("@NumeroDocumento", SqlDbType.NVarChar, 20));
            par[3].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sNumeroDocumento);

            par.Add(new SqlParameter("@Usuario", SqlDbType.NVarChar, 20));
            par[4].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sUsuario);

            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ListarUsuarios", par.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                oDatatable = ds.Tables[0];
            }

            return oDatatable;

        }


        public void ActualizarUsuarioOracle(string sCodigoUsuario, string sUsuario, string sActivo, string sCambiarClave)
        {

            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("P_COD_USUARIO", OracleDbType.Varchar2, 20));
            par[0].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new OracleParameter("P_USUARIO", OracleDbType.Varchar2, 20));
            par[1].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sUsuario);


            par.Add(new OracleParameter("P_ESTADO", OracleDbType.Varchar2,1));
            par[2].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sActivo);


            par.Add(new OracleParameter("P_GENERA_CLAVE", OracleDbType.Varchar2, 1));
            par[3].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sCambiarClave);


            APP.DBUtility.OracleHelper.ExecuteNonQuery(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_ACTUALIZAR_USUARIO", par.ToArray());


        }

        public void ActualizarUsuario(string sCodigoUsuario, string sUsuario, string sActivo, string sCambiarClave)
        {
          

            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new SqlParameter("@Usuario", SqlDbType.NVarChar, 20));
            par[1].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sUsuario);

            par.Add(new SqlParameter("@Estado", SqlDbType.NVarChar, 1));
            par[2].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sActivo);

            par.Add(new SqlParameter("@GenerarClave", SqlDbType.NVarChar, 1));
            par[3].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCambiarClave);

            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_ActualizarUsuario", par.ToArray());


          
        }


        public void CambiarPasswordOracle(string sCodigoUsuario, string sPassword)
        {
            List<OracleParameter> par;

            par = new List<OracleParameter>();

            par.Add(new OracleParameter("P_COD_USUARIO", OracleDbType.Varchar2, 20));
            par[0].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new OracleParameter("P_PASSWORD", OracleDbType.Varchar2, 20));
            par[1].Value = APP.DBUtility.OracleHelper.GetDataForDBfromNullable<String>(sPassword);


            APP.DBUtility.OracleHelper.ExecuteNonQuery(APP.DBUtility.OracleHelper.ConnectionString, CommandType.StoredProcedure, "SIGE.PKG_DIGITAL_FILE.SP_ACTUALIZAR_PASSWORD", par.ToArray());

        }


        public void CambiarPassword(string sCodigoUsuario, string sPassword)
        {
            List<SqlParameter> par;

            par = new List<SqlParameter>();

            par.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 20));
            par[0].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sCodigoUsuario);

            par.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 30));
            par[1].Value = APP.DBUtility.SqlHelper.GetDataForDBfromNullable<String>(sPassword);


            DataSet ds = APP.DBUtility.SqlHelper.ExecuteDataset(APP.DBUtility.SqlHelper.ConnectionString,
            CommandType.StoredProcedure, "SP_CambiarPassword", par.ToArray());


        }



    }
}
