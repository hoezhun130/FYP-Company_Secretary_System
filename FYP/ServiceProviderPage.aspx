<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceProviderPage.aspx.cs" Inherits="FYP.ServiceProviderPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:GridView ID="gvTenantAdmins" runat="server" AutoGenerateColumns="False" DataKeyNames="TA_ID" OnRowEditing="gvTenantAdmins_RowEditing" OnRowUpdating="gvTenantAdmins_RowUpdating" OnRowCancelingEdit="gvTenantAdmins_RowCancelingEdit" OnRowDeleting="gvTenantAdmins_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="TA_ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="TA_ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" SortExpression="ContactNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
