<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Arquesoft</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" >
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" ></script>
    <script src="https://code.jquery.com/jquery-3.6.0.slim.min.js"></script>
    <link href="Recursos/CSS/estilos.css" rel="stylesheet" />
    console.log('Holaaaaaaaaa');
</head>
<body class="bg-light">
    <div class="wrapper">
        <div class="formcontent">
           <form id="formulario_login" runat="server">
                <div class="form-control">
                    <div class="col-md-6 text-center mb-5">
                        <asp:label class="h3" ID="lblBienvenida" runat="server" text="Formulario login"></asp:label>
                    </div>
                    <div>
                        <asp:label  ID="LabelUsuario" runat="server" text="Usuario: "></asp:label>
                        <asp:TextBox ID="tbUsuario" CssClass="form-control" runat="server" placeholder="Nombre de usuario"></asp:TextBox>
                    </div>
                    <div>
                        <asp:label  ID="LabelPassword" runat="server" text="Password: "></asp:label>
                        <asp:TextBox ID="tbPassword" CssClass="form-control" runat="server" placeholder="Password"></asp:TextBox>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button  ID="btIngresar" CssClass="btn btn-primary btn-dark" runat="server" text="Ingresar" OnClick="BtnIngresar_Click" /> 
                    </div>
                </div>
           </form>
        </div>
     </div</body>
</html>
