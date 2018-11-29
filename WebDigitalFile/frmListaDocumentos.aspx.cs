using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DigitalFile_BE;
using DigitalFile_BL;
using System.Data;


namespace WebDigitalFile
{
    public partial class frmListaDocumentos : System.Web.UI.Page
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

                ListarDocumentos();

                LeerDocumentos();
                
            }

        }


        private void ListarDocumentos()
        {
            DocumentosBL oDocumentoBL = new DocumentosBL();
            DataTable oDataTable = new DataTable();
            int CantidadFilas = 0;

            string sCodigoUsuario = Session["Usuario"].ToString();
            string sNumDocumento = Session["NumeroDocumento"].ToString();

            oDataTable = oDocumentoBL.getDocumentos(sNumDocumento);

            grv_Documentos.DataSource = oDataTable;
            grv_Documentos.DataBind();

            CantidadFilas = oDataTable.Rows.Count;

            lblMensaje.ForeColor = System.Drawing.Color.Blue;
            lblMensaje.Text = "Se encontraron " + CantidadFilas.ToString() + " Registros.";


        }

        private void LeerDocumentos()
        {
            string NumDocumento = string.Empty;
            string sPathOrigen = string.Empty;
            string sPathDestino = string.Empty;

            ObtenerRutas(ref sPathOrigen, ref sPathDestino);
            NumDocumento = Session["NumeroDocumento"].ToString();

            //DirectoryInfo oDirectorio_Privado = new DirectoryInfo("S:\\MChau\\DocumentosDigitales");
            //DirectoryInfo oDirectorio_Publico = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Descarga_Documentos"));

            DirectoryInfo oDirectorio_Privado = new DirectoryInfo(HttpContext.Current.Server.MapPath(sPathOrigen));
            DirectoryInfo oDirectorio_Publico = new DirectoryInfo(HttpContext.Current.Server.MapPath(sPathDestino));


            foreach (var oFile_O in oDirectorio_Privado.GetFiles(NumDocumento +'*'))
            {
                    oFile_O.CopyTo(oDirectorio_Publico.ToString() + "\\" + oFile_O.Name, true);

            }

        }


        private void ObtenerRutas(ref string sPathOrigen , ref string sPathDestino)
        {
            ConfiguracionBL oConfiguracionBL = new ConfiguracionBL();
            ConfiguracionBE oConfiguracionBE = new ConfiguracionBE();

            oConfiguracionBE = oConfiguracionBL.getConfiguracion();

            sPathOrigen = oConfiguracionBE.Directorio_Origen;
            sPathDestino = oConfiguracionBE.Directorio_Destino;

        }

        protected void ibtn_Usuarios_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmGestionUsuarios.aspx");
        }

        protected void ibtn_cerrar_Click(object sender, ImageClickEventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }

        protected void ibtn_Password_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmCambioPassword.aspx");
        }
             

        protected void grv_Documentos_RowCreated(object sender, GridViewRowEventArgs e)
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


        protected void grv_Documentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string strCodigo;
            //string strNombre;
            //string strTipoTrabajador;
            //string strNumeroDocumento;
            //string strUsuario;
            //string strActivo;
            //string strCambioPassword;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    strCodigo = ((Label)e.Row.Cells[1].FindControl("lbl_Codigo")).Text.Trim().ToString();
            //    strNombre = ((Label)e.Row.Cells[2].FindControl("lbl_Nombre")).Text.Trim().ToString();
            //    strTipoTrabajador = ((HiddenField)e.Row.Cells[3].FindControl("hdn_TipoColaborador")).Value.Trim().ToString();
            //    strNumeroDocumento = ((Label)e.Row.Cells[4].FindControl("lbl_NumDocumentoIdentidad")).Text.Trim().ToString();
            //    strUsuario = ((Label)e.Row.Cells[5].FindControl("lbl_Usuario")).Text.Trim().ToString();
            //    strActivo = ((HiddenField)e.Row.Cells[6].FindControl("hdn_Estado")).Value.Trim().ToString();
            //    strCambioPassword = ((HiddenField)e.Row.Cells[7].FindControl("hdn_CambioPassword")).Value.Trim().ToString();

            //    ((RadioButton)e.Row.Cells[0].FindControl("rad_sel")).Attributes.Add("OnClick", "return CargaDetalle(this, '" + strCodigo + "','" + strNombre + "','" + strTipoTrabajador + "','" + strNumeroDocumento + "','" + strUsuario + "','" + strActivo + "','" + strCambioPassword + "');");

            //}

        }


        protected void grv_Documentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView obj_Grid = ((GridView)(sender));
            obj_Grid.PageIndex = e.NewPageIndex;

            ListarDocumentos();

        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as ImageButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }


    }
}