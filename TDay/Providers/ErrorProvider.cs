using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TDay
{
    public class ErrorProvider
    {
        public static void CheckLogDir()
        {
            if (TDay.Properties.Settings.Default.DebugMode)
            {
                DirectoryInfo LogDirectory = new DirectoryInfo(Application.CommonAppDataPath+@"\Logs");
                if (!LogDirectory.Exists)
                {
                    LogDirectory.Create();
                    FileStream FS = new FileStream(Application.CommonAppDataPath + "\\Logs\\" + "TDay_" + DateTime.Now.Date.ToShortDateString() + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter SW = new StreamWriter(FS);
                    SW.WriteLine("DebugMode ON ***************************************************************");
                    SW.WriteLine("Date:" + DateTime.Now);
                    SW.WriteLine("****************************************************************************");
                    SW.Close();
                    FS.Close();
                }
                else
                {
                    FileStream FS = new FileStream(Application.CommonAppDataPath + "\\Logs\\" + "TDay_" + DateTime.Now.Date.ToShortDateString() + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter SW = new StreamWriter(FS);
                    SW.WriteLine("DebugMode ON ***************************************************************");
                    SW.WriteLine("Date:"+DateTime.Now);
                    SW.WriteLine("****************************************************************************");
                    SW.Close();
                    FS.Close();
                }
            }
        }

        public static void SetException(Enums.ExceptionType exType, Exception ex)
        {
            if (TDay.Properties.Settings.Default.DebugMode)
            {
                FileStream FS = new FileStream(Application.CommonAppDataPath + "\\Logs\\" + "TDay_" + DateTime.Now.Date.ToShortDateString() + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter SW = new StreamWriter(FS);
                SW.WriteLine("Error:"+exType.ToString());
                SW.WriteLine("Message:" + ex.Message);
                SW.WriteLine("Source:" + ex.Source);
                SW.WriteLine("Data:" + ex.Data);
                SW.Close();
                FS.Close();
            }
        }

    }
}
