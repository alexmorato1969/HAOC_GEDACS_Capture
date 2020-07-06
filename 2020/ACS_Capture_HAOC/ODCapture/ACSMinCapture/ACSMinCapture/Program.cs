using ACSMinCapture.Config;
using ACSMinCapture.DataBase;
using ACSMinCapture.DataBase.Model;
using ACSMinCapture.Global;
using ACSMinCapture.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ACSMinCapture
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Program.Form1_UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string Processo = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(Processo).Length > 1)
            {
                WFMessageBox.Show("Programa em execução", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }

            MainForm = new WFMain();
            MainForm.Hide();
            Application.Run(MainForm);
        }

        public static WFMain MainForm { get; set; }


        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        { 
            try
            {
                ACSLog.InsertLog(MessageBoxIcon.Error, t.Exception);
                var fVerifica = t.Exception.StackTrace.ToUpper().Contains("SYSTEM.WINDOWS.FORMS.TREEVIEW.CUSTOMDRAW");
                if (!fVerifica)
                {
                    DialogResult result = DialogResult.Cancel;
               
                        result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
               

                    // Exits the program when the user clicks Abort.
                    if (result == DialogResult.Abort)
                        Application.Exit();
                }
               
            }
            catch
            {
                try
                {
                    MessageBox.Show("Erro", "Ocorreu um erro na aplicação. Por favor entre em contato com o administrador informando a seguinte mensagem: \n\n" +
                    "Erro desconhecido - Form1_UIThreadException",
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "Ocorreu um erro na aplicação. Por favor entre em contato com o administrador com a seguinte informação: \n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Erro",
                        "Erro na aplicação. Razão: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "Ocorreu um erro na aplicação. Por favor entre em contato com o administrador com a seguinte informação: \n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}
