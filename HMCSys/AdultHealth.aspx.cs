using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMCSys
{
    public partial class AdultHealth : System.Web.UI.Page
    {
        protected System.Data.DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Get_AppointmentList();
                }
                catch (Exception ex)
                {
                    PublicLib.handleError("", this.GetType().Name, ex.Message);
                }
            }
        }

        protected void Get_AppointmentList()
        {            
            string strSql = "select to_char(a.opd_date) as opd_date,a.reg_no,a.card_no, b.p_name from opd_reg a,reg_file b where a.reg_no = b.reg_no and a.card_no in ('21','22','23','24') and a.reg_type = '1' and a.time_shift = '1' order by opd_date,a.reg_no";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            GridView1.DataSource = dt;
            GridView1.DataBind();
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
            Get_AppointmentList();
        }
    }
}