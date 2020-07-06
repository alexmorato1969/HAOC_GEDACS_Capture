using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture
{
    static class ExceptionExistDetail
    {
        public static IEnumerable<string> GetExceptionDetail(this Exception e)
        {
            var result = (e.InnerException == null) ? string.Empty : e.InnerException.Message;
            yield return result;
        }
    }
}
