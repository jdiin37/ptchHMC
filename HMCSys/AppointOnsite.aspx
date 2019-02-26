<%@ Page Title="現場掛號" Language="C#" MasterPageFile="~/Details.master" AutoEventWireup="true" CodeBehind="AppointOnsite.aspx.cs" Inherits="HMCSys.AppointOnsite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadNestedContent" runat="server">
    <!-- Multi Level Push Menu -->
    <link href="<%= ResolveUrl("~/css/jquery.multilevelpushmenu.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/css/jquery.multilevelpushmenu-rtl.css") %>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainNestedContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <%= Page.Title %></h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    掛號別</div>
                                <asp:DropDownList ID="ddlReg_Type" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="form-group">
                        <div class="col-sm-6">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    看診日</div>
                                <asp:TextBox ID="txtOpd_Date" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    診別</div>
                                <asp:DropDownList ID="ddlTime_Shift" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    科別</div>
                                <asp:DropDownList ID="ddl_Dep_No" runat="server" CssClass="form-control select2-single" ClientIDMode="Static" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    主治醫師</div>
                                <asp:DropDownList ID="ddlDoc_Code" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    診間</div>
                                <asp:DropDownList ID="ddlRoom_No" runat="server" CssClass="form-control select2-single" ClientIDMode="Static" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    就診號</div>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="form-group">
                        <div class="col-sm-6">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    就醫序號</div>
                                <asp:DropDownList ID="ddlCard_No" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="掛號" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        歷史掛號紀錄</h3>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" Width="100%" GridLines="None">
                        <Columns>
                            <asp:BoundField HeaderText="看診日" DataField="Title" />
                            <%--<asp:BoundField DataField="Sta_Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="開始日期" />--%>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="資料庫目前無資料！"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <!-- Multi Level Push Menu -->
    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery.multilevelpushmenu.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery.multilevelpushmenu-rtl.js") %>" charset="big5"></script>
</asp:Content>
