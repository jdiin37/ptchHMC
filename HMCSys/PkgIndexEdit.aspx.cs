using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Dev.Sql;
using Dev.ODBCSql;
using System.Data.Odbc;

namespace HMCSys
{
    public partial class PkgIndexEdit : System.Web.UI.Page
    {
        private Label lbMsg;
        private MyConnection conn;
        private MyODBCConnection ODBCconn;
        private List<SqlCommand> list_sqlcmd = new List<SqlCommand>();
        protected string strQueID;
        protected string strQuePID;
        protected string strQueCmd;
        protected string strQuetype;
        protected string strPkgCode;

        protected void getQuery()
        {
            strQueCmd = Request.QueryString["cmd"];
            strQueID = Request.QueryString["ID"];
            strQuePID = Request.QueryString["pid"];
            strQuetype = Request.QueryString["type"];

            if (strQuetype == "0")
            {
                type_list.SelectedIndex = 0;
                pkg_panel.Attributes.Add("style", "visibility:hidden");
            }
            else
            {
                type_list.SelectedIndex = 1;
            }

            if (strQueID != null && strQueID != "")
            {
                edit_mode();
                Label1.Text = "編輯";
            }
            else
            {
                Label1.Text = "新增";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            lbMsg = (Label)Master.FindControl("myForm").FindControl("lbMsg");
            getQuery();

            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        protected void edit_mode()
        {

            conn = new MyConnection();
            string strcmd = "select * from [HMC_PkgRelative] Where ID = " + strQueID;
            SqlCommand cmd = new SqlCommand(strcmd);
            DataTable dt = conn.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                strPkgCode = dt.Rows[0].Field<string>("PkgCode");
                if (dt.Rows[0].Field<Decimal>("Type") == 1)
                {
                    type_list.SelectedValue = "1";
                    pkg_panel.Attributes.Add("style", "visibility:visible");
                    txtPkgCode.Text = strPkgCode;
                }
                else
                {
                    type_list.SelectedValue = "0";
                    pkg_panel.Attributes.Add("style", "visibility:hidden");
                }

                txtName.Text = dt.Rows[0].Field<string>("Name");
            }
            conn.Dispose();
        }

        protected void Insert_Relative() //新增
        {
            string strSql = "Insert Into [HMC_PkgRelative] (Type, Name, ParentId, PkgCode, Cre_Date, Cre_User) Values (@Type, @Name, @ParentId, @PkgCode, @Cre_Date, @Cre_user)";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = int.Parse(type_list.SelectedValue);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;

            if (strQuePID != null && strQuePID != "")
            {
                cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = strQuePID;
            }
            else
            {
                cmd.Parameters.Add("@ParentId", SqlDbType.Int).Value = DBNull.Value;
            }

            if (type_list.SelectedValue == "1")
            {
                cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text.Trim();
            }
            else
            {
                cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = DBNull.Value;
            }
            
            cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";

            list_sqlcmd.Add(cmd);
        }

        private void BindGrid() //新增/編輯 套餐 
        {
            DataTable dt = new DataTable();
            string strcmd = "SELECT ItemCode,Memo FROM HMC_PkgD WHERE 1=0";

            if (strPkgCode != null && strPkgCode != "")
            {
                strcmd = "SELECT ItemCode,Memo FROM HMC_PkgD WHERE PkgCode = @Param";
            }

            SqlCommand cmd = new SqlCommand(strcmd);
            if (strPkgCode != null && strPkgCode != "")
            {
                cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = strPkgCode;
            }

            conn = new MyConnection();
            dt = conn.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add();
            }
            ViewState["Pkg_Detail"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (strQueCmd == "add")
                {
                    conn = new MyConnection();

                    if (1 == 1)
                    {
                        Insert_Relative();
                        if (type_list.SelectedValue == "1")
                        {
                            Insert_PkgD();
                        }
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
                else if (strQueCmd == "edit")
                {
                    conn = new MyConnection();
                    if (1 == 1)
                    {
                        Update_Relative();
                        if (type_list.SelectedValue == "1")
                        {
                            Update_PkgD();
                        }
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }

            string strUrl = ResolveUrl(string.Format("~/PkgIndex.aspx?PID={0}", strQuePID));
            Response.Redirect(strUrl, false);
        }

        protected void Update_Relative() //編輯
        {
            string strSql = "Update [HMC_PkgRelative] Set Name=@Name, PkgCode=@PkgCode, Mod_Date=@Mod_Date, Mod_User=@Mod_USER Where Id=@Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQueID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            if (type_list.SelectedValue == "1")
            {
                cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text.Trim();
            }
            else
            {
                cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = DBNull.Value;
            }

            cmd.Parameters.Add("@Mod_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Mod_User", SqlDbType.VarChar).Value = "05890";
            
            list_sqlcmd.Add(cmd);
        }


        protected void Update_PkgD() //新增 and 修改
        {
            Delete_PkgD();
            Insert_PkgD();
        }

        protected void Delete_PkgD() //編輯
        {
            if (strPkgCode != null && strPkgCode != "")
            {
                string strSql = "DELETE FROM [HMC_PkgD] Where PkgCode=@PkgCode";
                SqlCommand cmd = new SqlCommand(strSql);
                cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = strPkgCode;
                list_sqlcmd.Add(cmd);
            }
        }

        protected void Insert_PkgD()
        {
            string strSql = "Insert Into [HMC_PkgD] (ItemCode,PkgCode,Cre_Date, Cre_User) Values (@ItemCode,@PkgCode, @Cre_Date, @Cre_User)";
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)row.FindControl("CkB_Del");
                if (!ckb.Checked)
                {
                    using (SqlCommand cmd = new SqlCommand(strSql))
                    {
                        cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = row.Cells[0].Text;
                        cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text;
                        cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
                        list_sqlcmd.Add(cmd);
                    }
                }
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            string last_url;
            if (strQuePID != null && strQuePID != "")
            {
                last_url = "~/PkgIndex.aspx?PID=" + strQuePID;
            }
            else
            {
                last_url = "~/PkgIndex.aspx";
            }
            Response.Redirect(last_url);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (check_itemExist())
            {
                add_new_row();
                txtItemCode.Text = "";
                txtItemName.Text = "";
            }
        }

        protected void add_new_row()
        {
            if (ViewState["Pkg_Detail"] != null)
            {
                DataTable dt = (DataTable)ViewState["Pkg_Detail"];
                DataRow drow = null;

                if (dt.Rows.Count > 0)
                {
                    drow = dt.NewRow();
                    drow["ItemCode"] = txtItemCode.Text;
                    drow["Memo"] = txtItemName.Text;
                }

                if (dt.Rows[0][0].ToString() == "")
                {
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();

                }
                //add created Rows into dataTable  
                dt.Rows.Add(drow);
                //Save Data table into view state after creating each row  
                ViewState["Pkg_Detail"] = dt;
                //Bind Gridview with latest Row  
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected Boolean check_itemExist()
        {
            Boolean bl_rtnflag = false;
            using (ODBCconn = new MyODBCConnection())
            {
                string strcmd = "select * from price_code where item_code = ?";
                OdbcCommand cmd = new OdbcCommand(strcmd);
                cmd.Parameters.Add("item_code", OdbcType.VarChar).Value = txtItemCode.Text;
                DataTable dt = ODBCconn.GetData(cmd);

                if (dt.Rows.Count > 0) //--資料庫裡面找不到資料
                {
                    txtItemName.Text = dt.Rows[0].Field<string>("f_desc");
                    bl_rtnflag = true;
                }
                else
                {
                    PublicLib.showAlert("資料庫無此細項代碼喔");
                    txtItemCode.Text = "";
                    txtItemName.Text = "";
                    bl_rtnflag = false;
                }

                cmd.Cancel();

                ODBCconn.Dispose();
            }

            return bl_rtnflag;
        }
    }
}