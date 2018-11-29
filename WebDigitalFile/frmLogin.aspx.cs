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
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Usuario"] = string.Empty;
            Session["Perfil"] = string.Empty;

            if (!IsPostBack)
            {

                if (Request.Cookies["AutenticacionUsuario"] != null)
                {
                    string sCodUsuario = Request.Cookies["AutenticacionUsuario"]["Usuario"];
                    string sPassword = Request.Cookies["AutenticacionUsuario"]["Password"];
                    Boolean bLoginOk = false;


                    if (sCodUsuario != null || sPassword != null)
                    {
                        ColaboradorBE oColaboradorBE = new ColaboradorBE();
                        ColaboradorBL oColaboradorBL = new ColaboradorBL();

                        oColaboradorBE = oColaboradorBL.getColaborador(sCodUsuario);


                        if (oColaboradorBE.TipoValidacion == "LDAP")
                        {
                            if (ColaboradorBL.Login(sCodUsuario, sPassword))
                            {
                                bLoginOk = true;
                            }
                        }
                        else
                            if (sPassword == oColaboradorBE.Password)
                        {
                            bLoginOk = true;

                        }

                        if (bLoginOk)
                        {
                            Session["Usuario"] = oColaboradorBE.Codigo;
                            Session["Perfil"] = oColaboradorBE.PerfilUsuario;
                            Session["NombreUsuario"] = oColaboradorBE.Nombre_Completo;
                            Session["NumeroDocumento"] = oColaboradorBE.NumeroDocIdentidad;
                            Session["PerfilDes"] = oColaboradorBE.PerfilUsuario == "A" ? "Administrador" : "Usuario";
                            Session["Password"] = oColaboradorBE.Password;
                            Session["CerrarApp"] = "N";
                            Session["TipoValidacion"] = oColaboradorBE.TipoValidacion;

                            if (Session["Perfil"].ToString() == "A")
                            {
                                Response.Redirect("frmGestionUsuarios.aspx");
                            }
                            else
                            {
                                if (oColaboradorBE.CambiarPassword == "S" && oColaboradorBE.TipoValidacion != "LDAP")
                                {
                                    Response.Redirect("frmCambioPassword.aspx");
                                }
                                else
                                {
                                    Response.Redirect("frmListaDocumentos.aspx");
                                }
                            }
                        }

                    }
                }
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ColaboradorBE oColaboradorBE = new ColaboradorBE();
            ColaboradorBL oColaboradorBL = new ColaboradorBL();

            
            lblMensaje.Text = string.Empty;

            string sCodUsuario = string.Empty;
            string sPassword = string.Empty;
            string sMensaje = "Usuario o Contraseña no Válido";
            Boolean bLoginOk = false;

            sCodUsuario = txtUsuario.Text.Trim();
            sPassword = txtPassword.Text.Trim();

            if ( sCodUsuario == string.Empty || sPassword == string.Empty)
            {
                lblMensaje.Text = "Usuario o Contraseña no Válido";

                return;
            }


            oColaboradorBE = oColaboradorBL.getColaborador(sCodUsuario);


            if (oColaboradorBE.Codigo != null)
            {
                if (oColaboradorBE.TipoValidacion == "LDAP")
                {
                    if (!ColaboradorBL.Login(sCodUsuario, sPassword))
                    {
                        sMensaje = "Verifique las credenciales suministradas.";
                    }
                    else
                    {
                        bLoginOk = true;
                    }
                }

                else
                {
                    if (sPassword == oColaboradorBE.Password)
                    {
                        bLoginOk = true;
                    }
                    else {
                        sMensaje = "Usuario o Contraseña no Válido";
                    }

                }

                if (bLoginOk)
                {
                    Session["Usuario"] = oColaboradorBE.Codigo;
                    Session["Perfil"] = oColaboradorBE.PerfilUsuario;
                    Session["NombreUsuario"] = oColaboradorBE.Nombre_Completo;
                    Session["NumeroDocumento"] = oColaboradorBE.NumeroDocIdentidad;
                    Session["PerfilDes"] = oColaboradorBE.PerfilUsuario == "A" ? "Administrador" : "Usuario";
                    Session["Password"] = oColaboradorBE.Password;
                    Session["TipoValidacion"] = oColaboradorBE.TipoValidacion;

                    if (chkRecordarCredenciales.Checked)
                    {
                        Response.Cookies["AutenticacionUsuario"]["Usuario"] = sCodUsuario;
                        Response.Cookies["AutenticacionUsuario"]["Password"] = sPassword;
                        Response.Cookies["AutenticacionUsuario"].Expires = DateTime.Now.AddDays(2);
                    }


                    if (Session["Perfil"].ToString() == "A")
                    {
                        Response.Redirect("frmGestionUsuarios.aspx");
                      //  Response.Redirect("frmListaUsuarios.aspx");
                    }
                    else {

                        if (oColaboradorBE.CambiarPassword == "S" && oColaboradorBE.TipoValidacion != "LDAP")
                        {
                            Response.Redirect("frmCambioPassword.aspx");
                        }
                        else
                        {
                            Response.Redirect("frmListaDocumentos.aspx");
                        }
                            
                    }

                }
               
            }

            lblMensaje.Text = sMensaje;

            return;
        }

    }
}