<%@ Page Title="客戶健檢進度" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RunDownScan.aspx.cs" Inherits="HMCSys.RunDownScan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                    病歷號</div>
                                <asp:TextBox ID="txtRegSearch" runat="server" CssClass="form-control"></asp:TextBox>
							</div>
						</div>
                        <div class="col-sm-4">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    身分證字號</div>
                                <asp:TextBox ID="txtIdNo" runat="server" CssClass="form-control"></asp:TextBox>
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
			</div>
		</div>
	</div>
    <!--進度查詢Modal-->
    <div id="divModal" class="modal fade bs-example-modal-lg" aria-hidden="true" tabindex="-1" role="dialog" aria-labelledby="ModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title">
                        健檢進度明細查詢</h4>
                </div>
                <div class="modal-body" id="divModalBody">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <!--基本資料-->
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        </h4>
                                    </div>
                                    <div class="panel-body">
                                    </div>                                        
                                    <div class="table-responsive">
					                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                            CssClass="table table-striped" DataKeyNames="Name" Width="100%" GridLines="None" 
                                            AllowPaging="True" 
                                            onpageindexchanging="GridView2_PageIndexChanging" 
                                            onrowdatabound="GridView2_RowDataBound">
					                    	<Columns>   
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table style="width: 100%; border: 1px solid #c0c0c0" class="table">
                                                            <tr>
                                                                <td>                                                                                                                   
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>                                                                                                                       
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Literal ID="Literal1" runat="server" Text=""></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
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
					                    			<asp:Button ID="btnLast" runat="server" Text="最末頁" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" CssClass="btn btn-xs btn-primary" CommandArgument="Last" CommandName="Page" />&nbsp; 資料筆數：<asp:Label ID="lbCount1" runat="server" Text="<%# dtRunDown.Rows.Count.ToString() %>"></asp:Label>
					                    			筆
					                    		</div>
					                    	</PagerTemplate>
					                    </asp:GridView>
				                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <!--進度查詢Modal-->
<%--    <script type="text/javascript">
        function showModal() {           
            $('#divModal').modal('show');
        }
    </script>--%>

</asp:Content>
