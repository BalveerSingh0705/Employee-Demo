
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Exceptions.Manager
{
    public static class NotifyException
    {
        private static string ErrorlineNo, Errormsg, ErrorLocation, extype, EmailHead, EmailSing, StackStress;

        private static string _ExceptionEmailReceivers = ConfigurationManager.AppSettings["ExceptionEmailReceivers"];
        private static string _Environment = ConfigurationManager.AppSettings["Environment"];
        private static string _EmailSender = ConfigurationManager.AppSettings["EmailSender"];
        private static string _EmailSenderPassword = ConfigurationManager.AppSettings["EmailSenderPassword"];

        private static string _SmtpClient = ConfigurationManager.AppSettings["SmtpClient"];
        private static int _SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
        private static bool _IsSslEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSslEnabled"]);


        /// <summary>
        /// This method sends OTP in email to user email for login from mobile side. 
        /// </summary>
        /// <param name="config">Object of Config.</param>
        public static void SendExceptionOccured(string message, Exception exception, string tenantName, string paramData)
        {
            if (String.IsNullOrEmpty(paramData)) paramData = "No Parameters present for this request";
            try
            {
                using (MailMessage msg = new MailMessage())
                {
                    string[] toEmailList = Regex.Split(_ExceptionEmailReceivers, ",");

                    if (toEmailList != null)
                    {
                        foreach (var item in toEmailList)
                        {
                            msg.To.Add(item);
                        }
                    }


                    var newline = "<br/>";
                    StackStress = exception.StackTrace;
                    ErrorlineNo = exception.StackTrace.Split(':').ToList().Last();

                    Errormsg = exception.GetType().Name.ToString();
                    extype = exception.GetType().ToString();
                    ErrorLocation = exception.Message.ToString();
                    EmailHead = "<b>Dear Team,</b>" + "<br/>" + "An exception occurred in a Application " + "With following Details" +
                        "<br/>" + "<br/>";
                    EmailSing = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>Application Admin </b>" + "</br>";
                    string errortomail = EmailHead + "<b>Log Written Date: </b>" + " " + DateTime.Now.ToString() + newline +
                        "<b>Error Line No :</b>" + " " + ErrorlineNo + "\t\n" + " " + newline +
                        "<b>Error Message:</b>" + " " + Errormsg + newline +
                        "<b>Exception Type:</b>" + " " + extype + newline +
                        "<b>Additional Info:</b>" + " " + message + newline +
                        "<b> Error Details :</b>" + " " + ErrorLocation + newline +
                        "<b>SubDomain Name:</b>" + " " + tenantName + newline +
                        "<b> StackTrace Details :</b>" + " " + StackStress + newline +
                        "<b> Parameter Details :</b>" + " " + paramData + newline + EmailSing;


                    msg.From = new MailAddress(_EmailSender, _EmailSender);
                    msg.Subject = string.Format("Exception occurred in {0} Environment", _Environment);
                    msg.Body = errortomail;
                    msg.IsBodyHtml = true;

                    using (SmtpClient client = new SmtpClient())
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(_EmailSender, _EmailSenderPassword);
                        client.Port = _SmtpPort;
                        client.Host = _SmtpClient;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.EnableSsl = true;

                        if (msg.To.Count > 0)
                        {
                            client.Send(msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
