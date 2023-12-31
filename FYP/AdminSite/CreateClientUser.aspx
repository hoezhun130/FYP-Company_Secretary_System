﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateClientUser.aspx.cs" Inherits="FYP.AdminSite.CreateClientUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblName" runat="server" Text="Client User's Name"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label>
            <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblICNumber" runat="server" Text="IC Number"></asp:Label>
            <asp:TextBox ID="txtICNumber" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblContactNumber" runat="server" Text="Contact Number"></asp:Label>
            <asp:TextBox ID="txtContactNumber" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblCreatedBy" runat="server" Text="Created By"></asp:Label>
            <asp:TextBox ID="txtCreatedBy" runat="server"></asp:TextBox>
            <br />


            <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
        </div>
    </form>
</body>
</html>
