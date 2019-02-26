<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjaxForm.aspx.cs" Inherits="HMCSys.AjaxForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtSearch").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: 'post',
                        url: "./WebService.asmx/GetPackageData",
                        data: "{ strParam: '" + $("#txtSearch").val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function (data) {
                            response($.map($.parseJSON(data.d), function (item) {
                                return {
                                    label: item.pat_code + " " + item.pat_name,
                                    val: item.pat_code
                                }
                            }))
                        },
                        error: function () {
                            alert("Failed");
                        }
                    });
                },
                select: function (event, ui) {
                    alert(ui.item.val);
                },
                minLength: 1
            });
        });
    </script>
</asp:Content>
