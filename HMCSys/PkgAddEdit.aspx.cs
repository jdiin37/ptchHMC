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
using System.Web.Services;
using System.Web.Script.Services;

namespace HMCSys
{
    public partial class PkgAddEdit : System.Web.UI.Page
    {
        private MyConnection conn;
        private MyODBCConnection ODBCconn;
        private List<SqlCommand> list_sqlcmd = new List<SqlCommand>();
        protected string strQueID;
        protected string strQueCmd;
        protected string strPkgCode;
        protected string strPkgName;
        protected int intPkgPrice;
        protected string strodbccmd = "select a.f_desc,b.amt_ex from price_code a,price_file b where a.item_code = b.item_code and b.e_date is null and b.insurance = '2' and a.item_code = ?";

        protected void getQuery()
        {
            strQueCmd = Request.QueryString["cmd"];
            strQueID = Request.QueryString["PkgId"];
            //strQuePID = Request.QueryString["pid"];

            if (strQueID != null && strQueID != "")
            {
                Label1.Text = "編輯";
                conn = new MyConnection();
                string strcmd = "select * from [HMC_PkgM] Where ID = " + strQueID;
                SqlCommand cmd = new SqlCommand(strcmd);
                DataTable dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    strPkgCode = dt.Rows[0].Field<string>("PkgCode");
                    strPkgName = dt.Rows[0].Field<string>("Name");
                    intPkgPrice = dt.Rows[0].Field<int>("PkgPrice");
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
                txtPkgCode.Text = strPkgCode;
                txtName.Text = strPkgName;
                txtPkgPrice.Text = intPkgPrice.ToString();
                BindGrid();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (check_itemExist())
            {
                add_new_row();
                txtItemCode.Text = "";
                txtItemName.Text = "";
                txtItemPrice.Text = "";
            }
        }
        #region 更新pkgM
        protected void Insert_PkgM() //新增
        {
            string strSql = "Insert Into [HMC_PkgM] (Name, PkgCode, PkgPrice, Cre_Date, Cre_User) Values (@Name, @PkgCode, @PkgPrice, @Cre_Date, @Cre_user)";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text.ToUpper();
            cmd.Parameters.Add("@PkgPrice", SqlDbType.Int).Value = txtPkgPrice.Text.Trim();
            cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
            
            list_sqlcmd.Add(cmd);
        }

        protected void Update_PkgM() //編輯
        {
            string strSql = "Update [HMC_PkgM] Set Name=@Name, PkgCode=@PkgCode, PkgPrice=@PkgPrice, Mod_Date=@Mod_Date, Mod_User=@Mod_USER Where Id=@Id";
            SqlCommand cmd = new SqlCommand(strSql);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = strQueID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text.ToUpper();
            cmd.Parameters.Add("@PkgPrice", SqlDbType.Int).Value = txtPkgPrice.Text.Trim();
            cmd.Parameters.Add("@Mod_Date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Mod_User", SqlDbType.VarChar).Value = "05890";
            list_sqlcmd.Add(cmd);
        }
        #endregion

        #region 更新pkgD
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
            DataTable dt = (DataTable)ViewState["Pkg_Detail"];
            if (dt.Rows[0][0].ToString() != "")
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
                            cmd.Parameters.Add("@PkgCode", SqlDbType.VarChar).Value = txtPkgCode.Text.ToUpper();
                            cmd.Parameters.Add("@Cre_Date", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@Cre_User", SqlDbType.VarChar).Value = "05890";
                            list_sqlcmd.Add(cmd);
                        }
                    }
                }
            }
        }
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (strQueCmd == "add")
                {
                    conn = new MyConnection();
                    if (1 == 1)
                    {
                        Insert_PkgM();
                        Insert_PkgD();
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
                else if (strQueCmd == "edit")
                {
                    conn = new MyConnection();
                    if (1 == 1)
                    {
                        Update_PkgM();
                        Delete_PkgD();
                        Insert_PkgD();
                        conn.Muti_Insert(list_sqlcmd);
                    }
                }
            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }         
            Response.Redirect("~/PkgIndex.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PkgIndex.aspx");
        }

        private void BindGrid() //新增/編輯 套餐 
        {
            DataTable dt = new DataTable();
            string strcmd = "SELECT ItemCode FROM HMC_PkgD WHERE 1=0";

            if (strPkgCode != null && strPkgCode != "")
            {
                strcmd = "SELECT ItemCode FROM HMC_PkgD WHERE PkgCode = @Param";
            }

            SqlCommand cmd = new SqlCommand(strcmd);
            if (strPkgCode != null && strPkgCode != "")
            {
                cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = strPkgCode;
            }

            conn = new MyConnection();
            dt = conn.GetData(cmd);
            dt.Columns.Add("ItemName",typeof(string));
            dt.Columns.Add("ItemPrice", typeof(string));
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add();
            }
            else
            {
                foreach (DataRow row in dt.Rows) //細項名稱
                {
                    string stritemCode = row["ItemCode"].ToString();
                    using (ODBCconn = new MyODBCConnection())
                    {
                        OdbcCommand odbccmd = new OdbcCommand(strodbccmd);
                        odbccmd.Parameters.Add("item_code", OdbcType.VarChar).Value = stritemCode;
                        DataTable odbcdt = ODBCconn.GetData(odbccmd);

                        if (odbcdt.Rows.Count > 0)
                        {
                            row["ItemName"] = odbcdt.Rows[0].Field<string>("f_desc");;
                            row["ItemPrice"] = odbcdt.Rows[0].Field<decimal>("amt_ex");
                        }
                        else
                        {
                            string strmsg = string.Format("代碼{0},資料庫中無此代碼", stritemCode);
                            PublicLib.showAlert(strmsg);
                        }
                        odbccmd.Cancel();
                        ODBCconn.Dispose();
                    }
                }
            }
            ViewState["Pkg_Detail"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
                    drow["ItemName"] = txtItemName.Text;
                    drow["ItemPrice"] = txtItemPrice.Text;
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
                OdbcCommand cmd = new OdbcCommand(strodbccmd);
                cmd.Parameters.Add("item_code", OdbcType.VarChar).Value = txtItemCode.Text;
                DataTable dt = ODBCconn.GetData(cmd);
                if (dt.Rows.Count > 0) //--資料庫裡面找不到資料
                {
                    txtItemName.Text = dt.Rows[0].Field<string>("f_desc");
                    txtItemPrice.Text = dt.Rows[0].Field<decimal>("amt_ex").ToString();                    
                    bl_rtnflag = true;
                }
                else
                {
                    PublicLib.showAlert("資料庫無此細項代碼喔");
                    txtItemCode.Text = "";
                    txtItemName.Text = "";
                    txtItemPrice.Text = "";
                    bl_rtnflag = false;
                }
                cmd.Cancel();
                ODBCconn.Dispose();
            }
            return bl_rtnflag;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CheckPkgCode(string CODE)
        {
            MyConnection conn;
            DataTable dt;
            string strrtn = "";
            string strSql;
            strSql = "Select * From [HMC_PkgM] Where Rec_Status = 'A' and PkgCode = '" + CODE  + "'" ;

            SqlCommand cmd = new SqlCommand(strSql);
            conn = new MyConnection();
            dt = conn.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                strrtn = "此代碼已經存在";
            }

            return strrtn;
        }
    }
}