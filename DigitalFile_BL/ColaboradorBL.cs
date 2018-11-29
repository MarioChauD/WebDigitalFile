using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalFile_BE;
using DigitalFile_DA;
using System.Data;
using System.IO;
using System.DirectoryServices;

namespace DigitalFile_BL
{
    public class ColaboradorBL
    {
        #region Variables

        public static string pathLDAP;
        public static DirectoryEntry entry;
        public static DirectorySearcher search;
        public static SearchResult result;
        public static SearchResultCollection results;
        public static int userAccountControl = 0;
        public static Int32 countUpdate = 0;
        public static Int32 countInactive = 0;
        public static Int32 countNoExist = 0;
        public static Int32 row = 0;
        public static string root = "";

        #endregion


        #region CredencialesAD

        public static string ADPath = "LDAP://PRODAC.COM.PE";             //Ruta raíz que apunta al servidor de AD.
        public static string ADDominio;          //Nombre de dominio de AD
        public static string ADAdminUser;        //Usuario con permisos para funciones de AD.
        public static string ADAdminPassword;    //Contraseña del usuario Adm. para funciones de AD

        #endregion

        #region Enumeraciones

        public enum ADAccountOptions
        {
            UF_TEMP_DUPLICATE_ACCOUNT = 0x0100,
            UF_NORMAL_ACCOUNT = 0x0200,
            UF_INTERDOMAIN_TRUST_ACCOUNT = 0x0800,
            UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,
            UF_SERVER_TRUST_ACCOUNT = 0x2000,
            UF_DONT_EXPIRE_PASSWD = 0x10000,
            UF_SCRIPT = 0x0001,
            UF_ACCOUNTDISABLE = 0x0002,
            UF_HOMEDIR_REQUIRED = 0x0008,
            UF_LOCKOUT = 0x0010,
            UF_PASSWD_NOTREQD = 0x0020,
            UF_PASSWD_CANT_CHANGE = 0x0040,
            UF_ACCOUNT_LOCKOUT = 0X0010,
            UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,
            UF_EXPIRE_USER_PASSWORD = 0x800000,
            /*
            UF_TEMP_DUPLICATE_ACCOUNT = &H100  //0x0100
            UF_NORMAL_ACCOUNT = &H200       //0x0200
            UF_INTERDOMAIN_TRUST_ACCOUNT = &H800
            UF_WORKSTATION_TRUST_ACCOUNT = &H1000
            UF_SERVER_TRUST_ACCOUNT = &H2000
            UF_DONT_EXPIRE_PASSWD = &H10000     //0x10000
            UF_SCRIPT = &H1
            UF_ACCOUNTDISABLE = &H2     //0x0002
            UF_HOMEDIR_REQUIRED = &H8
            UF_LOCKOUT = &H10
            UF_PASSWD_NOTREQD = &H20
            UF_PASSWD_CANT_CHANGE = &H40
            UF_ACCOUNT_LOCKOUT = &H10
            UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = &H80
            UF_PASSWORD_EXPIRED = &H800000              //- (Windows 2000, Windows Server 2003) La contraseña del usuario ha caducado. 
            */
        }

        #endregion


        #region Root
        private static string ADLOGGENERAL = "LDAP://PRODAC.COM.PE"; //ConfigurationManager.AppSettings.Item("ADLOGWIN").ToString().Trim()  //Ubicación del Log de Auditoría para los errores y mensajes en general
        #endregion


        #region Metodos

