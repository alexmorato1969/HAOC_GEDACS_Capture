using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ACSMinCapture.Log
{
    
    public static class ACSLog
    {
        static string GetLogPath()
        {
            var logpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Log\\" + 
                "Log" + DateTime.Now.Date.ToString("ddMMyyyy") + ".xml";
            return logpath;
        }

        public static void InsertLog(MessageBoxIcon LogType, Exception e,string StackTrace="")
        {

            XmlDocument XMLLog = new XmlDocument();
            XmlElement DOC;
            if (File.Exists(GetLogPath()))
            {

                using (var old = new StreamReader(GetLogPath()))
                {
                    var s = old.ReadToEnd();
                    old.Close();
                    XMLLog.LoadXml(s);
                }

                DOC = XMLLog.DocumentElement;
            }
            else
            {
                DOC = XMLLog.CreateElement("Log");
                XMLLog.AppendChild(DOC);
            }

            var LogTypeNode = XMLLog.CreateElement(LogType.ToString());

            LogTypeNode.SetAttribute("Message", e.Message);
            LogTypeNode.SetAttribute("Detail", e.GetExceptionDetail().First());
            
            if (StackTrace == "")
                LogTypeNode.SetAttribute("StackTrace", e.StackTrace);
            else
                LogTypeNode.SetAttribute("StackTrace", StackTrace);

            LogTypeNode.SetAttribute("DateTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            DOC.AppendChild(LogTypeNode);

            var dir = Path.GetDirectoryName(GetLogPath());

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            XMLLog.Save(GetLogPath());

        }

        public static void InsertLog(MessageBoxIcon LogType, string Message, string Detail = "")
        {

            XmlDocument XMLLog = new XmlDocument();
            XmlElement DOC;
            if (File.Exists(GetLogPath()))
            {

                using (var old = new StreamReader(GetLogPath()))
                {
                    var s = old.ReadToEnd();
                    old.Close();
                    XMLLog.LoadXml(s);
                }

                DOC = XMLLog.DocumentElement;
            }
            else
            {
                DOC = XMLLog.CreateElement("Log");
                XMLLog.AppendChild(DOC);
            }

            var LogTypeNode = XMLLog.CreateElement(LogType.ToString());
            LogTypeNode.SetAttribute("Message", Message);
            LogTypeNode.SetAttribute("Detail", Detail);
            LogTypeNode.SetAttribute("DateTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            DOC.AppendChild(LogTypeNode);

            var dir = Path.GetDirectoryName(GetLogPath());

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            XMLLog.Save(GetLogPath());


        }

    }
}
