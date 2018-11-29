<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="WebDigitalFile.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="Width=device-width, initial-scale=1"/>

    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Style.css" rel="stylesheet" type="text/css"/>
    <title>Validación de Usuario</title>

       <script>
// Example starter JavaScript for disabling form submissions if there are invalid fields
(function() {
  'use strict';
      window.addEventListener('load', function() {
        var form = document.getElementById('needs-validation');
        form.addEventListener('submit', function(event) {
          if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
          }
          form.classList.add('was-validated');
        }, false);
      }, false);
    })();
</script>    

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
                <h1 class="TituloLogin text-center">Inicio de Sesión</h1>
                <form id="loginForm"  runat="server">
                    <div class="form-group">
                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control" required=""  title="Ingrese su usuario" placeholder="Usuario"></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" required="" title="Ingrese su Contraseña" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:CheckBox  ID="chkRecordarCredenciales" runat="server" Text="&nbsp; Recordar Credenciales?" Font-Bold="True" Font-Size="11pt" ForeColor="White"  />
                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary btn-block" OnClick="btnLogin_Click"/>
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
