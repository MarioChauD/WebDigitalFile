<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebDigitalFile.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="Width=device-width, initial-scale=1"/>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/Style.css" rel="stylesheet" type="text/css"/>
    <title>Plataforma de Documentos Electrónicos</title>
</head>
<body>
   <div class="container">
      <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                 <h1 class="TituloApp text-center">Plataforma de Documentos Electrónicos</h1>
            </div>
          <div class="col-md-2"></div>
       </div>
       <div class="row">
           <div class="col-md-4">
           </div>
            <div class="col-md-4">
                <form id="loginForm"  runat="server" style="padding-top: 100px">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Ingresar.PNG" OnClick="ImageButton1_Click" />
                </form>
            </div>
            <div class="col-md-4">
            </div>
       </div>


     </div>
</body>
</html>
