<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGestionUsuarios.aspx.cs" Inherits="WebDigitalFile.frmGestionUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="Width=device-width, initial-scale=1"/>
    
        <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="css/Style.css" rel="stylesheet" type="text/css"/>
       

        <title>Gestión de Usuarios</title>

        <script type="text/javascript"  lang="jscript"  >

         function SoloNumeros(evt) {

             evt = (evt) ? evt : window.event
             var charCode = (evt.which) ? evt.which : evt.keyCode
             if (charCode > 31 && ((charCode < 48 && charCode != 46) || charCode > 57)) {
                 return false;
             }
             else {
                 return true;
             }
         }


        function UnCheckedByGVID(gridname) {
            $("#" + gridname).find('tr').each(function () {
                var tds = $(this).find("td");
                $(tds[0]).find("input:radio").removeAttr("checked");
            });

            return false;

        }

        function LimpiarControles() {

            $("#hdn_cod").val('');
            $("#txt_Codigo").removeAttr("disabled");
            $("#txt_Codigo").val('');
            $("#txt_Nombre").removeAttr("disabled");
            $("#txt_Nombre").val('');
            $("#ddl_TipoTrabajador").removeAttr("disabled");
            $("#ddl_TipoTrabajador")[0].selectedIndex = 0;
            $("#txt_NumDoc").removeAttr("disabled");
            $("#txt_NumDoc").val('');

            $("#txt_Usuario").val('');

            $("#chkActivo").prop("checked", false);
            $("#chkGenerarClave").prop("checked", false);

            $("#lblMensaje").html('');

            $("#ImagenAlerta").attr("src", "Images/info.gif");
            
            $("#txt_Nombre").focus();

            UnCheckedByGVID('grv_Usuarios');

         
            return false;
        }

        function ActivarDescativarCamposResultado(blnActivar) {

            if (blnActivar == false) {
                document.getElementById('ctl00_ContentPlaceHolder1_lblSinResultado').style.visibility = "hidden";

            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_lblSinResultado').style.visibility = "visible";
            }

            document.getElementById('div_detalle').style.display = !blnActivar ? "block" : "none";
            document.getElementById('div_detalle').style.visibility = !blnActivar ? "visible" : "hidden";

            return false;

        }


        function CargaDetalle(objRadio, Codigo, Nombre, TipoTrabajador, NumeroDoc, Usuario, Activo, CambioPassword ) {


            $("#hdn_cod").val(Codigo);
            $("#txt_Codigo").val(Codigo);
            $("#txt_Nombre").val(Nombre);
            $("#txt_NumDoc").val(NumeroDoc);
            $("#txt_Usuario").val(Usuario);

            $("#ddl_TipoTrabajador").val(TipoTrabajador).attr("selected", "selected");

            $("#chkActivo").prop("checked", Activo == 'A' ? true : false);
            $("#chkGenerarClave").prop("checked", CambioPassword == 'S' ? true : false);

            $("#txt_Codigo").attr("disabled", "disabled");
            $("#txt_Nombre").attr("disabled", "disabled");
            $("#txt_NumDoc").attr("disabled", "disabled");
            $("#ddl_TipoTrabajador").attr("disabled", "disabled");
            
            CheckOtherIsCheckedByGVIDMore(objRadio, 'grv_Usuarios');

          //  $("#" + objRadio.id).attr("checked", 'checked');
            $("#" + objRadio.id).prop("checked", true);

          
            return true;
        }


           
     </script>

    </head>
    <body class="body2">
         <form id="frm_Listausuarios" runat="server">
             <div class="row" >
                <div class="col-12" >
                    <img  src="Images/prodac.png"  style="height:40px"  />
                </div>
            </div>
        
           

          <div id="div_toolbar" class="row" >
                <div class=".col-md-1" >
                    <div></div>
                    <div></div>
                </div>
                <div class="col-md-1" >
                    <div>
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_filtrar" runat="server" ImageUrl="~/Images/page_find.png" onclick="ibtn_filtrar_Click" style="height: 16px; cursor:pointer;" />
                    </div>
                    <div>
                        <asp:Label ID="Label1" style="color:blue"  runat="server" Text="Buscar"></asp:Label>
                    </div>
                </div>
                <div class="col-md-1">
                    <div>
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_nuevo" runat="server" onClientclick="return LimpiarControles();" ImageUrl="~/Images/erase.png" style="height: 16px; cursor:pointer;"/>
                    </div>
                    <div>
                        <asp:Label ID="Label2" style="color:blue"  runat="server" Text="Limpiar" ></asp:Label>
                    </div>
                </div>
                <div class="col-md-1">
                    <div>
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_grabar" runat="server" ImageUrl="~/Images/disk.png" onclick="ibtn_grabar_Click" style="height: 16px; cursor:pointer;"/>
                    </div>
                    <div>
                         <asp:Label ID="Label3" style="color:blue"  runat="server" Text="Guardar" ></asp:Label>
                    </div>
                </div>
                <div class="col-md-5">
                    <div></div>
                    <div></div>
                </div>
                <div class="col-md-1">
                     <div>
                        &nbsp&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_documentos" runat="server" ImageUrl="~/Images/report.gif"  style="cursor:pointer;" OnClick="ibtn_documentos_Click"/>
                    </div>
                    <div>
                         <asp:Label ID="Label5" style="color:blue"  runat="server" Text="Doc.Electrónico" ></asp:Label>
                    </div>                
                </div>
                <div class="col-md-1">
                     <div>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ibtn_cerrar" runat="server" ImageUrl="~/Images/exit.png"  style="cursor:pointer;" OnClick="ibtn_cerrar_Click"/>
                    </div>
                    <div>
                         <asp:Label ID="Label4" style="color:blue"  runat="server" Text="Cerrar Sesión" ></asp:Label>
                    </div>                
                </div>
                <div class="col-md-1" >
                    <div></div>
                    <div></div>
                </div>
        </div>
        <div id="div_filtro" class="row">
          <div class="clearfix visible-sm-block col-md-1 " >
              <div></div>
              <div></div>
          </div>
            <div class="clearfix visible-md-block col-sm-4 col-md-1" >
                    <div><asp:Label ID="Label6" style="color:blue"  runat="server" Text="Código"></asp:Label></div>
                    <div><asp:TextBox ID="txt_Codigo" runat="server" CssClass="textbox_100" Width="100%" MaxLength="15" onKeyPress=" return SoloNumeros(event)" />
                        <asp:HiddenField ID="hdn_cod" runat="server" />
                    </div>
                </div>
            <div class="clearfix visible-md-block col-sm-4 col-md-2" >
                        <div><asp:Label ID="Label7" style="color:blue"  runat="server" Text="Nombre"></asp:Label></div>
                        <div><asp:TextBox ID="txt_Nombre" runat="server" CssClass="textbox_100"  Width="100%" MaxLength="100"  /></div>
             </div>
            <div class="clearfix visible-md-block col-sm-4 col-md-2" >
                        <div><asp:Label ID="Label10" style="color:blue"  runat="server" Text="Tipo Trabajador"></asp:Label></div>
                        <div><asp:DropDownList ID="ddl_TipoTrabajador" runat="server" CssClass="dropdownlist" Width="90%" Height="23px" /></div>
             </div>

             <div class="clearfix visible-md-block col-sm-3 col-md-1" >
                        <div><asp:Label ID="Label9" style="color:blue"  runat="server" Text="DNI"></asp:Label></div>
                        <div><asp:TextBox ID="txt_NumDoc" runat="server" CssClass="textbox_100" Width="100%" MaxLength="10" onKeyPress=" return SoloNumeros(event)"  /></div>
             </div>
            <div class="clearfix visible-md-block col-sm-3 col-md-2" >
                <div><asp:Label ID="Label11" style="color:blue"  runat="server" Text="Usuario"></asp:Label></div>
                <div><asp:TextBox ID="txt_Usuario" runat="server" CssClass="textbox_100" Width="100%" MaxLength="50"   /></div>
             </div>
             <div class="clearfix visible-md-block col-sm-3 col-md-1" >
                        <div>&nbsp;</div>
                        <div><asp:CheckBox ID="chkActivo" runat="server" Font-Size="11px"  Text="  Activo" ForeColor="#003399" style="cursor:pointer;"/></div>
             </div>

             <div class="clearfix visible-md-block col-sm-3 col-md-2" >
                <div>&nbsp;</div>
                <div><asp:CheckBox ID="chkGenerarClave" runat="server" Font-Size="11px"  Text="  Cambiar Contraseña" ForeColor="#003399" style="cursor:pointer;"/></div>
             </div>
          
           
       </div>
       <div class="row">
           <div class="col-1" ></div>
           <div class="col-11" >
               <div>
                &nbsp;&nbsp;<asp:GridView ID="grv_Usuarios" runat="server" 
                    CssClass="gridview" 
                    GridLines="None"
                    AllowPaging="True"
                    AutoGenerateColumns="False" 
                    PagerStyle-CssClass="pgr"
                    RowStyle-CssClass="def"
                    AlternatingRowStyle-CssClass="alt"
                    OnRowCreated="grv_Usuarios_RowCreated"  
                    OnRowDataBound="grv_Usuarios_RowDataBound"
                    onPageIndexChanging ="grv_Usuarios_PageIndexChanging" PageSize="25">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="tdHFCenter">
                            <ItemTemplate>
                                <asp:RadioButton ID="rad_sel" runat="server" OnClick="CheckOtherIsCheckedByGVIDMore(this,'grv_Usuarios');" />
                                <asp:HiddenField ID="hdn_cod" runat="server" Value='<%# Eval("Codigo_SAP") %>' />                    
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Codigo" runat="server" Text='<%# Eval("Codigo_SAP") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text='<%# Eval("Nombre_Completo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="300px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Colaborador">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TipoColaborador" runat="server" Text='<%# Eval("Tipo_Colaborador_Des") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_TipoColaborador" runat="server" Value='<%# Eval("Tipo_Colaborador") %>' />  
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="N° Doc. Identidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_NumDocumentoIdentidad" runat="server" Text='<%# Eval("Numero_Doc_Identidad") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Usuario">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Usuario" runat="server" Text='<%# Eval("Usuario") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# Eval("Estado_Acceso_Des") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_Estado" runat="server" Value='<%# Eval("Estado_Acceso") %>' />  
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Cambio Contraseña">
                            <ItemTemplate>
                                <asp:Label ID="lbl_CambioPassword" runat="server" Text='<%# Eval("Estado_Cambio_Password_Des") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_CambioPassword" runat="server" Value='<%# Eval("Estado_Cambio_Password") %>' />  
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                    <RowStyle CssClass="def"></RowStyle>
                </asp:GridView>
                   
              </div>
          </div>
           
       </div>
     
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
        <script src="Scripts/bootstrap.min.js"></script>
     </form>
    </body>
</html>
