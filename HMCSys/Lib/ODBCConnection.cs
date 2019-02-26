using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.Odbc;
using System.Data;

namespace Dev.ODBCSql
{
    public class MyODBCConnection : IDisposable
    {
        private static string strConn = WebConfigurationManager.ConnectionStrings["InformixConnectionString"].ConnectionString;
        private static OdbcConnection conn;
        private static OdbcTransaction tran;

        #region 創建SQL連線
        public MyODBCConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new OdbcConnection();
                    conn.ConnectionString = strConn;
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, this.GetType().Name, ex.Message));
            }
        }

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn.Close();
                conn = null;
            }
        }
        #endregion

        #region 接收SQL指令
        public bool ExecuteQuery(OdbcCommand cmd)
        {
            bool flag = false;

            try
            {
                tran = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = tran;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    flag = true;
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                flag = false;

                tran.Rollback();

                System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, this.GetType().Name, ex.Message));
            }
            finally
            {
                Dispose();
            }

            return flag;
        }
        #endregion

        #region Select資料
        public DataTable GetData(OdbcCommand cmd)
        {
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

            try
            {
                cmd.Connection = conn;
                OdbcDataAdapter adapter = new OdbcDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, this.GetType().Name, ex.Message));
            }
            finally
            {
                Dispose();
            }

            return dt;
        }
        #endregion
    }
}