using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMCSys
{
    public partial class PatientDetails : System.Web.UI.Page
    {
        private HiddenField hfReg_No;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region 初始化控制項
                //Show
                PublicLib.queryCodeFile(ddlSex_Show, "28", "");
                //Edit
                PublicLib.queryCodeFile(ddlSex_Edit, "28", "");
                PublicLib.queryCodeFile(ddlNative_Edit, "27", "");
                PublicLib.queryCodeFile(ddlMerry_Edit, "29", "");
                PublicLib.queryCodeFile(ddlBelief_Edit, "68", "");
                PublicLib.queryCodeFile(ddlP_Type_Edit, "12", "");
                PublicLib.queryCodeFile(ddlInsurance_Edit, "01", "");
                PublicLib.queryCodeFile(ddlSpecial_Edit, "41", "");
                PublicLib.queryCodeFile(ddlPay_Type_Edit, "03", "");
                PublicLib.queryCodeFile(ddlArea_Edit, "30", "");
                PublicLib.queryCodeFile(ddlArea1_Edit, "30", "");
                PublicLib.queryCodeFile(ddlCont_Area_Edit, "30", "");
                //Add
                PublicLib.queryCodeFile(ddlSex, "28", "");

                hfReg_No = (HiddenField)this.Master.FindControl("hfReg_No");
                #endregion
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "Insert Into reg_file (reg_no, p_name, birth_date, sex, id_no, area, merry, native, belief, insurance, p_type, special, pay_type, first_opd_date, last_opd_date, opd_count, miss_count, rent_man, iccard_id)" +
                    " Values (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                #region 測試資料
                //cmd.Parameters.Add("@reg_no", System.Data.Odbc.OdbcType.Char).Value = "Y99001";
                //cmd.Parameters.Add("@p_name", System.Data.Odbc.OdbcType.Char).Value = "測試號";
                //cmd.Parameters.Add("@birth_date", System.Data.Odbc.OdbcType.Date).Value = "1989/06/20";
                //cmd.Parameters.Add("@sex", System.Data.Odbc.OdbcType.Char).Value = "1";
                //cmd.Parameters.Add("@id_no", System.Data.Odbc.OdbcType.Char).Value = "E123456789";
                //cmd.Parameters.Add("@area", System.Data.Odbc.OdbcType.Char).Value = "4301";
                //cmd.Parameters.Add("@merry", System.Data.Odbc.OdbcType.Char).Value = "0";
                //cmd.Parameters.Add("@native", System.Data.Odbc.OdbcType.Char).Value = "01";
                //cmd.Parameters.Add("@belief", System.Data.Odbc.OdbcType.Char).Value = "0";
                //cmd.Parameters.Add("@insurance", System.Data.Odbc.OdbcType.Char).Value = "01";
                //cmd.Parameters.Add("@p_type", System.Data.Odbc.OdbcType.Char).Value = "00";
                //cmd.Parameters.Add("@special", System.Data.Odbc.OdbcType.Char).Value = "00";
                //cmd.Parameters.Add("@pay_type", System.Data.Odbc.OdbcType.Char).Value = "01";
                //cmd.Parameters.Add("@first_opd_date", System.Data.Odbc.OdbcType.Date).Value = DateTime.Now.Date;
                //cmd.Parameters.Add("@last_opd_date", System.Data.Odbc.OdbcType.Date).Value = DateTime.Now.Date;
                //cmd.Parameters.Add("@opd_count", System.Data.Odbc.OdbcType.SmallInt).Value = "0";
                //cmd.Parameters.Add("@miss_count", System.Data.Odbc.OdbcType.SmallInt).Value = "0";
                //cmd.Parameters.Add("@rent_man", System.Data.Odbc.OdbcType.Char).Value = "f872";
                //cmd.Parameters.Add("@iccard_id", System.Data.Odbc.OdbcType.Char).Value = "";
                #endregion
                cmd.Parameters.Add("@reg_no", System.Data.Odbc.OdbcType.Char).Value = txtReg_No.Text;
                cmd.Parameters.Add("@p_name", System.Data.Odbc.OdbcType.Char).Value = txtName.Text;
                cmd.Parameters.Add("@birth_date", System.Data.Odbc.OdbcType.Date).Value = txtBirth.Text;
                cmd.Parameters.Add("@sex", System.Data.Odbc.OdbcType.Char).Value = ddlSex.SelectedValue;
                cmd.Parameters.Add("@id_no", System.Data.Odbc.OdbcType.Char).Value = txtId_No.Text;
                cmd.Parameters.Add("@area", System.Data.Odbc.OdbcType.Char).Value = "4301";
                cmd.Parameters.Add("@merry", System.Data.Odbc.OdbcType.Char).Value = "0";
                cmd.Parameters.Add("@native", System.Data.Odbc.OdbcType.Char).Value = "01";
                cmd.Parameters.Add("@belief", System.Data.Odbc.OdbcType.Char).Value = "0";
                cmd.Parameters.Add("@insurance", System.Data.Odbc.OdbcType.Char).Value = "01";
                cmd.Parameters.Add("@p_type", System.Data.Odbc.OdbcType.Char).Value = "00";
                cmd.Parameters.Add("@special", System.Data.Odbc.OdbcType.Char).Value = "00";
                cmd.Parameters.Add("@pay_type", System.Data.Odbc.OdbcType.Char).Value = "01";
                cmd.Parameters.Add("@first_opd_date", System.Data.Odbc.OdbcType.Date).Value = DateTime.Now.Date;
                cmd.Parameters.Add("@last_opd_date", System.Data.Odbc.OdbcType.Date).Value = DateTime.Now.Date;
                cmd.Parameters.Add("@opd_count", System.Data.Odbc.OdbcType.SmallInt).Value = "0";
                cmd.Parameters.Add("@miss_count", System.Data.Odbc.OdbcType.SmallInt).Value = "0";
                cmd.Parameters.Add("@rent_man", System.Data.Odbc.OdbcType.Char).Value = "f872";
                cmd.Parameters.Add("@iccard_id", System.Data.Odbc.OdbcType.Char).Value = "";
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
                conn.ExecuteQuery(cmd);

                hfReg_No.Value = txtReg_No.Text;
                Response.Redirect(ResolveUrl(string.Format("~/AppointOnsite.aspx?ID={0}", hfReg_No.Value)));
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void btnOpd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl(string.Format("~/AppointOnsite.aspx?ID={0}", hfReg_No.Value)));
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }
    }
}