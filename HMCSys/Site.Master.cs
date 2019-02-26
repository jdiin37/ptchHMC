using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMCSys
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            isLogin();
        }

        protected void isLogin()
        {
            try
            {
                bool flag = Session["UID"] != null ? true : false;

                if (!flag)
                {
                    panelLogin.Controls.Add(new LiteralControl("<a class='dropdown-toggle' data-toggle='dropdown' href='#'>您尚未登入&nbsp;<i class='fa fa-caret-down'></i></a>"));
                    panelLogin.Controls.Add(new LiteralControl("<ul class='dropdown-menu dropdown-user'>"));
                    panelLogin.Controls.Add(new LiteralControl(string.Format("<li><a href='{0}'><i class='fa fa-sign-in fa-fw'></i>&nbsp;登入</a></li>", ResolveUrl("~/Login.aspx"))));
                    panelLogin.Controls.Add(new LiteralControl("</ul>"));
                }
                else
                {
                    panelLogin.Controls.Add(new LiteralControl(string.Format("<a class='dropdown-toggle' data-toggle='dropdown' href='#'>歡迎，{0}&nbsp;<i class='fa fa-caret-down'></i></a>", Session["UID"].ToString())));
                    panelLogin.Controls.Add(new LiteralControl("<ul class='dropdown-menu dropdown-user'>"));
                    panelLogin.Controls.Add(new LiteralControl(string.Format("<li><a href='{0}'><i class='fa fa-sign-out fa-fw'></i>&nbsp;登出</a></li>", ResolveUrl("~/Logoutn.aspx"))));
                    panelLogin.Controls.Add(new LiteralControl("</ul>"));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}