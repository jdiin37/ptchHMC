﻿<%@ Page Title="套餐維護" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PkgIndex.aspx.cs" Inherits="HMCSys.PkgIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row pull-right">
		<div class="col-sm-12">
			<p>
                <asp:Button ID="btnAdd_Pkg" runat="server" Text="新增套餐" CssClass="btn btn-success" 
                    onclick="btnAdd_Pkg_Click"/>
			</p>
		</div>
	</div>
	<div class="row">
		<div class="col-sm-12">
			<div class="panel panel-info">
				<div class="panel-heading">
					<h3 class="panel-title">
						<%= Page.Title %></h3>
				</div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-4">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    套餐名稱/代碼</div>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="form-group">
						<div class="col-sm-6">
							<div class="btn-group">
								<asp:Button ID="btnSearch" runat="server" Text="查詢" 
                                    CssClass="btn btn-default btn btn-primary" onclick="btnSearch_Click"  />
								<asp:Button ID="btnCancel" runat="server" Text="取消" 
                                    CssClass="btn btn-default btn btn-default"  UseSubmitBehavior="False" 
                                    onclick="btnCancel_Click"/>
							</div>
						</div>
					</div>
				</div>
				<div class="table-responsive">
					<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-striped" DataKeyNames="ID" Width="100%" GridLines="None" 
                        AllowPaging="True" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onrowcommand="GridView1_RowCommand">
						<Columns>    
							<asp:BoundField HeaderText="套餐名稱" DataField="Name" />
                            <asp:BoundField HeaderText="套餐代碼" DataField="PkgCode" />       
							<asp:TemplateField HeaderText="設定流程">
								<ItemTemplate>
									<asp:LinkButton ID="btnEditR" runat="server" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("ID") %>' CommandName="cmdEditR"><img src="./Images/ic_create_white_18dp.png" class="img-rounded" /></asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="80px" HorizontalAlign="Center" />
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="編輯">
								<ItemTemplate>
									<asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("ID") %>' CommandName="cmdEdit"><img src="./Images/ic_create_white_18dp.png" class="img-rounded" /></asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="60px" HorizontalAlign="Center" />
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除">
								<ItemTemplate>
									<asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("ID") + "," + Eval("PkgCode") %>' CommandName="cmdDel" OnClientClick="return confirm('確定要刪除嗎?')"><img src="./Images/ic_delete_white_18dp.png" class="img-rounded" /></asp:LinkButton>
								</ItemTemplate>
								<ItemStyle Width="60px" HorizontalAlign="Center" />
							</asp:TemplateField>
						</Columns>
						<EmptyDataTemplate>
							<asp:Label ID="lbEmpty" runat="server" Text="資料庫目前無資料！"></asp:Label>
						</EmptyDataTemplate>
						<PagerTemplate>
							<div class="text-center">
								頁數：<asp:Label ID="lbPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'></asp:Label>&nbsp; ／<asp:Label ID="lbTotalPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>&nbsp;
								<asp:Button ID="btnFirst" runat="server" Text="第一頁" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" CssClass="btn btn-xs btn-primary" CommandArgument="First" CommandName="Page" />&nbsp;
								<asp:Button ID="btnPrev" runat="server" Text="上一頁" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" CssClass="btn btn-xs btn-primary" CommandArgument="Prev" CommandName="Page" />&nbsp;
								<asp:Button ID="btnNext" runat="server" Text="下一頁" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" CssClass="btn btn-xs btn-primary" CommandArgument="Next" CommandName="Page" />&nbsp;
								<asp:Button ID="btnLast" runat="server" Text="最末頁" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" CssClass="btn btn-xs btn-primary" CommandArgument="Last" CommandName="Page" />&nbsp; 資料筆數：<asp:Label ID="lbCount" runat="server" Text="<%# dt.Rows.Count.ToString() %>"></asp:Label>
								筆
							</div>
						</PagerTemplate>
					</asp:GridView>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
