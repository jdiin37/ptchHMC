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
    public partial class PkgRunDown : System.Web.UI.Page
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
                strSql = "Select * From [HMC_PkgRelative] Where Type = 1 " + strParam + " Order By ID";

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
                    string strUrl = ResolveUrl(string.Format("~/PkgRunDownSet.aspx?PkgId={0}", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl);
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb1 = (Label)e.Row.FindControl("Label1");
                Label lbPid = (Label)e.Row.FindControl("LabelPid");
                List<string> list_path = new List<string>();
                string strSql = "Select Name,ParentId From [HMC_PkgRelative] Where Id = " + lbPid.Text;
                SqlCommand cmd = new SqlCommand(strSql);
                conn = new MyConnection();
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    list_path.Insert(0, dt.Rows[0].Field<string>("Name"));
                    int? intpid = dt.Rows[0].Field<int?>("ParentId");
                    while (intpid != null && intpid > 0)
                    {
                        strSql = "Select Name,ParentId From [HMC_PkgRelative] Where Id = " + intpid;
                        cmd = new SqlCommand(strSql);
                        conn = new MyConnection();
                        dt = conn.GetData(cmd);
                        if (dt.Rows.Count > 0)
                        {
                            list_path.Insert(0, dt.Rows[0].Field<string>("Name"));
                            intpid = dt.Rows[0].Field<int?>("ParentId");
                        }
                    }
                }

                foreach (string listitem in list_path)
                {
                    lb1.Text += listitem + ">";
                }
            }           
        }
    }
}