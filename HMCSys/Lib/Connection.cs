using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Dev.Sql
{
    public class MyConnection : IDisposable
    {
        private static string strConn = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        private static SqlConnection conn;
        private static SqlTransaction tran;

        #region 創建SQL連線
        public MyConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new SqlConnection();
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
        public bool ExecuteQuery(SqlCommand cmd)
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

        #region 接收SQL指令並回傳ID
        public string ExecuteScalar(SqlCommand cmd)
        {
            string strResult = String.Empty;

            try
            {
                tran = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = tran;

                strResult = cmd.ExecuteScalar().ToString();

                tran.Commit();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;

                tran.Rollback();
            }
            finally
            {
                Dispose();
            }

            return strResult;
        }
        #endregion

        #region Select資料
        public DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;

            try
            {
                cmd.Connection = conn;
                SqlDataAdapter adapter = new SqlDataAdapter();
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

        public DataSet GetDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            ds.Locale = System.Globalization.CultureInfo.InvariantCulture;

            try
            {
                cmd.Connection = conn;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, this.GetType().Name, ex.Message));
            }
            finally
            {
                Dispose();
            }

            return ds;
        }
        #endregion

        #region 修改資料庫
        public string ExecuteCmd(SqlCommand cmd)
        {
            string result = String.Empty;
            try
            {
                tran = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = tran;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = "Success";
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                result = ex.Message;

                tran.Rollback();

                System.Diagnostics.Debug.WriteLine(string.Format("{0}[{1}]{2}", PublicLib.Tag, this.GetType().Name, ex.Message));
            }
            finally
            {
                Dispose();
            }

            return result;
        }
        #endregion

        #region 接收多筆SQL指令
        public bool Muti_Insert(List<SqlCommand> insertcmd)
        {
            bool flag = false;
            SqlCommand cmd = null;
            try
            {
                tran = conn.BeginTransaction();

                foreach (SqlCommand command in insertcmd)
                {
                    cmd = command;
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        flag = true;
                    }
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                flag = false;

                tran.Rollback();
            }
            finally
            {
                Dispose();
            }

            return flag;
        }
        #endregion
    }
}