using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net.Mail;

namespace Dev.Mail
{
    public class MyMail : IDisposable
    {
        private string ServerIP = WebConfigurationManager.AppSettings["SmtpIP"];
        private int ServerPort = Convert.ToInt32(WebConfigurationManager.AppSettings["ServerPort"]);
        private string SmtpUser = WebConfigurationManager.AppSettings["SmtpUser"];
        private string SmtpPwd = WebConfigurationManager.AppSettings["SmtpPwd"];

        private static string Header = "<head><style>body { font-family: 'Microsoft Jhenghei'; } table { border-collapse: collapse; } table, th, td { border: 1px solid black; } th, td { padding: 5px; text-align: left; } </style></head>";

        private SmtpClient client;

        public MyMail()
        {
            try
            {
                if (client == null)
                {
                    client = new SmtpClient(ServerIP, ServerPort);
                    client.UseDefaultCredentials = false;
                    //client.EnableSsl = true;
                    //client.Credentials = new NetworkCredential(SmtpUser, SmtpPwd);
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }

        public void WriteMail(string[] strTo, string[] strCC, string[] strBcc, string strSubject, string strBody)
        {
            try
            {
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(SmtpUser, "系統發送信件");
                mms.Subject = strSubject;
                mms.Body = Header + string.Format("<body>{0}</body>", strBody);
                mms.IsBodyHtml = true;

                if (strTo != null)
                {
                    for (int i = 0; i < strTo.Length; i++)
                    {
                        if (!strTo[i].Equals(""))
                        {
                            mms.To.Add(new MailAddress(strTo[i]));
                        }
                    }
                }
                if (strCC != null)
                {
                    for (int i = 0; i < strCC.Length; i++)
                    {
                        if (!strCC[i].Equals(""))
                        {
                            mms.CC.Add(new MailAddress(strCC[i]));
                        }
                    }
                }
                if (strBcc != null)
                {
                    for (int i = 0; i < strBcc.Length; i++)
                    {
                        if (!strBcc[i].Equals(""))
                        {
                            mms.Bcc.Add(new MailAddress(strBcc[i]));
                        }
                    }
                }

                client.Send(mms);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Dispose();
            }
        }

        public void WriteMail(string strFrom, string strFromName, string[] strTo, string[] strCC, string[] strBcc, string strSubject, string strBody)
        {
            try
            {
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(strFrom, strFromName);
                mms.Subject = strSubject;
                mms.Body = Header + string.Format("<body>{0}</body>", strBody);
                mms.IsBodyHtml = true;

                if (strTo != null)
                {
                    for (int i = 0; i < strTo.Length; i++)
                    {
                        if (!strTo[i].Equals(""))
                        {
                            mms.To.Add(new MailAddress(strTo[i]));
                        }
                    }
                }
                if (strCC != null)
                {
                    for (int i = 0; i < strCC.Length; i++)
                    {
                        if (!strCC[i].Equals(""))
                        {
                            mms.CC.Add(new MailAddress(strCC[i]));
                        }
                    }
                }
                if (strBcc != null)
                {
                    for (int i = 0; i < strBcc.Length; i++)
                    {
                        if (!strBcc[i].Equals(""))
                        {
                            mms.Bcc.Add(new MailAddress(strBcc[i]));
                        }
                    }
                }

                client.Send(mms);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Dispose();
            }
        }

        public void WriteMailAuth(string strUser, string strPwd, string[] strTo, string[] strCC, string[] strBcc, string strSubject, string strBody)
        {
            try
            {
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(SmtpUser, "系統發送信件");
                mms.Subject = strSubject;
                mms.Body = Header + string.Format("<body>{0}</body>", strBody);
                mms.IsBodyHtml = true;

                if (strTo != null)
                {
                    for (int i = 0; i < strTo.Length; i++)
                    {
                        if (!strTo[i].Equals(""))
                        {
                            mms.To.Add(new MailAddress(strTo[i]));
                        }
                    }
                }
                if (strCC != null)
                {
                    for (int i = 0; i < strCC.Length; i++)
                    {
                        if (!strCC[i].Equals(""))
                        {
                            mms.CC.Add(new MailAddress(strCC[i]));
                        }
                    }
                }
                if (strBcc != null)
                {
                    for (int i = 0; i < strBcc.Length; i++)
                    {
                        if (!strBcc[i].Equals(""))
                        {
                            mms.Bcc.Add(new MailAddress(strBcc[i]));
                        }
                    }
                }

                client.Send(mms);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Dispose();
            }
        }

        public void WriteMailAuth(string strUser, string strPwd, string strFrom, string strFromName, string[] strTo, string[] strCC, string[] strBcc, string strSubject, string strBody)
        {
            try
            {
                MailMessage mms = new MailMessage();
                mms.From = new MailAddress(strFrom, strFromName);
                mms.Subject = strSubject;
                mms.Body = Header + string.Format("<body>{0}</body>", strBody);
                mms.IsBodyHtml = true;

                if (strTo != null)
                {
                    for (int i = 0; i < strTo.Length; i++)
                    {
                        if (!strTo[i].Equals(""))
                        {
                            mms.To.Add(new MailAddress(strTo[i]));
                        }
                    }
                }
                if (strCC != null)
                {
                    for (int i = 0; i < strCC.Length; i++)
                    {
                        if (!strCC[i].Equals(""))
                        {
                            mms.CC.Add(new MailAddress(strCC[i]));
                        }
                    }
                }
                if (strBcc != null)
                {
                    for (int i = 0; i < strBcc.Length; i++)
                    {
                        if (!strBcc[i].Equals(""))
                        {
                            mms.Bcc.Add(new MailAddress(strBcc[i]));
                        }
                    }
                }

                client.Send(mms);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Dispose();
            }
        }

        //public void WriteMail(string MailFrom, string MailTo, string Subject, string MailBody, string filePath)
        //{
        //    MailMessage mms = new MailMessage();
        //    mms.From = new MailAddress(MailFrom);
        //    mms.Subject = Subject;
        //    mms.Body = MailBody;
        //    mms.IsBodyHtml = true;
        //    mms.To.Add(new MailAddress(MailTo));

        //    Attachment file = null;
        //    if (filePath != null)
        //    {
        //        file = new Attachment(filePath);
        //        //加入信件的夾帶檔案
        //        mms.Attachments.Add(file);

        //    }

        //    SmtpClient client = new SmtpClient("mail.esst.com.tw");
        //    client.Send(mms);
        //    if (file != null)
        //    {
        //        file.Dispose();
        //        file = null;
        //    }

        //    client.Dispose();
        //    client = null;
        //}
    }
}