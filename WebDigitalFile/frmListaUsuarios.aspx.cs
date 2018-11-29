using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DigitalFile_BL;
using System.Data;

namespace WebDigitalFile
{
    public partial class frmListaUsuarios : System.Web.UI.Page
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
                lblPerfil.Text = "Perfil: " + Session["PerfilDes"].ToString();
                lblMensaje.Text = string.Empty;

                ListarTipoTrabajador();
                ListarUsuarios();

                
            }

        }


        private void ListarUsuarios()
        {
            ColaboradorBL oColaboradorBL = new ColaboradorBL();
            DataTable oDataTable = new DataTable();
            int CantidadFilas = 0;

            string sCodigoUsuario = txt_Codigo.Text.Trim();
            string sNombreUsuario = txt_Nombre.Text.Trim();
            string sTipoColaborador = ddl_TipoTrabajador.SelectedValue;
            string sNumeroDocumento = txt_NumDoc.Text.Trim();
            string sUsuario = txt_Usuario.Text.Trim();
            

            sCodigoUsuario = sCodigoUsuario == string.Empty ? "0" : sCodigoUsuario;
            sNombreUsuario = sNombreUsuario == string.Empty ? "0" : sNombreUsuario;
            sTipoColaborador = sTipoColaborador == string.Empty ? "0" : sTipoColaborador;
            sNumeroDocumento = sNumeroDocumento == string.Empty ? "0" : sNumeroDocumento;
            sUsuario = sUsuario == string.Empty ? "0" : sUsuario;

            oDataTable =  oColaboradorBL.getUsuarios(sCodigoUsuario, sNombreUsuario, sTipoColaborador, sNumeroDocumento, sUsuario);

            grv_Usuarios.DataSource = oDataTable;
            grv_Usuarios.DataBind();

            CantidadFilas = oDataTable.Rows.Count;

            lblMensaje.ForeColor = System.Drawing.Color.Blue;
            lblMensaje.Text = "Se encontraron " + CantidadFilas.ToString() + " Registros.";


        }

        private void ListarTipoTrabajador()
        {
            ListaBL oListaBL = new ListaBL();

            ddl_TipoTrabajador.DataSource = oListaBL.getListaTipoEmpleado();
            ddl_TipoTrabajador.DataTextField = "DESCRIPCION";
            ddl_TipoTrabajador.DataValueField = "CODIGO";
            ddl_TipoTrabajador.DataBind();
            ddl_TipoTrabajador.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void ibtn_filtrar_Click(object sender, ImageClickEventArgs e)
        {
            ListarUsuarios();
        }

        protected void ibtn_grabar_Click(object sender, ImageClickEventArgs e)
        {
            string sCodigoColaborador = hdn_cod.Value;
            string sUsuarioColaborador = txt_Usuario.Text.Trim();
            string sActivo = chkActivo.Checked ? "A" : "I";
            string sGenenarClave = chkGenerarClave.Checked ? "S" : "N";


            if (hdn_cod.Value != string.Empty)
            {

            
            ColaboradorBL oColaboradorBL = new ColaboradorBL();

            oColaboradorBL.ActualizarUsuario(sCodigoColaborador, sUsuarioColaborador, sActivo, sGenenarClave);

                Limpiar();

                ImagenAlerta.Src = "Images/info.gif";
                lblMensaje.ForeColor = System.Drawing.Color.Blue;
                lblMensaje.Text = "Los cambios se registraron corectamente.";
            }
            else
            {
                ImagenAlerta.Src = "Images/alert.png";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Seleccione un registro.";
            }
            

        }

        protected void ibtn_cerrar_Click(object sender, ImageClickEventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void ibtn_documentos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmListaDocumentos.aspx");
        }

        protected void grv_Usuarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Skip the header and footer rows
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Attributes.Add("onmouseover", "grillaMouseOver(this,'overFila');");
                    e.Row.Attributes.Add("onmouseout", "grillaMouseOut(this,'outFilaalt');");
                }
                else
                {

                    e.Row.Attributes.Add("onmouseover", "grillaMouseOver(this,'overFila');");
                    e.Row.Attributes.Add("onmouseout", "grillaMouseOut(this,'outFiladef');");
                }

            }
        }


        protected void grv_Usuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strCodigo;
            string strNombre;
            string strTipoTrabajador;
            string strNumeroDocumento;
            string strUsuario;
            string strActivo;
            string strCambioPassword;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strCodigo = ((Label)e.Row.Cells[1].FindControl("lbl_Codigo")).Text.Trim().ToString();
                strNombre = ((Label)e.Row.Cells[2].FindControl("lbl_Nombre")).Text.Trim().ToString();
                strTipoTrabajador = ((HiddenField)e.Row.Cells[3].FindControl("hdn_TipoColaborador")).Value.Trim().ToString();
                strNumeroDocumento = ((Label)e.Row.Cells[4].FindControl("lbl_NumDocumentoIdentidad")).Text.Trim().ToString();
                strUsuario = ((Label)e.Row.Cells[5].FindControl("lbl_Usuario")).Text.Trim().ToString();
                strActivo = ((HiddenField)e.Row.Cells[6].FindControl("hdn_Estado")).Value.Trim().ToString();
                strCambioPassword = ((HiddenField)e.Row.Cells[7].FindControl("hdn_CambioPassword")).Value.Trim().ToString();

                ((RadioButton)e.Row.Cells[0].FindControl("rad_sel")).Attributes.Add("OnClick", "return CargaDetalle(this, '" + strCodigo + "','" + strNombre + "','" + strTipoTrabajador + "','"  + strNumeroDocumento + "','" + strUsuario + "','" +  strActivo + "','"  + strCambioPassword  + "');");

            }

        }


        protected void grv_Usuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView obj_Grid = ((GridView)(sender));
            obj_Grid.PageIndex = e.NewPageIndex;

            ListarUsuarios();

        }


        private void Limpiar()
        {
            txt_Codigo.Text = string.Empty;
            txt_Nombre.Text = string.Empty;
            txt_NumDoc.Text = string.Empty;
            txt_Usuario.Text = string.Empty;
            hdn_cod.Value = string.Empty;
            ddl_TipoTrabajador.SelectedIndex = 0;
            chkActivo.Checked = false;
            chkGenerarClave.Checked = false;

            //ListarUsuarios();

        }

    }
}