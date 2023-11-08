<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientAdminPage.aspx.cs" Inherits="FYP.ClientAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Admin Page</title>

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
            <h2>Manage Client Admin</h2>

            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CU_ID" DataSourceID="SqlDataSource1" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CU_ID" HeaderText="CU_ID" InsertVisible="False" ReadOnly="True" SortExpression="CU_ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="ICNumber" HeaderText="ICNumber" SortExpression="ICNumber" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="CA_ID" HeaderText="CA_ID" SortExpression="CA_ID" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" DeleteCommand="DELETE FROM [ClientUser] WHERE [CU_ID] = @CU_ID" InsertCommand="INSERT INTO [ClientUser] ([Name], [Position], [CompanyName], [ICNumber], [ContactNumber], [Email], [Password], [CA_ID]) VALUES (@Name, @Position, @CompanyName, @ICNumber, @ContactNumber, @Email, @Password, @CA_ID)" SelectCommand="SELECT * FROM [ClientUser]" UpdateCommand="UPDATE [ClientUser] SET [Name] = @Name, [Position] = @Position, [CompanyName] = @CompanyName, [ICNumber] = @ICNumber, [ContactNumber] = @ContactNumber, [Email] = @Email, [Password] = @Password, [CA_ID] = @CA_ID WHERE [CU_ID] = @CU_ID">
                <DeleteParameters>
                    <asp:Parameter Name="CU_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Position" Type="String" />
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="ICNumber" Type="String" />
                    <asp:Parameter Name="ContactNumber" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                    <asp:Parameter Name="CU_ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Client User" OnClick="Button1_Click" CssClass="btn btn-primary"/>
            <br />
            
        </div>
    </form>
</body>
</html>
