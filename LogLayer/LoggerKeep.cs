using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogLayer
{
    class LoggerKeep
    {
        public static void LoggerAdd(log4net.ILog logger, string LoggerType, string LogProcess, string User, string Data, string LogMessage, System.Exception Ex)
        {
            if (log4net.ThreadContext.Properties["ip"] == null || !string.IsNullOrEmpty(log4net.ThreadContext.Properties["ip"].ToString()))
                log4net.ThreadContext.Properties["ip"] = HttpContext.Current.Request.UserHostAddress.PadLeft(15);

            if (LoggerType == "InfoFormat")
            {
                object[] args = new object[4];

                if (Data != null)
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = Data;
                    args[3] = LogMessage;
                    logger.InfoFormat("[{0}] [{1}] [{2}] [{3}]", args);
                }
                else
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = LogMessage;
                    logger.InfoFormat("[{0}] [{1}] [{2}] ", args);
                }
            }
            else if (LoggerType == "WarnFormat")
            {
                object[] args = new object[4];

                if (Data != null)
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = Data;
                    args[3] = LogMessage;
                    logger.WarnFormat("[{0}] [{1}] [{2}] [{3}]", args);
                }
                else
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = LogMessage;
                    logger.WarnFormat("[{0}] [{1}] [{2}] ", args);
                }
            }
            else if (LoggerType == "ErrorFormat")
            {
                object[] args = new object[4];
                string errorArgs;
                if (Data != null)
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = Data;
                    args[3] = LogMessage;
                    errorArgs = string.Format("[{0}] [{1}] [{2}] [{3}]", args);
                }
                else
                {
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = LogMessage;
                    errorArgs = string.Format("[{0}] [{1}] [{2}]", args);
                }
                logger.Error(errorArgs, Ex);
            }
            else if (LoggerType == "Fatal")
            {
                if (Data != null)
                {
                    object[] args = new object[4];
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = Data;
                    args[3] = LogMessage;
                    string ermes = string.Format("[{0}] [{1}] [{2}] [{3}]", args);
                    logger.Fatal(ermes, Ex);
                }
                else
                {
                    object[] args = new object[4];
                    args[0] = LogProcess;
                    args[1] = User;
                    args[2] = LogMessage;
                    string ermes = string.Format("[{0}] [{1}] [{2}] ", args);
                    logger.Fatal(ermes, Ex);
                }
            }
        }
    }
}
