using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HMCSys
{
    /// <summary>
    /// WebService 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        public string JsonData(System.Data.DataTable dt)
        {
            System.Text.StringBuilder strReturn = new System.Text.StringBuilder();

            try
            {
                strReturn.Append("[");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strReturn.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        strReturn.Append("\"" + dt.Columns[j].ColumnName + "\"");
                        strReturn.Append(":\"");
                        strReturn.Append(dt.Rows[i][j].ToString().Trim());
                        strReturn.Append("\",");
                    }
                    strReturn.Remove(strReturn.Length - 1, 1);
                    strReturn.Append("},");
                }
                strReturn.Remove(strReturn.Length - 1, 1);
                strReturn.Append("]");
            }
            catch (Exception ex)
            {

            }

            return strReturn.ToString();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetPatientData(string strParam)
        {
            string strReturn = String.Empty;

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                string strSql = "Select to_char(birth_date) As birth_date, lpad(insurance, 2,'0') As insurance, to_char(first_opd_date) As first_opd_date, to_char(last_opd_date) As last_opd_date, * From reg_file Where reg_no=? Or id_no=?";
                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
                cmd.Parameters.Add("@Param1", System.Data.Odbc.OdbcType.Char).Value = strParam;
                cmd.Parameters.Add("@Param2", System.Data.Odbc.OdbcType.Char).Value = strParam;
                Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
                dt = conn.GetData(cmd);

                if (dt.Rows.Count > 0)
                {
                    strReturn = JsonData(dt);
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
            }

            return strReturn;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetPackageData(string strParam)
        {
            string strReturn = String.Empty;

            System.Data.DataTable dt = new System.Data.DataTable();

            string strSql = "Select pat_code, pat_name From ordpatm Where doc_no='0000' And pat_code[1,2]='/h' And (pat_code Like '%" + strParam + "%' Or pat_name Like '%" + strParam + "%')";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                strReturn = JsonData(dt);
            }

            return strReturn;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetCodeData(string strType)
        {
            string strReturn = String.Empty;

            System.Data.DataTable dt = new System.Data.DataTable();

            string strSql = "Select * From code_file Where check_flag='Y' And item_type=?";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@Param", System.Data.Odbc.OdbcType.Char).Value = strType;
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                strReturn = JsonData(dt);
            }

            return strReturn;
        }

        [WebMethod]
        public string SavaPatient01(string p_name, string birth_date, string sex, string merry, string native, string belief, string tel_no, string con_name, string con_tel, string area, string address, string area1, string address1, string reg_no)
        {
            string strSql = "Update reg_file Set p_name=?, birth_date=?, sex=?, merry=?, native=?, belief=?, tel_no=?, con_name=?, con_tel=?, area=?, address=?, area1=?, address1=? Where reg_no=?";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@p_name", System.Data.Odbc.OdbcType.Char).Value = p_name;
            cmd.Parameters.Add("@birth_date", System.Data.Odbc.OdbcType.Date).Value = birth_date;
            cmd.Parameters.Add("@sex", System.Data.Odbc.OdbcType.Char).Value = sex;
            cmd.Parameters.Add("@merry", System.Data.Odbc.OdbcType.Char).Value = merry;
            cmd.Parameters.Add("@native", System.Data.Odbc.OdbcType.Char).Value = native;
            cmd.Parameters.Add("@belief", System.Data.Odbc.OdbcType.Char).Value = belief;
            cmd.Parameters.Add("@tel_no", System.Data.Odbc.OdbcType.VarChar).Value = tel_no;
            cmd.Parameters.Add("@con_name", System.Data.Odbc.OdbcType.VarChar).Value = con_name;
            cmd.Parameters.Add("@con_tel", System.Data.Odbc.OdbcType.VarChar).Value = con_tel;
            cmd.Parameters.Add("@area", System.Data.Odbc.OdbcType.Char).Value = area;
            cmd.Parameters.Add("@address", System.Data.Odbc.OdbcType.Char).Value = address;
            cmd.Parameters.Add("@area1", System.Data.Odbc.OdbcType.Char).Value = area1;
            cmd.Parameters.Add("@address1", System.Data.Odbc.OdbcType.VarChar).Value = address1;
            cmd.Parameters.Add("@reg_no", System.Data.Odbc.OdbcType.Char).Value = reg_no;
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();

            return conn.ExecuteQuery(cmd) ? "1" : "0";
        }

        [WebMethod]
        public string SavaPatient02(string cont_name, string cont_tel_no, string cont_rel, string cont_area, string cont_addr, string reg_no)
        {
            string strSql = "Update reg_file Set cont_name=?, cont_tel_no=?, cont_rel=?,cont_area=?, cont_addr=? Where reg_no=?";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@cont_name", System.Data.Odbc.OdbcType.VarChar).Value = cont_name;
            cmd.Parameters.Add("@cont_tel_no", System.Data.Odbc.OdbcType.VarChar).Value = cont_tel_no;
            cmd.Parameters.Add("@cont_rel", System.Data.Odbc.OdbcType.VarChar).Value = cont_rel;
            cmd.Parameters.Add("@cont_area", System.Data.Odbc.OdbcType.Char).Value = cont_area;
            cmd.Parameters.Add("@cont_addr", System.Data.Odbc.OdbcType.VarChar).Value = cont_addr;
            cmd.Parameters.Add("@reg_no", System.Data.Odbc.OdbcType.Char).Value = reg_no;
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();

            return conn.ExecuteQuery(cmd) ? "1" : "0";
        }
    }
}
