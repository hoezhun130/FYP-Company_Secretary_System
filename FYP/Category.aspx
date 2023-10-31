<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="FYP.Category" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<script>
    $(document).ready(function () {
        $(".category-link").contextmenu(function (e) {
            e.preventDefault();

            var categoryId = $(this).data("category-id");

            // Hide any existing context menus
            $(".context-menu").hide();

            // Show the context menu at the mouse position
            var menu = $("#context-menu");
            menu.css({ top: e.pageY, left: e.pageX });
            menu.show();

            // Set the category ID for the Edit and Delete buttons
            menu.find(".edit-category").data("category-id", categoryId);
            menu.find(".delete-category").data("category-id", categoryId);
        });

        // Hide the context menu when clicking anywhere on the page
        $(document).click(function () {
            $(".context-menu").hide();
        });
    });
</script>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:TextBox ID="txtCategoryName" runat="server" placeholder="Category Name"></asp:TextBox>
            <br />
            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" />
            <br />
            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCategory" runat="server" CommandArgument='<%# Eval("CategoryID") %>' Text='<%# Eval("CategoryName") %>' OnCommand="lnkCategory_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <div id="context-menu" class="context-menu" style="display: none; position: absolute; border: 1px solid #ccc; background-color: #fff; padding: 5px;">
        <div>
            <a href="#" class="edit-category">Edit</a>
        </div>
        <div>
            <a href="#" class="delete-category">Delete</a>
        </div>
    </div>

</body>
</html>
