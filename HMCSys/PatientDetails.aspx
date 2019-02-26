<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PatientDetails.aspx.cs" Inherits="HMCSys.PatientDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .panel-tool
        {
           padding-top: 7.5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <p>
                <input type="button" value="新增" class="btn btn-primary" onclick="showModal()" />
                <asp:Button ID="btnOpd" runat="server" Text="掛號" CssClass="btn btn-warning" OnClick="btnOpd_Click" /></p>
        </div>
    </div>
    <!--客戶資料(Show)-->
    <div class="row">
        <div class="col-sm-12">
            <div class="alert alert-info" role="alert">
                <div class="form-group">
                    <div class="col-sm-6">
                        <div class="input-group">
                            <div class="input-group-addon">
                                病歷號</div>
                            <asp:TextBox ID="txtReg_No_Show" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group">
                            <div class="input-group-addon">
                                身分證字號</div>
                            <asp:TextBox ID="txtId_No_Show" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-6">
                        <div class="input-group">
                            <div class="input-group-addon">
                                姓名</div>
                            <asp:TextBox ID="txtName_Show" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="input-group select2-bootstrap-prepend">
                            <div class="input-group-addon">
                                性別</div>
                            <asp:DropDownList ID="ddlSex_Show" runat="server" CssClass="form-control select2-single" ClientIDMode="Static" Enabled="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-6">
                        <div class="input-group">
                            <div class="input-group-addon">
                                出生年月日</div>
                            <asp:TextBox ID="txtBirth_Date_Show" runat="server" CssClass="form-control" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--//客戶資料(Show)-->
    <!--客戶資料(Edit)-->
    <div class="row">
        <div class="col-sm-12">
            <!--基本資料-->
            <div class="panel panel-success">
                <div class="panel-heading clearfix">
                    <h3 class="panel-title pull-left panel-tool">
                        <a data-toggle="collapse" href="#div01_Edit" aria-expanded="true">基本資料</a>
                    </h3>
                    <div class="btn-group pull-right">
                        <button type="button" class="btn btn-default" id="btnEdit01">
                            <li class="fa fa-pencil-square-o"></li>
                            &nbsp;編輯
                        </button>
                        <button type="button" class="btn btn-default" id="btnSave01">
                            <li class="fa fa-floppy-o"></li>
                            &nbsp;儲存
                        </button>
                    </div>
                </div>
                <div id="div01_Edit" class="panel-collapse collapse in" aria-expanded="true">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        姓名</div>
                                    <asp:TextBox ID="txtName_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        性別</div>
                                    <asp:DropDownList ID="ddlSex_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        出生年月日</div>
                                    <asp:TextBox ID="txtBirth_Date_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        國籍</div>
                                    <asp:DropDownList ID="ddlNative_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        婚姻</div>
                                    <asp:DropDownList ID="ddlMerry_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        信仰</div>
                                    <asp:DropDownList ID="ddlBelief_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        居住電話</div>
                                    <asp:TextBox ID="txtTel_No_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        公司電話</div>
                                    <asp:TextBox ID="txtCon_Name_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        手機</div>
                                    <asp:TextBox ID="txtCon_Tel_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        居住區域</div>
                                    <asp:DropDownList ID="ddlArea_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        居住地址</div>
                                    <asp:TextBox ID="txtAddress_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        戶籍區域</div>
                                    <asp:DropDownList ID="ddlArea1_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        戶籍地址</div>
                                    <asp:TextBox ID="txtAddress1_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button" onclick="copyAddr('#txtAddress1_Edit')">
                                            同居住地</button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--//基本資料-->
            <!--聯絡人資料-->
            <div class="panel panel-success">
                <div class="panel-heading clearfix">
                    <h3 class="panel-title pull-left panel-tool">
                        <a data-toggle="collapse" href="#div02_Edit" aria-expanded="true">緊急聯絡人資料</a>
                    </h3>
                    <div class="btn-group pull-right">
                        <button type="button" class="btn btn-default" id="btnEdit02">
                            <li class="fa fa-pencil-square-o"></li>
                            &nbsp;編輯
                        </button>
                        <button type="button" class="btn btn-default" id="btnSave02">
                            <li class="fa fa-floppy-o"></li>
                            &nbsp;儲存
                        </button>
                    </div>
                </div>
                <div id="div02_Edit" class="panel-collapse collapse in" aria-expanded="true">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        聯絡人姓名</div>
                                    <asp:TextBox ID="txtCont_Name_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        關係</div>
                                    <asp:TextBox ID="txtCont_Rel_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        聯絡人電話</div>
                                    <asp:TextBox ID="txtCont_Tel_No_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        聯絡人區域</div>
                                    <asp:DropDownList ID="ddlCont_Area_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        聯絡人地址</div>
                                    <asp:TextBox ID="txtCont_Addr_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button" onclick="copyAddr('#txtCont_Addr_Edit')">
                                            同居住地</button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--//聯絡人資料-->
            <!--其他資料-->
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <a data-toggle="collapse" href="#div03_Edit" aria-expanded="true">其他資料</a>
                    </h3>
                </div>
                <div id="div03_Edit" class="panel-collapse collapse in" aria-expanded="true">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        身分別</div>
                                    <asp:DropDownList ID="ddlP_Type_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        保險別</div>
                                    <asp:DropDownList ID="ddlInsurance_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        輔助身分</div>
                                    <asp:DropDownList ID="ddlSpecial_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="input-group select2-bootstrap-prepend">
                                    <div class="input-group-addon">
                                        付款方式</div>
                                    <asp:DropDownList ID="ddlPay_Type_Edit" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        初診日</div>
                                    <asp:TextBox ID="txtFirst_Opd_Date_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        門診日</div>
                                    <asp:TextBox ID="txtLast_Opd_Date_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        住院日期</div>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        欠款次數</div>
                                    <asp:TextBox ID="txtOpd_Count_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        爽約次數</div>
                                    <asp:TextBox ID="txtMiss_Count_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        IC卡號</div>
                                    <asp:TextBox ID="txtICCard_Id_Edit" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--//其他資料-->
        </div>
    </div>
    <!--//客戶資料(Edit)-->
    <!--資料新增Modal-->
    <div id="divModal" class="modal fade bs-example-modal-lg" aria-hidden="true" tabindex="-1" role="dialog" aria-labelledby="ModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title">
                        病患資料新增</h4>
                </div>
                <div class="modal-body" id="divModalBody">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                <!--基本資料-->
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            基本資料
                                        </h4>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        病歷號</div>
                                                    <asp:TextBox ID="txtReg_No" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        身分證字號</div>
                                                    <asp:TextBox ID="txtId_No" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        姓名</div>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        出生年月日</div>
                                                    <asp:TextBox ID="txtBirth" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <div class="input-group select2-bootstrap-prepend">
                                                    <div class="input-group-addon">
                                                        性別</div>
                                                    <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control select2-single" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        IC卡號</div>
                                                    <asp:TextBox ID="txtCard" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-info" type="button">
                                                            讀卡</button>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--//基本資料-->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="儲存" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
    <!--//資料新增Modal-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#div01_Edit :input').attr("disabled", true);
            $('#div02_Edit :input').attr("disabled", true);
            $('#div03_Edit :input').attr("disabled", true);
            $('#btnSave01').attr("disabled", true);
            $('#btnSave02').attr("disabled", true);

            $(".select2-single").select2({
                theme: "bootstrap",
                width: null,
                containerCssClass: ':all:'
            });

            //Edit
            $('#ddlArea_Edit').select2({ theme: "bootstrap", placeholder: "", allowClear: true });
            $('#ddlArea1_Edit').select2({ theme: "bootstrap", placeholder: "", allowClear: true });
            $('#ddlCont_Area_Edit').select2({ theme: "bootstrap", placeholder: "", allowClear: true });
            $("#ddlArea_Edit").val("").change();
            $("#ddlArea1_Edit").val("").change();
            $("#ddlCont_Area_Edit").val("").change();
        });
    </script>
    <script type="text/javascript">
        function showModal() {
            $('#divModal').modal('show');
        }
        function copyAddr(controlID) {
            $(controlID).val($('#txtAddress_Edit').val());
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtId_No_Show").blur(function () {
                getData();
            });
        });

        $("#btnEdit01").click(function () {
            $('#btnEdit01').attr("disabled", true);
            $('#btnSave01').attr("disabled", false);
            $('#div01_Edit :input').attr("disabled", false);
        });

        $("#btnSave01").click(function () {
            var p_name = $("#txtName_Edit").val();
            var birth_date = $("#txtBirth_Date_Edit").val();
            var sex = $("#ddlSex_Edit").val();
            var merry = $("#ddlMerry_Edit").val();
            var native = $("#ddlNative_Edit").val();
            var belief = $("#ddlBelief_Edit").val();
            var tel_no = $("#txtTel_No_Edit").val();
            var con_name = $("#txtCon_Name_Edit").val();
            var con_tel = $("#txtCon_Tel_Edit").val();
            var area = $("#ddlArea_Edit").val() == null ? "" : $("#ddlArea_Edit").val();
            var address = $("#txtAddress_Edit").val();
            var area1 = $("#ddlArea1_Edit").val() == null ? "" : $("#ddlArea1_Edit").val();
            var address1 = $("#txtAddress1_Edit").val();
            var reg_no = $("#hfReg_No").val();

            $.ajax({
                type: "post",
                url: "./WebService.asmx/SavaPatient01",
                data: "{ p_name: '" + p_name + "', birth_date: '" + birth_date + "', sex: '" + sex + "', merry: '" + merry + "', native: '" + native + "', belief: '" + belief + "', tel_no: '" + tel_no + "', con_name: '" + con_name + "', con_tel: '" + con_tel + "', area: '" + area + "', address: '" + address + "', area1: '" + area1 + "', address1: '" + address1 + "', reg_no: '" + reg_no + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (data) {
                    $('#btnEdit01').attr("disabled", false);
                    $('#btnSave01').attr("disabled", true);
                    $('#div01_Edit :input').attr("disabled", true);
                    $('#ddlSex_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlNative_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlMerry_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlBelief_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlArea_Edit').prop("disabled", true).select2({ theme: "bootstrap", placeholder: "", allowClear: true });
                    $('#ddlArea1_Edit').prop("disabled", true).select2({ theme: "bootstrap", placeholder: "", allowClear: true });
                    getData();
                    showAlert("儲存成功");
                },
                error: function () {
                    alert("Failed");
                }
            });
        });

        $("#btnEdit02").click(function () {
            $('#btnEdit02').attr("disabled", true);
            $('#btnSave02').attr("disabled", false);
            $('#div02_Edit :input').attr("disabled", false);
        });

        $("#btnSave02").click(function () {
            var cont_name = $("#txtCont_Name_Edit").val();
            var cont_tel_no = $("#txtCont_Tel_No_Edit").val();
            var cont_rel = $("#txtCont_Rel_Edit").val();
            var cont_area = $("#ddlCont_Area_Edit").val() == null ? "" : $("#ddlCont_Area_Edit").val();
            var cont_addr = $("#txtCont_Addr_Edit").val();
            var reg_no = $("#hfReg_No").val();

            $.ajax({
                type: "post",
                url: "./WebService.asmx/SavaPatient02",
                data: "{ cont_name: '" + cont_name + "', cont_tel_no: '" + cont_tel_no + "', cont_rel: '" + cont_rel + "', cont_area: '" + cont_area+ "', cont_addr: '" + cont_addr + "', reg_no: '" + reg_no + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (data) {
                    $('#btnEdit02').attr("disabled", false);
                    $('#btnSave02').attr("disabled", true);
                    $('#div02_Edit :input').attr("disabled", true);
                    $('#ddlCont_Area_Edit').prop("disabled", true).select2({ theme: "bootstrap", placeholder: "", allowClear: true });
                    getData();
                    showAlert("儲存成功");

                },
                error: function () {
                    alert("Failed");
                }
            });
        });

        function getData() {
            $.ajax({
                type: 'post',
                url: "./WebService.asmx/GetPatientData",
                data: "{ strParam: '" + $("#txtId_No_Show").val() + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (data) {
                    var items = $.parseJSON(data.d);
                    if (items != null) {
                        $("#hfReg_No").val(items[0].reg_no);
                        //病患基本資料顯示
                        $("#txtReg_No_Show").val(items[0].reg_no);
                        $("#txtName_Show").val(items[0].p_name);
                        $("#txtBirth_Date_Show").val(items[0].birth_date);
                        $("#ddlSex_Show").val(items[0].sex).change();
                        //病患資料編輯
                        $("#txtName_Edit").val(items[0].p_name);
                        $("#txtBirth_Date_Edit").val(items[0].birth_date);
                        $("#ddlSex_Edit").val(items[0].sex).change();
                        $("#ddlNative_Edit").val(items[0].native).change();
                        $("#ddlMerry_Edit").val(items[0].merry).change();
                        $("#ddlBelief_Edit").val(items[0].belief).change();
                        $("#ddlArea_Edit").val(items[0].area).change();
                        $("#txtAddress_Edit").val(items[0].address);
                        $("#ddlArea1_Edit").val(items[0].area1).change();
                        $("#txtAddress1_Edit").val(items[0].address1);
                        $("#txtTel_No_Edit").val(items[0].tel_no);
                        $("#txtCon_Name_Edit").val(items[0].con_name);
                        $("#txtCon_Tel_Edit").val(items[0].con_tel);
                        $("#txtCont_Name_Edit").val(items[0].cont_name);
                        $("#txtCont_Rel_Edit").val(items[0].cont_rel);
                        $("#ddlCont_Area_Edit").val(items[0].cont_area).change();
                        $("#txtCont_Addr_Edit").val(items[0].cont_addr);
                        $("#txtCont_Tel_No_Edit").val(items[0].cont_tel_no);
                        $("#txtFirst_Opd_Date_Edit").val(items[0].first_opd_date);
                        $("#txtLast_Opd_Date_Edit").val(items[0].last_opd_date);
                        $("#ddlP_Type_Edit").val(items[0].p_type).change();
                        $("#ddlInsurance_Edit").val(items[0].insurance).change();
                        $("#ddlSpecial_Edit").val(items[0].special).change();
                        $("#ddlPay_Type_Edit").val(items[0].pay_type).change();
                        $("#txtOpd_Count_Edit").val(items[0].opd_count);
                        $("#txtMiss_Count_Edit").val(items[0].miss_count);
                        $("#txtICCard_Id_Edit").val(items[0].iccard_id);
                    } else {
                        alert("初診客戶");
                        $('#divModal').modal('show');
                    }
                },
                error: function () {
                    alert("Failed");
                }
            });
        }
    </script>
    <%--<script type="text/javascript">
        function switchDiv(div_ID) {
            if ($('#' + div_ID + ' :input').is(':disabled')) {
                $('#' + div_ID + ' :input').attr("disabled", false);
            } else {
                $('#' + div_ID + ' :input').attr("disabled", true);
                if (div_ID == "div01_Edit") {
                    $('#ddlSex_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlNative_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlMerry_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlBelief_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                }
                if (div_ID == "div03_Edit") {
                    $('#ddlP_Type_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlInsurance_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlSpecial_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                    $('#ddlPay_Type_Edit').prop("disabled", true).select2({ theme: "bootstrap" });
                }
            }
        }
    </script>--%>
</asp:Content>
