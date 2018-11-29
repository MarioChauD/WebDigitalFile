using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebDigitalFile
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["app"] = "Documentos Digitales";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["Usuario"] = string.Empty;
            Session["Perfil"] = string.Empty;
            Session["NombreUsuario"] = string.Empty;
            Session["NumeroDocumento"] = string.Empty;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["Usuario"] = string.Empty;
            Session["Perfil"] = string.Empty;
            Session["NombreUsuario"] = string.Empty;
            Session["NumeroDocumento"] = string.Empty;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}