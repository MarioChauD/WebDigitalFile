using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDigitalFile
{
    public partial class frmUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"].ToString() == string.Empty)
                {
                    Response.Redirect("frmLogin.aspx");

                }

                lblUsuario.Text = Session["NombreUsuario"].ToString();

            }
        }
    }
}