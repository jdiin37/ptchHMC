using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMCSys
{
    public partial class AppointOnsite : System.Web.UI.Page
    {
        private TextBox txtReg_No_Show, txtId_No_Show, txtName_Show, txtBirth_Date_Show;
        private DropDownList ddlSex_Show;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region 初始化控制項
                txtReg_No_Show = (TextBox)this.Master.Master.FindControl("myForm").FindControl("MainContent").FindControl("txtReg_No_Show");
                txtId_No_Show = (TextBox)this.Master.Master.FindControl("myForm").FindControl("MainContent").FindControl("txtId_No_Show");
                txtName_Show = (TextBox)this.Master.Master.FindControl("myForm").FindControl("MainContent").FindControl("txtName_Show");
                ddlSex_Show = (DropDownList)this.Master.Master.FindControl("myForm").FindControl("MainContent").FindControl("ddlSex_Show");
                txtBirth_Date_Show = (TextBox)this.Master.Master.FindControl("myForm").FindControl("MainContent").FindControl("txtBirth_Date_Show");

                PublicLib.queryCodeFile(ddlReg_Type, "26", " And (item_code='A' Or item_code='1' Or item_code='9')");
                PublicLib.queryCodeFile(ddlTime_Shift, "25", "");
                PublicLib.queryCodeFile(ddl_Dep_No, "07", " And item_code='I9'");
                PublicLib.queryCodeFile(ddlRoom_No, "36", " And item_code='HE'");
                PublicLib.queryCodeFile(ddlCard_No, "99", "");
                PublicLib.queryDscweek(ddlDoc_Code);

                txtOpd_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
                //txtOpd_Date.Text = DateTime.Now.ToString("yyyy/MM/dd" + DateTime.Now.DayOfWeek.ToString("d"));
                #endregion

                GetPatientData(Request.QueryString["ID"].ToString());
                GetOpd_Reg(Request.QueryString["ID"].ToString());
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void GetPatientData(string strParam)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                string strSql = "Select to_char(birth_date) As birthday, * From reg_file Where reg_no=?";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                cmd.Parameters.Add("@Param", System.Data.Odbc.OdbcType.Char).Value = strParam;
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    txtReg_No_Show.Text = dt.Rows[0]["reg_no"].ToString();
                    txtId_No_Show.Text = dt.Rows[0]["id_no"].ToString();
                    txtName_Show.Text = dt.Rows[0]["p_name"].ToString();
                    ddlSex_Show.SelectedValue = dt.Rows[0]["sex"].ToString();
                    txtBirth_Date_Show.Text = dt.Rows[0]["birthday"].ToString();
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void GetOpd_Reg(string strParam)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            string strSql = "Select First 10 * From opd_reg Where reg_no=? Order By opd_date DESC";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@Param", System.Data.Odbc.OdbcType.Char).Value = strParam;
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}