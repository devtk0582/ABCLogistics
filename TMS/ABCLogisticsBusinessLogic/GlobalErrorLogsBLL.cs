using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABCLogisticsBusinessLogic
{
    public class GlobalErrorLogsBLL : BaseBLL
    {
        public GlobalErrorLogsBLL()
            : base()
        {
        }

        public GlobalErrorLogsBLL(string connectionString)
            : base(connectionString)
        {
        }

        public string LogGlobalError(Exception ex, string methodFrom)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Exception exBase = ex.GetBaseException();
                var newError = new ErrorLog()
                {
                    Message = ((exBase.Message == null) || (exBase.Message == string.Empty)) ? null : exBase.Message,
                    Source = ((exBase.Source == null) || (exBase.Source == string.Empty)) ? null : exBase.Source,
                    StackTrace = ((exBase.StackTrace == null) || (exBase.StackTrace == string.Empty)) ? null : exBase.StackTrace,
                    MemberName = ((exBase.TargetSite.Name == null) || (exBase.TargetSite.Name == string.Empty)) ? null : exBase.TargetSite.Name,
                    MethodFrom = methodFrom,
                    LogDate = DateTime.Now
                };

                entities.ErrorLogs.AddObject(newError);
                entities.SaveChanges();

                return "Please contact administrator about Error ID " + newError.ErrorID;
            }
            else
                return "";
        }
    }
}
