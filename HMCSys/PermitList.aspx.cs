using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dev.Sql;
using System.Data;
using System.Data.SqlClient;

namespace HMCSys
{
    public partial class PermitList : System.Web.UI.Page
    {
        private MyConnection conn;
        protected DataTable dt
        {
            get { return (DataTable)ViewState["dt"]; }
            set { ViewState["dt"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    querySql();
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void querySql()
        {
            try
            {
                string strParam = String.Empty;
                if (!txtSearch.Text.Equals(""))
                {
                    strParam += " And UID Like '%' + @Param + '%'";
                }

                string strSql = "Select DISTINCT(UID) From [HMC_Permit] Where 1=1" + strParam + " Order By UID";
                SqlCommand cmd = new SqlCommand(strSql);
                if (!txtSearch.Text.Equals(""))
                {
                    cmd.Parameters.Add("@Param", SqlDbType.VarChar).Value = txtSearch.Text;
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
                    string strUrl = ResolveUrl(string.Format("~/PermitEdit.aspx?cmd={0}&ID={1}", "edit", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl);
                }

                if (e.CommandName.Equals("cmdDel"))
                {
                    if (1 == 1)
                    {
                        string strSql = "Update [Account] Set Rec_Status='R' Where ID=@ID";
                        SqlCommand cmd = new SqlCommand(strSql);
                        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = new Guid(e.CommandArgument.ToString());
                        conn = new MyConnection();
                        conn.ExecuteQuery(cmd);
                    }

                    querySql();
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                //    btnDelete.OnClientClick = "return confirm('確定要刪除：" + e.Row.Cells[0].Text + "?');";
                //}
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }
    }
}