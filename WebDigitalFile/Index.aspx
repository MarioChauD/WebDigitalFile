<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebDigitalFile.Index" %>

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
    <form id="form1" runat="server">
        <div class="row">
            <div class="col" >
                <asp:Label ID="lblTitulo" class="float-left" runat="server" Text="Documentos Digitales" Font-Size="Large" Font-Bold="True" ForeColor="#FF9933" CssClass="text-deco"></asp:Label>
                <asp:Label ID="lblUsuario" class="float-right" runat="server" Text="" Font-Size="Large" Font-Bold="True" ForeColor="#FF9933"></asp:Label>
             </div>
        </div>
        <div class="row">
            <div class="col" >
                <nav class="navbar navbar-expand bg-primary">
                    <ul class="navbar-nav">
                        <% if (Session["Perfil"].ToString() == "A") { %>
                        <li class="nav-item"><a href="frmGestionUsuarios.aspx" style="color:aqua" class="nav-link">Gestión de usuarios</a></li>
                        <%} %>
                        <li class="nav-item"><a href="frmListaDocumentos.aspx" style="color:aqua" class="nav-link">Lista de Documentos</a></li>
                        <li class="nav-item"><a href="frmLogin.aspx" style="color:aqua" class="nav-link" >Salir</a></li>
                    </ul>
                </nav>
            </div>
       </div>
    </form>
<script src="Scripts/jquery-3.3.1.min.js"></script>
<script src="Scripts/popper.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
