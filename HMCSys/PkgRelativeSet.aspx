<%@ Page Title="新增套餐至類別" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PkgRelativeSet.aspx.cs" Inherits="HMCSys.PkgRelativeSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
		<div class="col-sm-12">
			<div class="panel panel-info">
				<div class="panel-heading">
					<h3 class="panel-title">
                        新增套餐(<asp:Label ID="Label1" runat="server" Text=""></asp:Label>)</h3>
				</div>
				<div class="panel-body">
                    <div class="form-group">
                        <span class="col-sm-2 control-label" style="color: #FF5050">*套餐代碼</span>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlPkgCode" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
					<div class="form-group text-center">
						<asp:Button ID="btnSubmit" runat="server" Text="送出" CssClass="btn btn-success" 
                            onclick="btnSubmit_Click"  />
						<asp:Button ID="btnBack" runat="server" CssClass="cancel btn btn-grey" 
                            Text="離開" onclick="btnBack_Click"  />
					</div>
				</div>
			</div>
		</div>
	</div>

    <script language ="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".select2-single").select2({
                theme: "bootstrap",
                width: null,
                containerCssClass: ':all:'
            });
        });
    </script>
</asp:Content>
