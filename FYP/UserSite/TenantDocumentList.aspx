<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenantDocumentList.aspx.cs" Inherits="FYP.UserSite.TenantDocumentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css"/>
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <title>Tenant Document List Page</title>

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

</head>

<body>
        <form id="form1" runat="server">

        <%--Headers--%>
        <div class="container-fluid bg-dark text-white">
            <div class="row">
                <div class="col-6">
                    <h3 class="p-3">Record Management System</h3>
                </div>
                <div class="col-6 d-flex justify-content-end align-items-center">
                    <!-- Add Category and Company as clickable selections -->
                    <a href="TenantCategorySelection.aspx" class="nav-link text-white menu-link" data-target="category">
                        <h5 class="p-3 mb-0">Category</h5>
                    </a>
                    <a href="Package.aspx" class="nav-link text-white menu-link" data-target="package">
                        <h5 class="p-3 mb-0">Package</h5>
                    </a>
                    <a href="TenantUserProfile.aspx" class="nav-link text-white menu-link" data-target="userprofile">
                        <h5 class="p-3 mb-0">User Profile</h5>
                    </a>
                    <button runat="server" class="btn btn-light ml-2" onclick="btnLogout_Click">
                        <i class="fas fa-user mr-2"></i>
                        <asp:Button ID="btnLogout" runat="server" Text="Log Out" OnClick="btnLogout_Click" CssClass="btn" />

                    </button>
                </div>
            </div>
        </div>

    <br />


    <div class="col-12" style="position: relative; top: -10px; left: 2px;">
        <asp:Button ID="Button1" runat="server" Text="< Back" OnClick="Button1_Click" CssClass="btn text-dark underline-with-arrow" />
    </div>



            <div class="modal fade" id="deleteConfirmationModal" tabindex="-1" role="dialog" aria-labelledby="deleteConfirmationModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteConfirmationModalLabel">Confirmation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this document?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                   <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm" OnClick="btnConfirmDelete_Click" CssClass="btn btn-danger ml-2"/>
                </div>
            </div>
        </div>
    </div>



        <div class="container-fluid mt-3">
            <div class="form-inline d-flex">
                <h3 class="text-left">
                    <i class="fas fa-file mr-2"></i><b>
                        <asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label></b>
                </h3>
                <asp:Button ID="btnUploadDocument" runat="server" Text="Upload Document" OnClick="btnUploadDocument_Click" CssClass="btn btn-secondary ml-auto" />
            </div>

            <br />

            <div class="form-inline">
                <asp:Label ID="lblFrom" runat="server" Text="From: " CssClass="control-label"></asp:Label>
                <asp:DropDownList ID="ddlFromYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFilter_Click" CssClass="form-control m-2"></asp:DropDownList>
                <asp:Label ID="lblTo" runat="server" Text="To: "></asp:Label>
                <asp:DropDownList ID="ddlToYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnFilter_Click" CssClass="form-control m-2"></asp:DropDownList>
                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" CssClass="btn btn-primary m-2" />


            </div>

            <hr class="mb-4" />

            <div class="table-responsive">
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>Document Name</th>
                            <th>View Document</th>
                            <th>Uploaded By</th>
                            <th>Date Created</th>
                            <th>Last Updated</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="RepeaterDocuments" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><a href='<%# "TenantVersionControl.aspx?DocumentID=" + Eval("DocumentID") + "&CategoryID=" + Request.QueryString["CategoryID"] + "&CompanyName=" + Request.QueryString["CompanyName"] %>'><%# Eval("DocumentName") %></a></td>
                                    <td><a href='<%# GetDocumentUrl(Eval("DocumentName").ToString()) %>' target="_blank">View Document</a></td>
                                    <td><%# Eval("UploadedByName") %></td>
                                    <td><%# Eval("Date", "{0:dd/MM/yyyy}") %></td>
                                    <td><%# Eval("LastUpdate", string.IsNullOrEmpty(Convert.ToString(Eval("LastUpdate"))) ? "" : Eval("LastUpdate", "{0:dd/MM/yyyy}")) %></td>

                                    <td><asp:Button ID="btnEditDocument" runat="server" CommandName="UpdateDocument" CommandArgument='<%# Eval("DocumentID") %>' Text="Edit" OnCommand="btnEditDocument_Command" CssClass="btn btn-primary" /></td>
                                    <td><asp:Button ID="btnDeleteDocument" runat="server" CommandName="DeleteDocument" CommandArgument='<%# Eval("DocumentID") %>' Text="Delete" CssClass="btn btn-danger ml-2" OnClientClick='<%# "showDeleteConfirmationModal(" + Eval("DocumentID") + "); return false;" %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <asp:Label ID="lblNoDocuments" runat="server" Text="There are no documents." Visible="false"></asp:Label>
            <br />
        </div>
                <asp:HiddenField runat="server" ID="hdnDocumentIDToDelete" />
                <script>
                    function showDeleteConfirmationModal(documentId) {
                        $('#hdnDocumentIDToDelete').val(documentId); // Set the value of hdnDocumentIDToDelete
                        $('#deleteConfirmationModal').modal('show');
                    }
                </script>
    </form>

</body>
</html>
