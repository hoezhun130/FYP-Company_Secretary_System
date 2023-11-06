<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantUploadDocument.aspx.cs" Inherits="FYP.UserSite.TenantUploadDocument" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="uploadForm" runat="server">
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblUploadDocument" runat="server" Text="Upload Document:" />
            <asp:FileUpload ID="FileUploadDocument" runat="server" />
            <br />
            <asp:Label ID="lblUploadedBy" runat="server" Text="Uploaded By:" />
            <asp:TextBox ID="txtUploadedBy" runat="server" ReadOnly="true" />
            <br />
            <asp:Label ID="lblDate" runat="server" Text="Date:" />
            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Text='<%# DateTime.Now.ToShortDateString() %>' />
            <br />
            <asp:Label ID="lblViewDocs" runat="server" Text="View Permissions:" />
            <asp:DropDownList ID="ddlViewPermissions" runat="server" />
            <br />
            <asp:Label ID="lblCompany" runat="server" Text="Company:" />
            <asp:DropDownList ID="ddlCompany" runat="server" Enabled="false" />

            <br />
            <asp:Button ID="btnSaveDocument" runat="server" Text="Save Document" OnClick="btnSaveDocument_Click" />
            <asp:HiddenField ID="hiddenCategoryID" runat="server" />

        </div>
    </form>
</body>
</html>
