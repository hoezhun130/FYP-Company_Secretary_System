<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTenantAdmin.aspx.cs" Inherits="FYP.AdminSite.CreateTenantAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Tenant Admin Page</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        body {
            background-color: #ACC4B7; /* Set the background color to white */
        }
    </style>
</head>
<body>

    <%--Headers--%>
    <div class="container-fluid bg-dark text-white">
        <div class="row">
            <div class="col-6">
                <h3 class="p-3">Record Management System</h3>
            </div>
            <div class="col-6 text-right p-3">
                <button class="btn btn-light">
                    <i class="fas fa-user mr-2"></i>Tenant
                </button>
            </div>
        </div>
    </div>


     <div class="container mt-3 card p-3 mb-4" style="max-width: 500px;">
    <form id="formCreateTenantAdmin" runat="server" class="container mt-3 needs-validation" style="border-radius: 10px;">
  

            <h3 class="text-left">
                <i class="fas fa-user mr-2 mb-3"></i><b>Add Tenant User Account</b>
            </h3>

            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label>
            <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
            <asp:TextBox ID="txtRole" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Label ID="lblContactNumber" runat="server" Text="Contact Number"></asp:Label>
            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <br />

            <asp:Label ID="lblCreatedBy" runat="server" Text="Created By"></asp:Label>
            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" CssClass="btn btn-primary"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary"/>

        
    </form>
         </div>
</body>
</html>
