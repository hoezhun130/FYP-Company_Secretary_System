<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantDocumentList.aspx.cs" Inherits="FYP.UserSite.TenantDocumentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="RepeaterDocuments" runat="server">
            <ItemTemplate>
                <div>
                    <%# Eval("DocumentName") %>
                    <!-- Other document properties can go here -->
                    <a href='<%# GetDocumentUrl(Eval("DocumentName").ToString()) %>' target="_blank">View Document</a>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>
        <asp:Label ID="lblNoDocuments" runat="server" Text="There are no documents." Visible="false"></asp:Label>
        <br />
        <asp:Button ID="btnUploadDocument" runat="server" Text="Upload Document" OnClick="btnUploadDocument_Click"/>
    </form>
</body>
</html>
