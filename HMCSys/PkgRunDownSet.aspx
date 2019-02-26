﻿<%@ Page Title="套餐流程維護" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PkgRunDownSet.aspx.cs" Inherits="HMCSys.PkgRunDownSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
		<div class="col-sm-12">
			<div class="panel panel-info">
				<div class="panel-heading">
					<h3 class="panel-title">
                        流程設定(<asp:Label ID="Label1" runat="server" Text=""></asp:Label>)</h3>
				</div>
				<div class="panel-body">
       				<div class="form-group">     
                        <div class="col-sm-4">
                            <div class="input-group select2-bootstrap-prepend">
                                <div class="input-group-addon">
                                    流程名稱</div>
                                <asp:DropDownList ID="ddlRunDown" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                </asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnAdd" runat="server" Text="新增流程" 
                                    CssClass="btn btn-success" onclick="btnAdd_Click" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" 
                                DataKeyNames="RunDown_ID" AutoGenerateColumns="false" 
                                AutoGenerateDeleteButton="False" onrowcommand="GridView1_RowCommand">
                                <Columns>                                                                     
                                    <asp:BoundField HeaderText="流程名稱" DataField="Name" />
                                    <asp:TemplateField HeaderText="排序">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSeqNo" runat="server" text='<%# Eval("Seq_No")%>' CssClass="form-control" onkeypress="return number_only(event)" MaxLength="2"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="刪除">
							        	<ItemTemplate>
							        		<asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("RunDown_ID") %>' CommandName="cmdDel"><img src="./Images/ic_delete_white_18dp.png" class="img-rounded" /></asp:LinkButton>
							        	</ItemTemplate>
							        	<ItemStyle Width="60px" HorizontalAlign="Center" />
							        </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label id="LabelId" runat ="server" text='<%# Eval("RunDown_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="資料庫目前無資料！"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
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
    <script type = "text/javascript">
        function number_only(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
//                alert("Allow Only Numbers");
                return false;
            }
        }
    </script>
<%--    <script language ="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtRunDown.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "PkgRunDownSet.aspx/GetRunDown",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return { value: item }
                            }))
                        },
                        error: function (XMLHttpResquest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                }
            });
        });
    </script>--%>

</asp:Content>
