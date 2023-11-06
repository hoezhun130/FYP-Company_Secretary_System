<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FYP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <title></title>
    <style>
        body {
            background-color: #ACC4B7; /* Set the background color to white */
        }
    </style>
</head>

<body>
    <div class="container mt-3" style="max-width: 500px; padding-top: 50px; padding-bottom: 100px;">
    <form id="form1" runat="server" class="container mt-3" style="border-radius: 10px;">


        <div class="card p-3">
            <img src="images/authenticator-icon.png" class="img-fluid mx-auto d-block mb-3" style="max-width: 150px; height: auto; padding-top:30px;" />
            <h1 class="text-center">Authenticator</h1>
            <p class="text-center">Please enter your account details.</p>
        <div>

            <asp:MultiView ID="multiView" runat="server" >  


                
                <asp:View ID="loginView" runat="server">

                    <asp:Label ID="statusLabel" runat="server" ForeColor="Red"></asp:Label><br />

                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                        </div>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control email-icon"></asp:TextBox><br />
                    </div><br />
                    
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                        </div>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control password-icon"></asp:TextBox><br />
                    </div><br />

                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary btn-block" /><br />
                </asp:View>
                


                <asp:View ID="twoFactorView" runat="server">
                    <asp:Image ID="barcodeImage" runat="server" Height="150px" Width="150px" CssClass="img-fluid mx-auto d-block mb-3"/>
<%--                    <asp:Label ID="setupCodeLabel" runat="server"></asp:Label><br />--%>
                    <asp:TextBox ID="txtToken" runat="server" placeholder="Token" CssClass="form-control"></asp:TextBox><br />
                    <asp:Button ID="btnAuthenticate" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" CssClass="btn btn-success btn-block"/>
                    <asp:Button ID="btnEmailOTP" runat="server" Text="Email OTP" OnClick="btnEmailOTP_Click" CssClass="btn btn-primary btn-block"/><br />
                </asp:View>

                <asp:View ID="emailOTPView" runat="server">
                    <asp:TextBox ID="txtEmailOTP" runat="server" placeholder="Email OTP" CssClass="form-control"></asp:TextBox><br />
                    <asp:Button ID="btnVerifyEmailOTP" runat="server" Text="Verify Email OTP" OnClick="btnVerifyEmailOTP_Click" CssClass="btn btn-primary btn-block"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary btn-block"/>

                </asp:View>


            </asp:MultiView>
        </div>
    
                </div>
     
    </form>
        </div>

</body>
</html>
