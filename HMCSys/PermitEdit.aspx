<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PermitEdit.aspx.cs" Inherits="HMCSys.PermitEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/css/bs-tree-view.css") %>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="well">
                <!-- tree nav starts here -->
                <div class="nav-tree">
                    <label>
                        <span class="fa fa-home"></span>&nbsp;健檢中心資訊平台</label>
                    <ul>
                        <li class="has-child">
                            <p>
                                &nbsp;<asp:CheckBox ID="ckb_hmc0100p" runat="server" ClientIDMode="Static" />&nbsp;客戶資訊</p>
                            <a class="" href="#"><i class="fa fa-minus-square"></i></a>
                            <ul>
                                <li>
                                    <p>
                                        &nbsp;<asp:CheckBox ID="ckb_hmc0101p" runat="server" ClientIDMode="Static" />&nbsp;客戶資訊</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;<asp:CheckBox ID="ckb_hmc0102p" runat="server" ClientIDMode="Static" />&nbsp;現場掛號</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;<asp:CheckBox ID="ckb_hmc0103p" runat="server" ClientIDMode="Static" />&nbsp;客戶歷史健檢資訊</p>
                                </li>
                            </ul>
                        </li>
                        <li class="has-child">
                            <p>
                                &nbsp;掛號</p>
                            <a class="" href="#"><i class="fa fa-minus-square"></i></a>
                            <ul>
                                <li>
                                    <p>
                                        &nbsp;Testing 1</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 2</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 3</p>
                                </li>
                            </ul>
                        </li>
                        <li class="has-child">
                            <p>
                                &nbsp;團檢</p>
                            <a class="" href="#"><i class="fa fa-minus-square"></i></a>
                            <ul>
                                <li>
                                    <p>
                                        &nbsp;Testing 1</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 2</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 3</p>
                                </li>
                            </ul>
                        </li>
                        <li class="has-child">
                            <p>
                                &nbsp;健檢報告</p>
                            <a class="" href="#"><i class="fa fa-minus-square"></i></a>
                            <ul>
                                <li>
                                    <p>
                                        &nbsp;Testing 1</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 2</p>
                                </li>
                                <li>
                                    <p>
                                        &nbsp;Testing 3</p>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!-- end of tree nav -->
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="form-group text-center">
                <asp:Button ID="btnSubmit" runat="server" Text="送出" CssClass="btn btn-success" />
                <asp:Button ID="btnCancel" runat="server" CssClass="cancel btn btn-warning" Text="取消" />
                <asp:Button ID="btnBack" runat="server" CssClass="cancel btn btn-grey" Text="離開" />
            </div>
        </div>
    </div>
</asp:Content>
