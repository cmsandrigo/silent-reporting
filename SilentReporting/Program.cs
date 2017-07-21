using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SilentReporting
{
    static class Program
    {
        private const string FILE_PARAMETER_NAME = "-f";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var args = Environment.GetCommandLineArgs();

            if (!CommandLineUtil.HasParameter(args, FILE_PARAMETER_NAME, out string fileName))
                throw new ArgumentException("Se debe especificar el parametro -f.");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Se debe especificar el arhivo temporal de entrada.");
            
            Application.Run(new Form1());
        }
    }
}
