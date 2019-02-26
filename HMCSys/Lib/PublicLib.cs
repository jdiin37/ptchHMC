using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class PublicLib
{
    #region 公用字串
    public static string Tag = "<DevLog>";
    public static string Notice = "標註*之紅色文字為必填欄位";
    public static string FileRestrict = "檔案格式限制為doc, docx, xls, xlsx, ppt, pptx, pdf, rar, zip, jpg, gif, png";
    #endregion

    #region AlertBox
    public static void showAlert(string Message)
    {
        try
        {
            System.Web.UI.Page page = HttpContext.Current.CurrentHandler as System.Web.UI.Page;
            string strScript = string.Format("showAlert('{0}');", Message);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alertMsg"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alertMsg", strScript, true /* addScriptTags */);
            }
        }
        catch (Exception ex)
        {
            PublicLib.handleError("", "PublicLib", ex.Message);
        }
    }
    #endregion


    #region 例外處理
    public static void handleError(string ErrorType, string strClass, string strException)
    {
        if (System.Web.Configuration.WebConfigurationManager.AppSettings["SaveError"].Equals("1"))
        {
            string strSql = "Insert Into [ErrorLog] (ID, Class, Exception, Cre_Date) Values (NEWID(), @Class, @Exception, @Cre_Date)";
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSql);
            cmd.Parameters.Add("@Class", System.Data.SqlDbType.VarChar).Value = strClass;
            cmd.Parameters.Add("@Exception", System.Data.SqlDbType.NVarChar).Value = strException;
            cmd.Parameters.Add("@Cre_Date", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
            Dev.Sql.MyConnection conn = new Dev.Sql.MyConnection();
            conn.ExecuteQuery(cmd);
        }

        System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, strClass, strException));
    }
    #endregion

    #region 檔案處理
    public static string getMime(string fileName)
    {
        string strReturn = String.Empty;

        try
        {
            string fileType = System.IO.Path.GetExtension(fileName).ToLower();

            switch (fileType)
            {
                case ".doc":
                    strReturn = "application/vnd.ms-word";
                    break;
                case ".docx":
                    strReturn = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".xls":
                    strReturn = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    strReturn = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".ppt":
                    strReturn = "application/vnd.ms-powerpoint";
                    break;
                case ".pptx":
                    strReturn = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".jpg":
                    strReturn = "image/jpg";
                    break;
                case ".png":
                    strReturn = "image/png";
                    break;
                case ".gif":
                    strReturn = "image/gif";
                    break;
                case ".pdf":
                    strReturn = "application/pdf";
                    break;
                case ".zip":
                    strReturn = "application/zip";
                    break;
                case ".rar":
                    strReturn = "application/x-rar-compressed";
                    break;
            }
        }
        catch (Exception ex)
        {

        }

        return strReturn;
    }

    public static string saveFile(HttpPostedFile file)
    {
        string strReturn = String.Empty;

        try
        {
            Guid guid = Guid.NewGuid();

            string fileName = System.IO.Path.GetFileName(file.FileName);
            string fileType = System.IO.Path.GetExtension(fileName).ToLower();

            file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/UploadFiles/") + guid.ToString("N") + fileType);

            strReturn = guid.ToString("N") + fileType;
        }
        catch (Exception ex)
        {

        }

        return strReturn;
    }


    public static string getFilePath(string fileName)
    {
        string strReturn = String.Empty;

        try
        {
            strReturn = "~/UploadFiles/" + fileName;
        }
        catch (Exception ex)
        {

        }

        return strReturn;
    }

    public static bool deleteFile(string fileName)
    {
        bool strReturn = false;

        try
        {
            string strPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadFiles/") + fileName;

            if (System.IO.File.Exists(strPath))
            {
                System.IO.File.Delete(strPath);

                strReturn = true;
            }
        }
        catch (Exception ex)
        {

        }

        return strReturn;
    }

    public static Byte[] convertBinary(System.IO.Stream fs)
    {
        Byte[] bytes = null;

        try
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            bytes = br.ReadBytes((Int32)fs.Length);
        }
        catch (Exception ex)
        {

        }

        return bytes;
    }

    public static void downloadFile(byte[] bytes, string fileName, string fileMime)
    {
        try
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + fileName));
            System.Web.HttpContext.Current.Response.AddHeader("Content-Type", fileMime);
            ms.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
            //System.Web.HttpContext.Current.Response.OutputStream.Write(bytes, 0, bytes.Length);
            System.Web.HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Control轉HTML
    public static string renderToString(System.Web.UI.Control ctl)
    {
        string strReturn = String.Empty;

        try
        {
            System.IO.TextWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(tw);
            ctl.RenderControl(htw);
            strReturn = tw.ToString();
        }
        catch (Exception ex)
        {

        }

        return strReturn;
    }
    #endregion

    public static void queryCodeFile(object sender, string strType, string strParam)
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            List<System.Web.UI.WebControls.ListItem> listItem = new List<System.Web.UI.WebControls.ListItem>();

            string strSql = "Select item_code, description From code_file Where check_flag='Y' And item_type=?" + strParam;
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@Param", System.Data.Odbc.OdbcType.Char).Value = strType;
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listItem.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["item_code"].ToString().Trim() + " " + dt.Rows[i]["description"].ToString().Trim(), dt.Rows[i]["item_code"].ToString().Trim()));
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

    public static void queryDscweek(object sender)
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            List<System.Web.UI.WebControls.ListItem> listItem = new List<System.Web.UI.WebControls.ListItem>();

            string strSql = "Select b.doc_code, b.emp_name From dscdocm b Inner Join dscweek a On a.emp_id=b.emp_id Where p_yymm=? And shift_no='A13'";
            System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(strSql);
            cmd.Parameters.Add("@Param1", System.Data.Odbc.OdbcType.Char).Value = DateTime.Now.ToString("yyyyMM");
            //cmd.Parameters.Add("@Param2", System.Data.Odbc.OdbcType.Char).Value = DateTime.Now.ToString("yyyyMM");
            Dev.ODBCSql.MyODBCConnection conn = new Dev.ODBCSql.MyODBCConnection();
            dt = conn.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listItem.Add(new System.Web.UI.WebControls.ListItem(dt.Rows[i]["doc_code"].ToString().Trim() + " " + dt.Rows[i]["emp_name"].ToString().Trim(), dt.Rows[i]["doc_code"].ToString().Trim()));
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
}