<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantAdminPage.aspx.cs" Inherits="FYP.TenantAdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CA_ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="CA_ID" HeaderText="CA_ID" InsertVisible="False" ReadOnly="True" SortExpression="CA_ID" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                    <asp:BoundField DataField="RegistrationNumber" HeaderText="RegistrationNumber" SortExpression="RegistrationNumber" />
                    <asp:BoundField DataField="PhysicalAddress" HeaderText="PhysicalAddress" SortExpression="PhysicalAddress" />
                    <asp:BoundField DataField="PICName" HeaderText="PICName" SortExpression="PICName" />
                    <asp:BoundField DataField="PICContactNumber" HeaderText="PICContactNumber" SortExpression="PICContactNumber" />
                    <asp:BoundField DataField="TotalNumberOfBOD" HeaderText="TotalNumberOfBOD" SortExpression="TotalNumberOfBOD" />
                    <asp:BoundField DataField="TA_ID" HeaderText="TA_ID" SortExpression="TA_ID" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecordManagementConnectionString %>" DeleteCommand="DELETE FROM [ClientAdmin] WHERE [CA_ID] = @CA_ID" InsertCommand="INSERT INTO [ClientAdmin] ([CompanyName], [RegistrationNumber], [PhysicalAddress], [PICName], [PICContactNumber], [TotalNumberOfBOD], [TA_ID]) VALUES (@CompanyName, @RegistrationNumber, @PhysicalAddress, @PICName, @PICContactNumber, @TotalNumberOfBOD, @TA_ID)" SelectCommand="SELECT * FROM [ClientAdmin]" UpdateCommand="UPDATE [ClientAdmin] SET [CompanyName] = @CompanyName, [RegistrationNumber] = @RegistrationNumber, [PhysicalAddress] = @PhysicalAddress, [PICName] = @PICName, [PICContactNumber] = @PICContactNumber, [TotalNumberOfBOD] = @TotalNumberOfBOD, [TA_ID] = @TA_ID WHERE [CA_ID] = @CA_ID">
                <DeleteParameters>
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="RegistrationNumber" Type="String" />
                    <asp:Parameter Name="PhysicalAddress" Type="String" />
                    <asp:Parameter Name="PICName" Type="String" />
                    <asp:Parameter Name="PICContactNumber" Type="String" />
                    <asp:Parameter Name="TotalNumberOfBOD" Type="Int32" />
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CompanyName" Type="String" />
                    <asp:Parameter Name="RegistrationNumber" Type="String" />
                    <asp:Parameter Name="PhysicalAddress" Type="String" />
                    <asp:Parameter Name="PICName" Type="String" />
                    <asp:Parameter Name="PICContactNumber" Type="String" />
                    <asp:Parameter Name="TotalNumberOfBOD" Type="Int32" />
                    <asp:Parameter Name="TA_ID" Type="Int32" />
                    <asp:Parameter Name="CA_ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="Button1" runat="server" Text="Create Client Admin" OnClick="Button1_Click"/>
            
            <asp:Button ID="Button2" runat="server" Text="Create Tenant User" OnClick="Button2_Click"/>
            
        </div>
    </form>
</body>
</html>
