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
    public partial class RunDown : System.Web.UI.Page
    {
        protected DataTable dt;
        private MyConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                querySql();
            }
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv != null)
            {
                GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;
                if (pagerRow != null)
                {
                    pagerRow.Visible = true;
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            querySql();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            querySql();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void btnAdd_RunDown_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RunDownAddEdit.aspx");
        }
        protected void querySql()
        {
            try
            {
                string strParam = String.Empty;
                if (!txtSearch.Text.Equals(""))
                {
                    strParam += " and NAME Like '%' + @Param + '%'";
                }

                string strSql;
                strSql = "Select * From [HMC_Rundown] Where Rec_Status = 'A'" + strParam + " Order By ID";

                SqlCommand cmd = new SqlCommand(strSql);
                if (!txtSearch.Text.Equals(""))
                {
                    cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = txtSearch.Text;
                }
                conn = new MyConnection();
                dt = conn.GetData(cmd);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("cmdEdit"))
                {
                    string strUrl = ResolveUrl(string.Format("~/RunDownAddEdit.aspx?id={0}", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl);
                }
                if (e.CommandName.Equals("cmdDel"))
                {
                    conn = new MyConnection();
                    string strsql = "select * from [HMC_PkgRundown] where Rundown_Id=@Rundown_Id ";
                    SqlCommand cmd = new SqlCommand(strsql);
                    cmd.Parameters.Add("@Rundown_Id", SqlDbType.Int).Value = e.CommandArgument.ToString();
                    dt = conn.GetData(cmd);

                    if (dt.Rows.Count > 0)
                    {
                        PublicLib.showAlert("此流程已被設定在套餐內,無法刪除");
                    }
                    else
                    {
                        deleteData(e.CommandArgument.ToString());
                        querySql();
                    }
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }
        protected void deleteData(string argId)
        {
            conn = new MyConnection();
            string strSql = "Update [HMC_Rundown] Set Mod_Date=@Mod_Date, Mod_User=@Mod_USER, Rec_Status=@Rec_Status Where Id=@Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = argId;
            cmd.Parameters.Add("@Mod_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Mod_User", SqlDbType.VarChar).Value = "05890";
            cmd.Parameters.Add("@Rec_Status", SqlDbType.VarChar).Value = "R";
            conn.ExecuteQuery(cmd);
        }
    }
}