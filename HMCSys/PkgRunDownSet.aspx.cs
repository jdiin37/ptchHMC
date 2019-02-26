using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Dev.Sql;
using System.Web.Services;
using System.Web.Script.Services;

namespace HMCSys
{
    public partial class PkgRunDownSet : System.Web.UI.Page
    {
        private MyConnection conn;
        private List<SqlCommand> list_sqlcmd = new List<SqlCommand>();
        private string strQuePkgId;

        protected void queryRunDown(object sender) //add by tsunghan 2016/08/23
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                List<System.Web.UI.WebControls.ListItem> listItem = new List<System.Web.UI.WebControls.ListItem>();

                string strSql = "Select * From [HMC_RunDown] Where Rec_Status = 'A' ";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSql);
                Dev.Sql.MyConnection conn = new Dev.Sql.MyConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listItem.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["Name"].ToString().Trim(), dt.Rows[i]["Id"].ToString().Trim()));
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

        protected void getQuery()
        {
            strQuePkgId = Request.QueryString["PkgId"];

            if (strQuePkgId != null && strQuePkgId != "")
            {
                conn = new MyConnection();
                string strcmd = "select * from [HMC_PkgM] Where Id = '" + strQuePkgId + "'";
                SqlCommand cmd = new SqlCommand(strcmd);
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    Label1.Text = dt.Rows[0].Field<string>("Name");
                }
            }
        }

        protected void BindGrid()
        {
            try
            {
                string strSql = "Select RunDown_ID,Seq_No From [HMC_PkgRunDown] Where Pkg_Id = '" + strQuePkgId + "' order by seq_no";
                SqlCommand cmd = new SqlCommand(strSql);
                conn = new MyConnection();
                DataTable dt = conn.GetData(cmd);

                dt.Columns.Add("Name", typeof(string));

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add();
                }
                else
                {
                    foreach (DataRow row in dt.Rows) //細項名稱
                    {
                        string strRunDounID = row["RunDown_ID"].ToString();
                        using (conn = new MyConnection())
                        {
                            string strSql1 = "Select Name From [HMC_RunDown] Where Id = @Id";
                            SqlCommand cmd1 = new SqlCommand(strSql1);
                            cmd1.Parameters.Add("Id", SqlDbType.Int).Value = int.Parse(strRunDounID);
                            DataTable dt1 = conn.GetData(cmd1);

                            if (dt1.Rows.Count > 0)
                            {
                                row["Name"] = dt1.Rows[0].Field<string>("Name");
                            }
                            else
                            {
                                string strmsg = string.Format("代碼{0},資料庫中無此流程", strRunDounID);
                                PublicLib.showAlert(strmsg);
                            }
                            cmd1.Cancel();
                            conn.Dispose();
                        }
                    }
                }

                ViewState["Pkg_Rundown"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            getQuery();
            queryRunDown(ddlRunDown);
            if (!Page.IsPostBack)
            {              
                BindGrid();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {                
                var list_seq = new List<string>();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox txtseq = (TextBox)row.FindControl("txtSeqNo");
                    if (list_seq.Contains(txtseq.Text))
                    {
                        PublicLib.showAlert("歐歐 排序有重複喔");
                        return;
                    }
                    list_seq.Add(txtseq.Text);
                }
                
                conn = new MyConnection();
                if (1 == 1)
                {
                    Delete_PkgRunDown();
                    Insert_PkgRunDown();
                    conn.Muti_Insert(list_sqlcmd);
                }               
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }

            Response.Redirect("~/PkgIndex.aspx");
        }

        protected void Delete_PkgRunDown() //新增
        {
            string strSql = "Delete From [HMC_PkgRunDown] Where Pkg_Id=@Pkg_Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Pkg_Id", SqlDbType.VarChar).Value = strQuePkgId;
            list_sqlcmd.Add(cmd);
        }

        protected void Insert_PkgRunDown() //新增
        {
            string strSql = "Insert Into [HMC_PkgRunDown] (Pkg_Id,RunDown_ID,Cre_Date, Cre_User,Seq_No) Values (@Pkg_Id,@RunDown_ID, @Cre_Date, @Cre_User, @Seq_No)";
            foreach (GridViewRow row in GridView1.Rows)
            {                
                Label lbId = (Label)row.FindControl("LabelId");
                TextBox txtseq = (TextBox)row.FindControl("txtSeqNo");

                using (SqlCommand cmd = new SqlCommand(strSql))
                {
                    cmd.Parameters.Add("@Pkg_Id", SqlDbType.VarChar).Value = strQuePkgId;
                    cmd.Parameters.Add("@RunDown_ID", SqlDbType.VarChar).Value = lbId.Text;
                    cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
                    if (txtseq.Text != "" && txtseq.Text != null)
                    {
                        cmd.Parameters.Add("@Seq_No", SqlDbType.Int).Value = int.Parse(txtseq.Text);
                    }
                    else
                    {
                        cmd.Parameters.Add("@Seq_No", SqlDbType.Int).Value = DBNull.Value;
                    }
                    list_sqlcmd.Add(cmd);
                }                
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PkgIndex.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ViewState["Pkg_Rundown"] != null)
            {
                DataTable dt = (DataTable)ViewState["Pkg_Rundown"];
                DataRow drow = null;

                string strRundown_ID = ddlRunDown.SelectedValue;
                string strRundown_Name = ddlRunDown.SelectedItem.Text;

                if (dt.Rows.Count > 0)
                {
                    var list_RundownID = new List<string>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        list_RundownID.Add(dr["RunDown_ID"].ToString());

                        if (list_RundownID.Contains(strRundown_ID))
                        {
                            PublicLib.showAlert("歐歐 此排程已經存在");
                            return;
                        }
                        list_RundownID.Add(dr["RunDown_ID"].ToString());
                    }

                    drow = dt.NewRow();
                    drow["RunDown_ID"] = int.Parse(strRundown_ID);
                    drow["Name"] = strRundown_Name;
                }

                if (dt.Rows[0][0].ToString() == "")
                {
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();
                }
                //add created Rows into dataTable  
                dt.Rows.Add(drow);
                //Save Data table into view state after creating each row  
                ViewState["Pkg_Rundown"] = dt;
                //Bind Gridview with latest Row  
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void delete_row(string RunDown_ID)
        {
            if (ViewState["Pkg_Rundown"] != null)
            {
                DataTable dt = (DataTable)ViewState["Pkg_Rundown"];
                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["Rundown_ID"].ToString() == RunDown_ID)
                        {
                            dr.Delete();
                            dt.AcceptChanges();
                        }
                    }
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add();
                    }
                    ViewState["Pkg_Rundown"] = dt; 
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }            
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("cmdDel"))
                {
                    delete_row(e.CommandArgument.ToString());
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }
    }
}