<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmListaDocumentos.aspx.cs" Inherits="WebDigitalFile.frmListaDocumentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="Width=device-width, initial-scale=1, shrink-to-fit=no"/>
<meta http-equiv="x-ua-compatible" content="ie-edge"/>
<title>Usuarios</title>
<link href="css/bootstrap.css" rel="stylesheet" />
<link href="css/bootstrap.min.css" rel="stylesheet" />
<link href="css/Style.css" rel="stylesheet" />
    </head>
<body class="body2">
    <form id="frm_ListaDocumentos" runat="server">
        <div class="row">
            <div class="col-12" >
                <img  src="Images/prodac.png"  />
             </div>
        </div>
        <div id="div_toolbar" class="row" >
               
                <div class="col-1">
                     <div>
                          <% if (Session["TipoValidacion"].ToString() != "LDAP") { %>
                             &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_Password" runat="server" ImageUrl="~/Images/password.png"  style="cursor:pointer;" OnClick="ibtn_Password_Click" Width="20px" Height="20px" />
                          <%} %>
                    </div>
                    <div>
                          <% if (Session["TipoValidacion"].ToString() != "LDAP") { %>
                                <asp:Label ID="Label1" style="color:blue"  runat="server" Text="Cambiar Password" ></asp:Label>
                          <%} %>
                    </div>                
                </div>
             <div class="col-1" ></div>
                <div class="col-1"></div>
                <div class="col-1"></div>
                <div class="col-6"></div>
                  <div class="col-1">
                     <div>
                          <% if (Session["Perfil"].ToString() == "A") { %>
                            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_Usuarios" runat="server" ImageUrl="~/Images/Usuario.png"  style="cursor:pointer;" OnClick="ibtn_Usuarios_Click" Width="20px" Height="20px" />
                          <%} %>
                    </div>
                    <div>
                          <% if (Session["Perfil"].ToString() == "A") { %>
                                <asp:Label ID="Label5" style="color:blue"  runat="server" Text="Usuarios" ></asp:Label>
                          <%} %>
                    </div>                
                </div>
                <div class="col-1">
                     <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_cerrar" runat="server" ImageUrl="~/Images/exit.png"  style="cursor:pointer;" OnClick="ibtn_cerrar_Click"/>
                    </div>
                    <div>
                         <asp:Label ID="Label4" style="color:blue"  runat="server" Text="Cerrar Sesión" ></asp:Label>
                    </div>                
                </div>
        </div>
       
        <div class="row">
           <div class="col-1" ></div>
           <div class="col-11" >
               <div>
                &nbsp;&nbsp;<asp:GridView ID="grv_Documentos" runat="server" 
                    CssClass="gridview" 
                    GridLines="None"
                    AllowPaging="True"
                    AutoGenerateColumns="False" 
                    PagerStyle-CssClass="pgr"
                    RowStyle-CssClass="def"
                    AlternatingRowStyle-CssClass="alt"
                    OnRowCreated="grv_Documentos_RowCreated"  
                    OnRowDataBound="grv_Documentos_RowDataBound"
                    onPageIndexChanging ="grv_Documentos_PageIndexChanging" PageSize="25">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Año">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Anio" runat="server" Text='<%# Eval("Anio") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Periodo">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text='<%# Eval("MES_LITERAL") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_Mes" runat="server" Value='<%# Eval("MES") %>' />  
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Documento">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Documento" runat="server" Text='<%# Eval("NOMBRE_ARCHIVO") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_LinkDocumento" runat="server" Value='<%# Eval("RUTA") %>' />  
                            </ItemTemplate>
                            <ItemStyle Width="300px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descargar">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" Text = "Descargar" ImageUrl="~/Images/icono_download.png" runat="server" CommandArgument = '<%# Eval("RUTA") %>' OnClick = "DownloadFile" ImageWidth="20px" ImageHeight="20px" Height="20px" Width="20px" />
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Visualizar">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("RUTA") %>' Target="_new" Text='<%# Eval("NOMBRE_ARCHIVO") %>' ImageUrl="~/Images/search-icon.png" ImageWidth="20px" ImageHeight="20px"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="def"></RowStyle>
                </asp:GridView>
                   
              </div>
          </div>
           
       </div> 
         
    </form>

    <div id="div_footer">
         <div class="row">
             <div  class="col-3"  style="border-style: inset outset outset inset; border-width: thin">
                <img src="Images/Usuario.png" style="padding: 3px; width: 24px; height: 24px" />
                <asp:Label ID="lblUsuario"  runat="server" Text="Mario chau" Font-Size="12px" ForeColor="#000099" ></asp:Label>
             </div>
             <div  class="col-2" style="border-style: inset outset outset inset; border-width: thin"  >
                 <asp:Label ID="lblPerfil"   runat="server" Text="Trabajador" Font-Size="12px" ForeColor="#000099"></asp:Label>
             </div>

             <div  class="col-7" style="border-style: inset outset outset inset; border-width: thin" >
                 <img runat="server" id="ImagenAlerta" src="Images/info.gif" style="padding: 3px; width: 24px; height: 24px" />
                <asp:Label ID="lblMensaje"   runat="server" Text="" Font-Size="12px" ForeColor="#000099"></asp:Label>

             </div>
             </div>
     </div>

<script src="Scripts/JBasic.js"></script>
<script src="Scripts/jquery-3.3.1.min.js"></script>
<script src="Scripts/popper.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
