using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace HMCSys
{
    public partial class RunDownScan : System.Web.UI.Page
    {
        protected System.Data.DataTable dt;
        protected System.Data.DataTable dtRunDown;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generated"]) == "true")
            {
                querySql();
            }


            if (!Page.IsPostBack)
            {
                try
                {
                    querySql();
                }
                catch (Exception ex)
                {
                    PublicLib.handleError("", this.GetType().Name, ex.Message);
                }
            }
            
        }

        protected void querySql()
        {
            try
            {
                string strParam = " and a.opd_date=?";
                
                if (!txtRegSearch.Text.Equals(""))
                {
                    strParam += " and a.reg_no=?";
                }

                if (!txtIdNo.Text.Equals(""))
                {
                    strParam += " and b.id_no=?";
                }
                // and room_no = 'HE'
                string strSql = "select to_char(a.opd_date) as opd_date,a.reg_no,a.card_no, b.p_name, b.id_no from opd_reg a,reg_file b where a.reg_no = b.reg_no and dep_no = 'I9' "+  strParam + " Order by opd_date desc, a.reg_no";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();

                cmd.Parameters.Add("opd_date", System.Data.Odbc.OdbcType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd");

                if (!txtRegSearch.Text.Equals(""))
                {
                    cmd.Parameters.Add("reg_no", System.Data.Odbc.OdbcType.Char).Value = txtRegSearch.Text;
                }
                if (!txtIdNo.Text.Equals(""))
                {
                    cmd.Parameters.Add("id_no", System.Data.Odbc.OdbcType.Char).Value = txtIdNo.Text;
                }

                dt = conn.GetData(cmd);
                PlaceHolder1.Controls.Clear();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    string strName = dr["p_name"].ToString();
                    string strRegNo = dr["reg_no"].ToString();
                    PlaceHolder1.Controls.Add(creat_div_tophalf(strName, strRegNo));
                    PlaceHolder1.Controls.Add(creat_detail_btn(strName,strRegNo));
                    PlaceHolder1.Controls.Add(creat_div_bottomhalf(strRegNo));   
                }

                ViewState["Generated"] = "true";

            }
            catch (Exception ex)
            {
                PublicLib.handleError("", this.GetType().Name, ex.Message);
            }
        }

        protected LiteralControl creat_div_tophalf(string strName, string strRegNo)
        {
            LiteralControl lc = new LiteralControl();
            lc.Text = "<div class=\"panel panel-info\">" +
                          "<div class=\"panel-heading\">" +
                                "<div class=\"panel-title pull-left\">" + strName + "(" + strRegNo  + ")</div>" +
                                "<div class=\"panel-title pull-right\">";                       
            return lc;
        }

        protected LiteralControl creat_div_bottomhalf(string strRegNo)
        {
            LiteralControl lc = new LiteralControl();
            lc.Text = "</div>" +
                      "<div class=\"clearfix\"></div>" +
                         "</div>" +
                         "<div class=\"panel-body\">" +
                               create_rundown(strRegNo) +
                         "</div>" +
                     "</div>";
            return lc;
        }

        protected void showmodal(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Label1.Text = btn.ID;
            Bind_RunDown();
            string script = "$('#divModal').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showmodal", script, true);
        }

        protected Button creat_detail_btn(string strName,string strRegNo)
        {
            Button btn = new Button();
            btn.ID = strName + "(" + strRegNo + ")";
            btn.Text = "明細";
            btn.Click += new EventHandler(showmodal);
            btn.CssClass = "btn btn-default";
            return btn;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //querySql();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            Bind_RunDown();
        }

        protected void Bind_RunDown()
        {
            dtRunDown = new System.Data.DataTable();
            dtRunDown.Columns.Add("SeqNo", typeof(string));
            dtRunDown.Columns.Add("Name", typeof(string));
            dtRunDown.Columns.Add("CheckDateTime", typeof(string));

            System.Data.DataRow drow = null;
            ///資料
            drow = dtRunDown.NewRow();
            drow["SeqNo"] = "1";
            drow["Name"] = "身高體重";
            dtRunDown.Rows.Add(drow);
            drow = dtRunDown.NewRow();
            drow["SeqNo"] = "2";
            drow["Name"] = "視力";
            dtRunDown.Rows.Add(drow);
            drow = dtRunDown.NewRow();
            drow["SeqNo"] = "3";
            drow["Name"] = "腰臀圍";
            dtRunDown.Rows.Add(drow);
            //
            GridView2.DataSource = dtRunDown;
            GridView2.DataBind();
        }

        protected string create_rundown(string RegNo)
        {
            string strrtn = string.Empty;

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("SeqNo", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("CheckDateTime", typeof(string));

            System.Data.DataRow drow = null;
            ///資料
            drow = dt.NewRow();
            drow["SeqNo"] = "1";
            drow["Name"] = "身高體重";
            drow["CheckDateTime"] = "2016/08/29 08:00";
            dt.Rows.Add(drow);
            drow = dt.NewRow();
            drow["SeqNo"] = "2";
            drow["Name"] = "視力";
            drow["CheckDateTime"] = "2016/08/29 08:10";
            dt.Rows.Add(drow);
            drow = dt.NewRow();
            drow["SeqNo"] = "3";
            drow["Name"] = "腰臀圍";
            drow["CheckDateTime"] = "";
            dt.Rows.Add(drow);
            drow = dt.NewRow();
            drow["SeqNo"] = "4";
            drow["Name"] = "繳費";
            drow["CheckDateTime"] = "";
            dt.Rows.Add(drow);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string strName = dr["Name"].ToString();
                string strCheckDateTime = dr["CheckDateTime"].ToString();

                if (strCheckDateTime != null && strCheckDateTime != "")
                {
                    strrtn += "<span class=\"label label-success\">" + strName + "</span>";
                }
                else
                {
                    strrtn += "<span class=\"label label-warning\">" + strName + "</span>";
                }

                int i = dt.Rows.IndexOf(dr);
                if (dt.Rows.IndexOf(dr) != dt.Rows.Count - 1)
                {
                    strrtn += ">";
                }
            }

            return strrtn;
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)e.Row.FindControl("Label2");
                Literal Lt = (Literal)e.Row.FindControl("Literal1");

                switch (lb.Text)
                {
                    case "身高體重":
                        Lt.Text = get_rundown_detail();
                        break;

                    default:
                        break;
                }

            }

        }

        protected string get_rundown_detail()
        {
            string str_detail_item = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("detail_item_name", typeof(string));
            dt.Columns.Add("detail_item_value", typeof(string));

            System.Data.DataRow drow = null;
            ///資料
            drow = dt.NewRow();
            drow["detail_item_name"] = "身高";
            drow["detail_item_value"] = "180";
            dt.Rows.Add(drow);

            drow = dt.NewRow();
            drow["detail_item_name"] = "體重";
            drow["detail_item_value"] = "60";
            dt.Rows.Add(drow);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                str_detail_item += dr["detail_item_name"].ToString() + ":";
                str_detail_item += dr["detail_item_value"].ToString() ;
                str_detail_item += "</br>";
            }
            
            return str_detail_item;                
        }


    }
}