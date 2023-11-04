<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateClientAdmin.aspx.cs" Inherits="FYP.AdminSite.CreateClientAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div>
            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblRegistrationNumber" runat="server" Text="Registration Number"></asp:Label>
            <asp:TextBox ID="txtRegistrationNumber" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblPhysicalAddress" runat="server" Text="Physical Address"></asp:Label>
            <asp:TextBox ID="txtPhysicalAddress" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblPICName" runat="server" Text="Person in Charge (PIC) Name"></asp:Label>
            <asp:TextBox ID="txtPICName" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="lblPICContactNumber" runat="server" Text="PIC Contact Number"></asp:Label>
            <asp:TextBox ID="txtPICContactNumber" runat="server"></asp:TextBox>
            <br />
            
            <asp:Label ID="lblTotalNumberOfBOD" runat="server" Text="Total Number of BOD"></asp:Label>
            <asp:TextBox ID="txtTotalNumberOfBOD" runat="server"></asp:TextBox>
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
