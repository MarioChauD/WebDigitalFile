using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.Win32;

namespace APP.DBUtility
{
    public abstract class SqlHelper
    {
        public delegate T ReaderHandler<T>(IDataReader reader);

        //Database connection strings
        public static readonly string ServerSource = ConfigurationManager.AppSettings["ServerSource"];
        public static readonly string ServerDB = ConfigurationManager.AppSettings["ServerDB"];
        public static readonly string ServerUser = ConfigurationManager.AppSettings["ServerUser"];
        public static readonly string ServerPass = ConfigurationManager.AppSettings["ServerPass"];
        public static readonly string ConnectionString = getStrCad();

     
        public static String getStrCad()
        {

            return "Data Source=" + ServerSource + "; Database=" + ServerDB + ";User ID=" + ServerUser + ";password=" + ServerPass;
        }
        public static System.Collections.Generic.List<T> ExecuteReaderWithList<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params SqlParameter[] commandParameters)
        {
            System.Collections.Generic.List<T> l = new System.Collections.Generic.List<T>();
            using (SqlDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {

                while (rdr.Read())
                {
                    T value = handler(rdr);
                    l.Add(value);
                }
                return l;
            }
        }
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            cmd.CommandTimeout = 800;

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 800;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        public static T ConvertToNonNullableOrDefaultValue<T>(object value)
        {
            if ((value != DBNull.Value))
                return (T)value;
            return default(T);
        }
        public static object GetDataForDBfromNullable<T>(Nullable<T> value) where T : struct
        {
            if (value.HasValue)
            {
                if (value.GetType() == Type.GetType("System.Boolean"))
                    return value.Value;
            }
            if (value.HasValue && !value.Value.Equals(default(T)))
                return value.Value;
            return DBNull.Value;
        }
        public static object GetDataForDBfromNullable<T>(T value)
        {
            if (value != null)
            {
                return value;
            }
            else
            {
                return DBNull.Value;
            }
        }
        public static T ExecuteReaderWithOneResult<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params SqlParameter[] commandParameters)
        {
            using (SqlDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {
                if (rdr.Read())
                {
                    T value = handler(rdr);
                    return value;
                }
                return default(T);
            }
        }
        public static DataTable ExecuteReaderDataTable(String connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            bool mustCloseConnection = false;

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            PrepareCommand(cmd, conn, (SqlTransaction)null, commandType, commandText, commandParameters);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    conn.Close();

                return dt;
            }
        }
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection == null) throw new ArgumentNullException("connection");

                // Create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 800;
                bool mustCloseConnection = false;
                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        connection.Close();

                    // Return the dataset
                    return ds;
                }
            }
        }
      }
}
