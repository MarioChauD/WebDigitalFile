<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="WebDigitalFile.frmUsuario" %>

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
<body style="background-image: url('Images/Fondo_2.jpg');">

<form id="UsuarioForm"  runat="server" novalidate="novalidate">
 <div class="container border"  style="height:500px; top: 100px;">
    
   

    <div class="row justify-content-between " >
        <div class="col" >
                <asp:Label ID="lblTitulo" class="float-left" runat="server" Text="Gestión de Usuarios" Font-Size="Large" Font-Bold="True" ForeColor="#FF9933"></asp:Label>
                <asp:Label ID="lblUsuario" class="float-right" runat="server" Text="" Font-Size="Large" Font-Bold="True" ForeColor="#FF9933"></asp:Label>
             </div>
    </div>
    <div class="row">
        <div class="col-12 col-sm-3 col-md-3">
            <label for="username" class="control-label">Usuario</label>
        </div>
        <div class="col-12 col-sm-3 col-md-3">
              <asp:TextBox ID="txtUsuario" runat="server" class="form-control" required=""  title="Ingrese su usuario" placeholder=""></asp:TextBox>
        </div>
        <div class="col-12 col-sm-3 col-md-3">
            <label for="password" class="control-label">Contraseña</label>
        </div>
        <div class="col-12 col-sm-3 col-md-3">
              <asp:TextBox ID="txtPassword" runat="server" class="form-control" required="" title="Ingrese su Contraseña" TextMode="Password"></asp:TextBox>
        </div>
     </div>
    <div class="row">
          <div class="col-12">
           <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-success btn-block"  />
        </div>
    </div>
  </div>
</form>
<script src="Scripts/jquery-3.3.1.min.js"></script>
<script src="Scripts/popper.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
