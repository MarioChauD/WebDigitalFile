using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Configuration;


namespace APP.DBUtility
{
    public abstract class OracleHelper 
    {
        
        public delegate T ReaderHandler<T>(IDataReader reader);

        //Database connection strings
        public static readonly string TNSname = ConfigurationManager.AppSettings["tnsName_SIGE"];
        public static readonly string ServerUser = ConfigurationManager.AppSettings["usuario_BDSIGE"];
        public static readonly string ServerPass = ConfigurationManager.AppSettings["password_BDSIGE"];
        public static readonly string ConnectionString = getStrCad();


        public static String getStrCad()
        {
            return "Data Source=" + TNSname + ";User ID=" + ServerUser + ";password=" + ServerPass;
        }


        public static System.Collections.Generic.List<T> ExecuteReaderWithList<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params OracleParameter[] commandParameters)
        {
            System.Collections.Generic.List<T> l = new System.Collections.Generic.List<T>();
            using (OracleDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {

                while (rdr.Read())
                {
                    T value = handler(rdr);
                    l.Add(value);
                }
                return l;
            }
        }

        public static OracleDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);
            cmd.CommandTimeout = 800;

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandTimeout = 800;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
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
                foreach (OracleParameter parm in cmdParms)
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

        public static T ExecuteReaderWithOneResult<T>(string connectionString, CommandType cmdType, string cmdText, ReaderHandler<T> handler, params OracleParameter[] commandParameters)
        {
            using (OracleDataReader rdr = ExecuteReader(connectionString, cmdType, cmdText, commandParameters))
            {
                if (rdr.Read())
                {
                    T value = handler(rdr);
                    return value;
                }
                return default(T);
            }
        }

        public static DataTable ExecuteReaderDataTable(String connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            bool mustCloseConnection = false;

            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);

            PrepareCommand(cmd, conn, (OracleTransaction)null, commandType, commandText, commandParameters);

            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    conn.Close();

                return dt;
            }
        }
        
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                if (connection == null) throw new ArgumentNullException("connection");


                // Create a command and prepare it for execution
                DataSet ds_resultado = new DataSet();
                ds_resultado.EnforceConstraints = false;
                OracleCommand commandOracle = new OracleCommand();
                OracleDataAdapter dataAdapter = new OracleDataAdapter();

                commandOracle.CommandTimeout = 800;

                bool mustCloseConnection = false;
                PrepareCommand(commandOracle, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                using (OracleDataAdapter da = new OracleDataAdapter(commandOracle))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    commandOracle.Parameters.Clear();

                    if (mustCloseConnection)
                        connection.Close();

                    // Return the dataset
                    return ds;
                }
            }
        }


        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                if (connection == null) throw new ArgumentNullException("connection");


                // Create a command and prepare it for execution
                DataSet ds_resultado = new DataSet();
                ds_resultado.EnforceConstraints = false;
                OracleCommand commandOracle = new OracleCommand();
                OracleDataAdapter dataAdapter = new OracleDataAdapter();

                commandOracle.CommandTimeout = 800;

                bool mustCloseConnection = false;
                PrepareCommand(commandOracle, connection, (OracleTransaction)null, commandType, commandText, commandParameters);

                commandOracle.ExecuteNonQuery();

                commandOracle.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();
            }
        }

        #region Dispose




        #endregion
    }

}
