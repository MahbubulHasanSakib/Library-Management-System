using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lms
{
    static class Program
    {
        public static string NAME = "";
        public static int TOTALfine;
        public static string SID= "";
        public static DateTime renew_today;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new intro());
        }
    }
}
