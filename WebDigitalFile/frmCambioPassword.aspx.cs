using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DigitalFile_BE;
using DigitalFile_BL;

namespace WebDigitalFile
{
    public partial class frmCambioPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("frmLogin.aspx");

                }
            }
        }


        protected void btnCambioPassword_Click(object sender, EventArgs e)
        {
            ColaboradorBE oColaboradorBE = new ColaboradorBE();
            ColaboradorBL oColaboradorBL = new ColaboradorBL();


            //lblMensaje.Text = string.Empty;

            string sCodUsuario = string.Empty;
            string sPasswordOri = string.Empty;
            string sPassword = string.Empty;
            string sPassword_New1 = string.Empty;
            string sPassword_New2 = string.Empty;

            sCodUsuario = Session["Usuario"].ToString();
            sPasswordOri = Session["Password"].ToString();
            sPassword = txtPassword.Text.Trim();
            sPassword_New1 = txtNewPassword.Text.Trim();
            sPassword_New2 = txtNewPassword2.Text.Trim();

            if (!sPasswordOri.Equals(sPassword))
            {
                lblMensaje.Text = "Contraseña Incorrecta Favor de ingresar la Contraseña.";

                return;
            }

            if (sPassword_New1.Length < 6 || sPassword_New2.Length < 6)
            {
                lblMensaje.Text = "La Nueva Contraseña debe tener por lo menos 6 caracteres.";

                return;
            }

            if (sCodUsuario == string.Empty || sPassword == string.Empty || sPassword_New1 == string.Empty || sPassword_New2 == string.Empty )
            {
                lblMensaje.Text = "Contraseñas Incorrectas. Favor de registrar la Nueva Contraseña.";

                return;
            }

           
            if (!sPassword_New1.Equals(sPassword_New2))
            {

                lblMensaje.Text = "Confirmación de Nueva Contraseña Incorrecta.";

                return;
            }

            else
            {
                if (sPasswordOri.Equals(sPassword_New1))
                {
                    lblMensaje.Text = "La Nueva contraseña debe ser diferente a la anterior.";

                    return;
                }
                else {
                    oColaboradorBL.CambiarPassword(sCodUsuario, sPassword_New1);

                    Session["Usuario"] = string.Empty;
                    Response.Redirect("frmLogin.aspx");

                }

            }

            return;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["Usuario"] = string.Empty;
            Response.Redirect("frmLogin.aspx");
        }
    }
}