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
    public partial class PkgRelative : System.Web.UI.Page
    {
        private MyConnection conn;
        protected DataTable dt;
        protected string strPID;

        protected void get_PID()
        {
            strPID = Request.QueryString["PID"];
        }

        protected HyperLink create_hyperlink(string argtext, string argurl)
        {
            HyperLink hl = new HyperLink();
            hl.Text = argtext;
            hl.NavigateUrl = argurl;
            return hl;
        }

        protected Label create_label(string argtext)
        {
            Label lb = new Label();
            lb.Text = argtext;
            return lb;
        }

        protected void set_path() //定位父子關係
        {
            Panel1.Controls.Clear();
            Panel1.Controls.Add(create_hyperlink("總覽", "~/PkgRelative.aspx"));

            if (strPID != null && strPID != "")
            {
                List<string> list_path = new List<string>();
                string strpid = strPID;
                conn = new MyConnection();
                string strcmd = "select * from [HMC_Relative] Where Id = " + strpid;
                SqlCommand cmd = new SqlCommand(strcmd);
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    string strurl = "~/PkgRelative.aspx?PID=";
                    string strname = dt.Rows[0].Field<string>("Name");
                    int? intpid = dt.Rows[0].Field<int?>("Parent_Id");
                    list_path.Insert(0, strname + "," + strurl + strpid);
                    while (intpid != null && intpid > 0)//找爸爸的爸爸 and so on
                    {
                        conn = new MyConnection();
                        strcmd = "select * from [HMC_Relative] Where Id = " + intpid;
                        cmd = new SqlCommand(strcmd);
                        dt = conn.GetData(cmd);
                        if (dt.Rows.Count > 0)
                        {
                            strname = dt.Rows[0].Field<string>("Name");
                            list_path.Insert(0, strname + "," + strurl + intpid);
                            intpid = dt.Rows[0].Field<int?>("Parent_Id");
                        }
                    }
                }

                foreach (string listitem in list_path)
                {
                    Panel1.Controls.Add(create_label(">"));
                    Panel1.Controls.Add(create_hyperlink(listitem.Split(',')[0].Trim(), listitem.Split(',')[1].Trim()));
                }
            }
            else
            {
                btnAdd_Pkg.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            get_PID();
            set_path();
            if (!Page.IsPostBack)
            {
                querySql();
            }
        }

        protected void querySql()
        {
            try
            {
                string strParam = String.Empty;
                if (!txtSearch.Text.Equals(""))
                {
                    strParam += " And NAME Like '%' + @Param + '%'";

                    string strsql_pkgm = "select id from [HMC_PkgM] where Name Like '%' + @Param + '%'";
                    SqlCommand cmd_pkgm = new SqlCommand(strsql_pkgm);
                    cmd_pkgm.Parameters.Add("@Param", SqlDbType.NVarChar).Value = txtSearch.Text;
                    conn = new MyConnection();
                    dt = conn.GetData(cmd_pkgm);

                    if (dt.Rows.Count > 0)
                    {
                        string strid = string.Empty;
                        foreach (DataRow row in dt.Rows)
                        {
                            strid += row.Field<int>("ID") + ",";
                        }
                        strid = strid.Substring(0, strid.Length - 1);
                        strParam = " And (NAME Like '%' + @Param + '%' or Pkg_Id in (" + strid + "))";
                    }
                }

                string strSql;

                if (strPID != null && strPID != "")
                {
                    strSql = "Select * From [HMC_Relative] Where Parent_Id =" + strPID + strParam + " Order By Type,ID";
                }
                else
                {
                    strSql = "Select * From [HMC_Relative] Where Parent_Id is null " + strParam + " Order By Type,ID";
                }
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
                if (e.CommandName.Equals("cmdView"))
                {
                    string strUrl = ResolveUrl(string.Format("~/PkgRelative.aspx?PID={0}", e.CommandArgument.ToString()));
                    Response.Redirect(strUrl, false);
                }

                if (e.CommandName.Equals("cmdEdit"))
                {                   
                    string strUrl = ResolveUrl(string.Format("~/ClassAddEdit.aspx?cmd={0}&pid={1}&ID={2}", "edit", strPID, e.CommandArgument));                    
                    Response.Redirect(strUrl);
                }

                if (e.CommandName.Equals("cmdDel"))
                {                    
                    if (have_child(e.CommandArgument.ToString()))
                    {
                        PublicLib.showAlert("請先刪除子項目內的資料");
                    }
                    else
                    {
                        delete_Relative(e.CommandArgument.ToString());
                        querySql();
                    }                  
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }
        private Boolean have_child(string ID)
        {
            Boolean bl_rtnflag = false;
            conn = new MyConnection();
            string strcmd = "select * from [HMC_Relative] Where Parent_Id = " + ID;
            SqlCommand cmd = new SqlCommand(strcmd);
            DataTable dt = conn.GetData(cmd);          
            if (dt.Rows.Count > 0)
            {
                bl_rtnflag = true;
            }
            return bl_rtnflag;
        }

        private void delete_Relative(string ID)
        {
            try
            {
                conn = new MyConnection();
                string strcmd = "Delete From [HMC_Relative] Where Id=@Id";
                SqlCommand cmd = new SqlCommand(strcmd);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(ID);
                conn.ExecuteQuery(cmd);                
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "1")
                {
                    e.Row.Cells[2].Text = "套餐";

                    LinkButton btnson = (LinkButton)e.Row.FindControl("btnView");
                    LinkButton btnedit = (LinkButton)e.Row.FindControl("btnEdit");

                    btnson.Visible = false;
                    btnedit.Visible = false;

                    //套餐資訊
                    conn = new MyConnection();
                    string strcmd = "select * from [HMC_PkgM] Where Id = " + e.Row.Cells[1].Text;
                    SqlCommand cmd = new SqlCommand(strcmd);
                    DataTable dt = conn.GetData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        e.Row.Cells[0].Text = dt.Rows[0].Field<string>("Name");
                        e.Row.Cells[1].Text = dt.Rows[0].Field<string>("PkgCode");
                    }
                }
                else if (e.Row.Cells[2].Text == "0")
                {
                    e.Row.Cells[2].Text = "類別";
                }
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

        protected void btnAdd_Category_Click(object sender, EventArgs e)
        {
            get_PID();
            string strUrl = ResolveUrl(string.Format("~/ClassAddEdit.aspx?cmd={0}&pid={1}", "add", strPID));
            Response.Redirect(strUrl);
        }

        protected void btnAdd_Pkg_Click(object sender, EventArgs e)
        {
            string strUrl = ResolveUrl(string.Format("~/PkgRelativeSet.aspx?pid={0}", strPID));
            Response.Redirect(strUrl);
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            querySql();
        }

        protected void GridView1_PreRender1(object sender, EventArgs e)
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
    }
}