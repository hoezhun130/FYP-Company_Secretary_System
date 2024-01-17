<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserSite/Header.Master" CodeBehind="CategorySelection.aspx.cs" Inherits="FYP.UserSite.CategorySelection" %>

<asp:Content runat="server" ContentPlaceHolderID="Content2">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Client Category Selection Page</title>

    <!--Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />


    <!-- jQuery, Popper.js, and Bootstrap JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        .category-item img.category-icon {
            width: 50px; /* Or whatever size you prefer */
            height: auto; /* Maintain aspect ratio */
        }

        .category-item {
            display: inline-block;
            text-align: center;
            margin: 10px;
        }

        .active-page {
            border-bottom: 2px solid white;
        }
    </style>

    <script>
        function showPasswordPopup() {
            $("#passwordModal").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    </script>
</head>

<body>

    <div class="container-fluid text-white" style="height: 350px; background-color: #1d2f43;">
        <div class="row align-items-center justify-content-center" style="height: 100%;">
            <div class="text-center">
                <h1 class="pt-3 display-2 font-weight-normal">Category</h1>
                <hr class="bg-white" style="height: 4px; width: 100px;" />
                <p class="mt-3 font-weight-normal">
                    Please select a category from the options provided and proceed to upload
                <br />
                    your digital documents.
                </p>
            </div>
        </div>
    </div>

    <br />

    <div class="container">

        <div id="form1">
                <div class="modal fade" id="passwordModal" tabindex="-1" role="dialog" aria-labelledby="passwordModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="passwordModalLabel">Register New Password</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="txtNewPassword">New Password:</asp:Label>
                <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" CssClass="form-control"/>

                <br />

                <asp:Label runat="server" ID="lblConfirmPassword" AssociatedControlID="txtConfirmPassword">Confirm Password:</asp:Label>
                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control"/>
                
                <br />

                <asp:Button ID="btnSubmit" runat="server" Text="Update Password" OnClick="btnSubmit_Click1" CssClass="btn btn-primary"/>
            </div>
        </div>
    </div>
</div>

            <div class="modal fade" id="deleteCategoryModal" tabindex="-1" role="dialog" aria-labelledby="deleteCategoryModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-bold" id="deleteCategoryModalLabel">Delete Category</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p class="font-weight-normal">Select a category to delete:</p>
                <asp:DropDownList ID="ddlDeleteCategory" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
            </div>
        </div>
    </div>
</div>

            <div class="form-inline">
                <h5 class="ml-3">List of Categories</h5>
                <asp:Button ID="btnCreateCategoryPage" runat="server" Text="Create New Category" OnClick="btnCreateCategoryPage_Click" CssClass="btn btn-primary ml-auto" />
                <asp:Button ID="btnDeleteCategory" runat="server" CommandName="DeleteCategory" Text="Delete Category" CssClass="btn btn-danger ml-2" OnClientClick="openDeleteCategoryModal(); return false;" />
            </div>
            <hr class="bg-dark" />
            <br />


            <div class="row">
                <asp:Repeater ID="rptCategories" runat="server" OnItemCommand="Category_Command">
                    <ItemTemplate>
                        <div class="col-md-3 mb-4 mt-3">

                            <asp:LinkButton ID="lnkCategory" runat="server" CommandName="SelectCategory" CommandArgument='<%# Eval("CategoryID") %>' CssClass="category-item">
            <img src='<%# ResolveUrl("~/CategoryIcon/" + Eval("IconPath")) %>' alt='<%# Eval("CategoryName") %>' class="category-icon" />
            <span><%# Eval("CategoryName") %></span>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>




        </div>
        <br />
    </div>
    <script>
        function openDeleteCategoryModal() {
            $("#deleteCategoryModal").modal("show");
        }
    </script>
</body>
</html>
</asp:Content>

