<%@ Page Title="成建二階掛號" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="AdultHealth.aspx.cs" Inherits="HMCSys.AdultHealth" EnableEventValidation="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        成健二街預約掛號名單</h3>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="table table-striped" Width="100%" GridLines="None" AllowPaging="True"
                        onpageindexchanging="GridView1_PageIndexChanging" 
                        onprerender="GridView1_PreRender">
                        <Columns>
                            <asp:BoundField HeaderText="預約掛號日" DataField="opd_date" />
                            <asp:BoundField HeaderText="姓名" DataField="p_name" />
                            <asp:BoundField HeaderText="病歷號" DataField="reg_no" />
                            <asp:BoundField HeaderText="就醫序號" DataField="card_no" />
                            <%--<asp:BoundField DataField="Sta_Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="開始日期" />--%>
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
