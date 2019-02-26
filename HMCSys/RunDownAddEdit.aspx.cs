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
    public partial class RunDownAddEdit : System.Web.UI.Page
    {
        private MyConnection conn;
        protected string strQueID;
        protected string strName;

        protected void getQuery()
        {
            strQueID = Request.QueryString["ID"];

            if (strQueID != null && strQueID != "")
            {
                Label1.Text = "編輯";
                conn = new MyConnection();
                string strcmd = "select * from [HMC_Rundown] Where ID = " + strQueID;
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
                if (strQueID != null && strQueID != "")
                {
                    if (1 == 1)
                    {
                        Update_Rundown();                         
                    }
                }
                else
                {
                    if (1 == 1)
                    {
                        Insert_Rundown();                         
                    }
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
            Response.Redirect("~/RunDown.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RunDown.aspx");
        }

        protected void Insert_Rundown() //新增
        {
            conn = new MyConnection();
            string strSql = "Insert Into [HMC_Rundown] (Name, Cre_Date, Cre_User) Values (@Name, @Cre_Date, @Cre_user)";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
            conn.ExecuteQuery(cmd);
        }

        protected void Update_Rundown() //編輯
        {
            conn = new MyConnection();
            string strSql = "Update [HMC_Rundown] Set Name=@Name, Mod_Date=@Mod_Date, Mod_User=@Mod_USER Where Id=@Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQueID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            cmd.Parameters.Add("@Mod_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Mod_User", SqlDbType.VarChar).Value = "05890";
            conn.ExecuteQuery(cmd);
        }
    }
}