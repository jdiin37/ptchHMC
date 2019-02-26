<%@ Page Title="階層維護" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassAddEdit.aspx.cs" Inherits="HMCSys.ClassAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
		<div class="col-sm-12">
			<div class="panel panel-info">
				<div class="panel-heading">
					<h3 class="panel-title">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>類別(<asp:Label ID="Label2" runat="server" Text="總覽"></asp:Label>)</h3>
				</div>
				<div class="panel-body">
                    <div class="form-group">
                        <span class="col-sm-2 control-label" style="color: #FF5050">*類別名稱</span>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
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
</asp:Content>
