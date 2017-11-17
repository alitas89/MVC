using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLayer
{
    public class LogServices
    {
        protected static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Sample: LogServices.AddLog("Eren Güvercin", InfoFormat, "KullanıcıIslem", "Kullanıcı sisteme giriş yaptı", null, null);
        public static void AddLog(string user, string loggertype, string logprocess, string message, string data, Exception ex)
        {
            LoggerKeep.LoggerAdd(logger, loggertype, logprocess, user, data, message, ex);
        }
    }
}
