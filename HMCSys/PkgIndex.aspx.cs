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
    public partial class PkgIndex : System.Web.UI.Page
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            querySql();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
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

        protected void querySql()
        {
            try
            {
                string strParam = String.Empty;
                if (!txtSearch.Text.Equals(""))
                {
                    strParam += " and (NAME Like '%' + @Param + '%' or PkgCode Like '%' + @Param + '%')";
                }

                string strSql;
                strSql = "Select * From [HMC_PkgM] Where Rec_Status = 'A' " + strParam + " Order By ID";

                SqlCommand cmd = new SqlCommand(strSql);
                if (!txtSearch.Text.Equals(""))
                {
                    cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = txtSearch.Text;
                }
                conn = new MyConnection();
                dt = conn.GetData(cmd);

                ViewState["PkgTotal"] = dt;
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
                    string strUrl = ResolveUrl(string.Format("~/PkgAddEdit.aspx?cmd={0}&PkgId={1}", "edit", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl);
                }
                if (e.CommandName.Equals("cmdEditR"))
                {
                    string strUrl = ResolveUrl(string.Format("~/PkgRunDownSet.aspx?PkgId={0}", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl);
                }
                if (e.CommandName.Equals("cmdDel"))
                {
                    string strPkgid = e.CommandArgument.ToString().Split(',')[0].Trim();
                    string strPkgCode = e.CommandArgument.ToString().Split(',')[1].Trim();
                    
                    //刪除套餐....
                    string strcmd = "DELETE FROM [HMC_PkgM] Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(strcmd);
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(strPkgid);
                    conn = new MyConnection();
                    conn.ExecuteQuery(cmd);

                    //刪除關聯階層...
                    strcmd = "DELETE FROM [HMC_Relative] Where Pkg_Id=@Pkg_Id";
                    cmd = new SqlCommand(strcmd);
                    cmd.Parameters.Add("@Pkg_Id", SqlDbType.Int).Value = int.Parse(strPkgid);
                    conn = new MyConnection();
                    conn.ExecuteQuery(cmd);

                    //刪除細項...                   
                    strcmd = "DELETE FROM [HMC_PkgD] Where PkgCode=@PkgCode";
                    cmd = new SqlCommand(strcmd);
                    cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = strPkgCode;
                    conn = new MyConnection();
                    conn.ExecuteQuery(cmd);

                    //刪除流程...
                    strcmd = "DELETE FROM [HMC_PkgRunDown] Where Pkg_Id=@Pkg_Id";
                    cmd = new SqlCommand(strcmd);
                    cmd.Parameters.Add("@Pkg_Id", SqlDbType.Int).Value = int.Parse(strPkgid);
                    conn = new MyConnection();
                    conn.ExecuteQuery(cmd);

                    Response.Redirect("~/PkgIndex.aspx");
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }

        }

        protected void btnAdd_Pkg_Click(object sender, EventArgs e)
        {
            string strUrl = ResolveUrl(string.Format("~/PkgAddEdit.aspx?cmd={0}", "add"));
            Response.Redirect(strUrl);
        }
    }
}