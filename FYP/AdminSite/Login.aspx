<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FYP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="multiView" runat="server">
                <asp:View ID="loginView" runat="server">
                    <asp:Label ID="statusLabel" runat="server" ForeColor="Red"></asp:Label><br />
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox><br />
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox><br />
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /><br />
                </asp:View>
                <asp:View ID="twoFactorView" runat="server">
                    <asp:Image ID="barcodeImage" runat="server" Height="150px" Width="150px" />
                    <asp:Label ID="setupCodeLabel" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtToken" runat="server" placeholder="Token"></asp:TextBox><br />
                    <asp:Button ID="btnAuthenticate" runat="server" Text="Authenticate" OnClick="btnAuthenticate_Click" />
                    <asp:Button ID="btnEmailOTP" runat="server" Text="Email OTP" OnClick="btnEmailOTP_Click" /><br />
                </asp:View>
                <asp:View ID="emailOTPView" runat="server">
                    <asp:TextBox ID="txtEmailOTP" runat="server" placeholder="Email OTP"></asp:TextBox><br />
                    <asp:Button ID="btnVerifyEmailOTP" runat="server" Text="Verify Email OTP" OnClick="btnVerifyEmailOTP_Click" />
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
