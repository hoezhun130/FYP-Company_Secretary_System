<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantAdminPage.aspx.cs" Inherits="FYP.TenantAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tenant Admin Page</title>

    <!--Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">


    <!-- jQuery, Popper.js, and Bootstrap JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        .input-control {
            width: 200px; /* Set the width as needed */
            height: 30px; /* Set the height as needed */
            font-size: 16px; /* Set the font size as needed */
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

    <form id="form1" runat="server">
        <div class="container mt-3 mx-lg-5">
            <h3 class="text-left">
                <i class="fas fa-user mr-2"></i><b>User Account</b>
            </h3>
                        
            <br /><br />
            <!-- Client Admin Management Section -->
            <h2>Manage Tenant User</h2>
            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CA_ID" DataSourceID="SqlDataSource3" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:BoundField DataField="CA_ID" HeaderText="CA_ID" InsertVisible="False" ReadOnly="True" SortExpression="CA_ID" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="RegistrationNumber" HeaderText="RegistrationNumber" SortExpression="RegistrationNumber" />
                    <asp:BoundField DataField="PhysicalAddress" HeaderText="PhysicalAddress" SortExpression="PhysicalAddress" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="TotalNumberOfBOD" HeaderText="TotalNumberOfBOD" SortExpression="TotalNumberOfBOD" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="TA_ID" HeaderText="TA_ID" SortExpression="TA_ID" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" SelectCommand="SELECT * FROM [ClientAdmin]">
            </asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Create Client Admin" OnClick="Button1_Click" CssClass="btn btn-primary"/>
            

            <!-- Tenant User Management Section -->
            <br /><br /><br />
            <h2>Manage Client Admin</h2>
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TU_ID" DataSourceID="SqlDataSource2" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="TU_ID" HeaderText="TU_ID" InsertVisible="False" ReadOnly="True" SortExpression="TU_ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="TA_ID" HeaderText="TA_ID" SortExpression="TA_ID" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" DeleteCommand="DELETE FROM [TenantUser] WHERE [TU_ID] = @TU_ID" InsertCommand="INSERT INTO [TenantUser] ([Name], [Position], [Role], [ContactNumber], [Email], [Password], [TA_ID]) VALUES (@Name, @Position, @Role, @ContactNumber, @Email, @Password, @TA_ID)" SelectCommand="SELECT * FROM [TenantUser]" UpdateCommand="UPDATE [TenantUser] SET [Name] = @Name, [Position] = @Position, [Role] = @Role, [ContactNumber] = @ContactNumber, [Email] = @Email, [Password] = @Password, [TA_ID] = @TA_ID WHERE [TU_ID] = @TU_ID">
                <DeleteParameters>
                    <asp:Parameter Name="TU_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                    <asp:Parameter Name="TU_ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:Button ID="Button2" runat="server" Text="Create Tenant User" OnClick="Button2_Click" CssClass="btn btn-primary"/>
            
        </div>
    </form>
</body>
</html>
