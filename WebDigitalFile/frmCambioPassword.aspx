<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCambioPassword.aspx.cs" Inherits="WebDigitalFile.frmCambioPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="Width=device-width, initial-scale=1"/>

    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Style.css" rel="stylesheet" type="text/css"/>
    <title>Cambio de Contraseña</title>

</head>
<body>
<div class="container">
        <div class="row">
            <div class="col-md-12">
                 <h1 class="TituloApp text-center">Sistema de Documentos Electrónicos</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4 col-md-4P">
                <h1 class="TituloLogin text-center">Cambio de Contraseña</h1>
                <form id="loginForm"  runat="server" novalidate="novalidate">
                    <div class="form-group" >
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" required="" title="Ingrese su Contraseña" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" required=""  title="Ingrese Nueva Contraseña"  placeholder="Nueva Contraseña" TextMode="Password" ></asp:TextBox>
                        <asp:TextBox ID="txtNewPassword2" runat="server" class="form-control" required=""  title="Repita Nueva Contraseña" placeholder="Nueva Contraseña" TextMode="Password"></asp:TextBox>
                        <asp:Button ID="btnCambioPassword" runat="server" Text="Actualizar" CssClass="btn btn-primary btn-block" OnClick="btnCambioPassword_Click"/>
                        <asp:Button ID="btnSalir"  runat="server" Text="Salir" class="btn btn-secondary btn-block"  OnClick="btnSalir_Click" />
                        <br />
                        <asp:Label class="float-left" style="color:red" ID="lblMensaje" runat="server" Text="" Font-Size="20px"></asp:Label>
                        <br />
                    </div>
                </form>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
            
<script src="Scripts/jquery-3.3.1.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
