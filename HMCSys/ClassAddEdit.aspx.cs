using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Dev.Sql;

namespace HMCSys
{
    public partial class ClassAddEdit : System.Web.UI.Page
    {
        private MyConnection conn;
        private List<SqlCommand> list_sqlcmd = new List<SqlCommand>();
        protected string strQueID;
        protected string strQuePID;
        protected string strQueCmd;
        protected string strName;

        protected void getQuery()
        {
            strQueCmd = Request.QueryString["cmd"];
            strQueID = Request.QueryString["ID"];
            strQuePID = Request.QueryString["pid"];

            if (strQueID != null && strQueID != "")
            {
                Label1.Text = "編輯";
                conn = new MyConnection();
                string strcmd = "select * from [HMC_Relative] Where ID = " + strQueID;
                SqlCommand cmd = new SqlCommand(strcmd);
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    strName = dt.Rows[0].Field<string>("Name");
                }              
            }
            else
            {
                Label1.Text = "新增";
            }

            if (strQuePID != null && strQuePID != "")
            {
                conn = new MyConnection();
                string strsql = "select * from [HMC_Relative] where Id=@Id";
                SqlCommand cmd = new SqlCommand(strsql);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQuePID;
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    Label2.Text = dt.Rows[0].Field<string>("Name");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            getQuery();

            if (!Page.IsPostBack)
            {
                txtName.Text = strName;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {   
                if(strQueCmd == "add")
                {
                    conn = new MyConnection();                    
                    if (1 == 1)
                    {
                        Insert_Relative();                     
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
                else if (strQueCmd == "edit")
                {
                    conn = new MyConnection();
                    if (1 == 1)
                    {
                        Update_Relative();
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
            string strUrl = ResolveUrl(string.Format("~/PkgRelative.aspx?PID={0}", strQuePID));
            Response.Redirect(strUrl, false);
        }

        protected void Insert_Relative() //新增
        {
            string strSql = "Insert Into [HMC_Relative] (Type,Name, Parent_Id, Cre_Date, Cre_User) Values (@Type, @Name, @Parent_Id, @Cre_Date, @Cre_user)";
            SqlCommand cmd = new SqlCommand(strSql);

            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            if (strQuePID != null && strQuePID != "")
            {
                cmd.Parameters.Add("@Parent_Id", SqlDbType.Int).Value = strQuePID;
            }
            else
            {
                cmd.Parameters.Add("@Parent_Id", SqlDbType.Int).Value = DBNull.Value;
            }

            cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";

            list_sqlcmd.Add(cmd);
        }

        protected void Update_Relative() //編輯
        {
            string strSql = "Update [HMC_Relative] Set Name=@Name, Mod_Date=@Mod_Date, Mod_User=@Mod_USER Where Id=@Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQueID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            cmd.Parameters.Add("@Mod_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Mod_User", SqlDbType.VarChar).Value = "05890";

            list_sqlcmd.Add(cmd);
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