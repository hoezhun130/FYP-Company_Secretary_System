<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantAdminPage.aspx.cs" Inherits="FYP.TenantAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Client Admin Management Section -->
            <h2>Manage Client Admins</h2>
            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CA_ID" DataSourceID="SqlDataSource3">
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
            <asp:Button ID="Button1" runat="server" Text="Create Client Admin" OnClick="Button1_Click"/>
            

            <!-- Tenant User Management Section -->
            <h2>Manage Tenant Users</h2>
            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TU_ID" DataSourceID="SqlDataSource2">
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

            <asp:Button ID="Button2" runat="server" Text="Create Tenant User" OnClick="Button2_Click"/>
            
        </div>
    </form>
</body>
</html>