        /*------------------------------------------------------------------------
        ---------Método de control de acceso para el Usuario Admin.---------------
        --------------------------------------------------------------------------*/
        public static bool Login(string userAdmin, string pwdAdmin)
        {
            try
            {
                //Verificar si efectivamente este usuario existe y está activo
                entry = GetDirectoryObject(userAdmin, pwdAdmin);
                search = new DirectorySearcher();
                search.SearchRoot = entry;
                search.Filter = "(&(objectClass=user)(sAMAccountName=" + userAdmin + "))";
                search.SearchScope = SearchScope.Subtree;

                try
                {
                    result = search.FindOne();
                }
                catch (Exception ex)
                {
                    return false;
                }

                if (result != null)
                {
                    Int32 userAccountControl = Convert.ToInt32(result.GetDirectoryEntry().Properties["userAccountControl"][0].ToString());
                    if (IsAccountActive(userAccountControl))
                        return true;
                    else
                        WriteInLog("La cuenta del usuario: " + result.Properties["SAMAccountName"][0] + " se encuentra inactiva...", userAdmin);
                }
            }
            catch (Exception ex)
            {
                search.Dispose();
                entry.Dispose();
                entry.Close();
                throw ex;
            }

            return false;

        }

        //--------------------------------------------------------------------------------//
        //---Método para validar si la cuenta de usuario en el AD está habilitado o no----//
        //--------------------------------------------------------------------------------//

        private static bool IsAccountActive(Int32 paramUserAccountControl)
        {
            Int32 userAccountControl_Disabled = Convert.ToInt32(ADAccountOptions.UF_ACCOUNTDISABLE);
            Int32 flagExists = (paramUserAccountControl & userAccountControl_Disabled);
            if (flagExists > 0)
                return false;
            else
                return true;
        }

        //-----------------------------------------------------------------------//
        // ------------Métodos para crear un nuevo objeto directorio de AD-------//
        //-----------------------------------------------------------------------//

        private static DirectoryEntry GetDirectoryObject(string usuario, string password)
        {
            DirectoryEntry oDE = new DirectoryEntry(ADPath, usuario, password, AuthenticationTypes.Secure);
            return oDE;
        }

        private static DirectoryEntry GetDirectoryObject(string pathLDAP)
        {
            DirectoryEntry oDE = new DirectoryEntry(pathLDAP, ADAdminUser, ADAdminPassword, AuthenticationTypes.Secure);
            return oDE;
        }


        //-----------------------------------------------------------------------//
        //---------Método para escribir en el Log de Auditoria General-----------//
        //-----------------------------------------------------------------------//
        public static void WriteInLog(string message, string userAdmin)
        {
            FileStream fs = new FileStream(ADLOGGENERAL, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine("Usuario de la Aplicación: " + userAdmin + GetDataMachine());
            sw.WriteLine(message);
            sw.WriteLine();
            sw.Close();
        }

        //-----------------------------------------------------------------------//
        //--------Método para obtener el nombre e IP de la máquina cliente-------//
        //-----------------------------------------------------------------------//
        private static string GetDataMachine()
        {
            string dataMachine = "";
            string nombreHost = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostEntry(nombreHost);

            dataMachine = ", Nombre de Máquina: " + hostInfo.HostName.ToString() + ", IP de Máquina: " + hostInfo.AddressList[0].ToString();

            return dataMachine;
        }

      


        public ColaboradorBE getColaborador(string sCodUsuario)
        {
            ColaboradorDA oColaboradorDA = new ColaboradorDA();

            return oColaboradorDA.getColaboradorOracle(sCodUsuario);

        }


        public DataTable getUsuarios(string sCodigoUsuario, string sNombreUsuario, string sTipoColaborador, string sNumeroDocumento, string sUsuario)
        {
            ColaboradorDA oColaboradorDA = new ColaboradorDA();

            return oColaboradorDA.getUsuariosOracle(sCodigoUsuario, sNombreUsuario, sTipoColaborador, sNumeroDocumento, sUsuario);

        }


        public void ActualizarUsuario(string sCodigoUsuario, string sUsuario, string sActivo, string sCambiarClave)
        {
            ColaboradorDA oColaboradorDA = new ColaboradorDA();

            oColaboradorDA.ActualizarUsuarioOracle(sCodigoUsuario, sUsuario, sActivo, sCambiarClave);

        }

        public void CambiarPassword(string sCodigoUsuario, string sPassword)
        {
            ColaboradorDA oColaboradorDA = new ColaboradorDA();

            oColaboradorDA.CambiarPasswordOracle(sCodigoUsuario, sPassword);

        }


        #endregion

    }
}
