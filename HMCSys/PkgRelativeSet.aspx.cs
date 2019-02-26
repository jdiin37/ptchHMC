using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using Dev.Sql;

namespace HMCSys
{
    public partial class PkgRelativeSet : System.Web.UI.Page
    {
        protected DataTable dt;
        private MyConnection conn;
        protected string strQuePID;

        protected void get_PID()
        {
            strQuePID = Request.QueryString["pid"];
            if (strQuePID != null && strQuePID != "")
            {
                conn = new MyConnection();
                string strsql = "select * from [HMC_Relative] where Id=@Id";
                SqlCommand cmd = new SqlCommand(strsql);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQuePID;
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    Label1.Text = dt.Rows[0].Field<string>("Name");                    
                }
            }
        }

        protected void queryPkgM(object sender)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                List<System.Web.UI.WebControls.ListItem> listItem = new List<System.Web.UI.WebControls.ListItem>();

                string strSql = "Select * From [HMC_PkgM] Where Rec_Status = 'A' ";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSql);
                Dev.Sql.MyConnection conn = new Dev.Sql.MyConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listItem.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["PkgCode"].ToString().Trim() + " " + dt.Rows[i]["Name"].ToString().Trim(), dt.Rows[i]["Id"].ToString().Trim()));
                    }

                    ((System.Web.UI.WebControls.DropDownList)sender).Items.AddRange(listItem.ToArray());
                    ((System.Web.UI.WebControls.DropDownList)sender).DataBind();
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", "PublicLib", ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            queryPkgM(ddlPkgCode);
            get_PID();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MyConnection();
                string strSql = "Insert Into [HMC_Relative] (Type,Pkg_Id,Parent_Id, Cre_Date, Cre_User) Values (@Type,@Pkg_Id, @Parent_Id, @Cre_Date, @Cre_user)";
                SqlCommand cmd = new SqlCommand(strSql);
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Pkg_Id", SqlDbType.Int).Value = ddlPkgCode.SelectedValue;
                cmd.Parameters.Add("@Parent_Id", SqlDbType.Int).Value = strQuePID;
                cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
                conn.ExecuteQuery(cmd);
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
            string strUrl = ResolveUrl(string.Format("~/PkgRelative.aspx?PID={0}", strQuePID));
            Response.Redirect(strUrl);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string last_url;
            if (strQuePID != null && strQuePID != "")
            {
                last_url = "~/PkgRelative.aspx?PID=" + strQuePID;
            }
            else
            {
                last_url = "~/PkgRelative.aspx";
            }
            Response.Redirect(last_url);
        }
    }
}