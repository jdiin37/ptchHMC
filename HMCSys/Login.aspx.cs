using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMCSys
{
    public partial class Login : System.Web.UI.Page
    {
        protected System.Data.DataTable dt
        {
            get { return (System.Data.DataTable)ViewState["dt"]; }
            set { ViewState["dt"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUID.Text = "f872";
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = true;
            string strAlert = String.Empty;

            if (flag)
            {
                string strSql = "Select userid From users Where userid=? And userpasswd=?";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                cmd.Parameters.Add("@userid", System.Data.Odbc.OdbcType.Char).Value = txtUID.Text.Trim();
                cmd.Parameters.Add("@userpasswd", System.Data.Odbc.OdbcType.Char).Value = txtPwd.Text.Trim();
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count <= 0)
                {
                    flag = false;
                    strAlert += "此帳號不存在或密碼錯誤";
                }
            }

            if (flag)
            {
                //string strSql = "Select prog_id From ctrl_mpriv Where user_id=? And prog_id=?";
                string strSql = "Select b.* From users b Inner Join ctrl_mpriv a On a.user_id=b.userid Where a.user_id=?";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                cmd.Parameters.Add("@user_id", System.Data.Odbc.OdbcType.Char).Value = txtUID.Text.Trim();
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count <= 0)
                {
                    flag = false;
                    strAlert += "此帳號沒有本系統權限";
                }
            }

            if (flag)
            {
                string strSql = "If NOT EXISTS (Select ID From HMC_Permit Where UID=@UID And ProcID='hmc0000p')" +
                    " Begin Insert Into HMC_Permit (ID, UID, ProcID, Cre_Date, Cre_User) Values (NEWID(), @UID, @ProcID, @Cre_Date, @Cre_User) End";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSql);
                cmd.Parameters.Add("@UID", System.Data.SqlDbType.VarChar).Value = dt.Rows[0]["userid"].ToString();
                cmd.Parameters.Add("@ProcID", System.Data.SqlDbType.VarChar).Value = "hmc0000p";
                cmd.Parameters.Add("@Cre_Date", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Cre_User", System.Data.SqlDbType.VarChar).Value = dt.Rows[0]["userid"].ToString();
                Dev.Sql.MyConnection conn = new Dev.Sql.MyConnection();
                conn.ExecuteQuery(cmd);

                Session["UID"] = dt.Rows[0][0].ToString();
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            }
        }
    }
}