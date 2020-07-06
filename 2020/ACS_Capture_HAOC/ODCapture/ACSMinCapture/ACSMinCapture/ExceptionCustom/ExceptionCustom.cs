using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ACSMinCapture
{
    public partial class ExceptionCustom : Exception
    {

        public ExceptionCustom():
            base()
        {
        }
        public ExceptionCustom(string message):
            base(message)
        {
        }
        public ExceptionCustom(string message, Exception innerException) :
            base(message,innerException)
        {
            PreserveStackTrace(this);
        }

        private static void PreserveStackTrace(Exception exception)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
              BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(exception, null);
        }
    }
}
