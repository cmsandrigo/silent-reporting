using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using Autofac.Extras.NLog;

namespace SilentReporting
{
    static class Program
    {
        private const string FILE_PARAMETER_NAME = "-f";
        private static readonly IContainer container;

        static Program()
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
                throw new ArgumentException("Se debe especificar el arhivo temporal de entrada.");

            string fileName = args[1];

            // Create the builder with which components/services are registered.
            var builder = new ContainerBuilder();
            builder.RegisterModule<NLogModule>();

            // Como el archivo de configuración va a ser uno solo, hago que el ConfigFileReader se resuelva como singleton
            builder.RegisterType<ConfigFileReader>().AsSelf().WithParameter("path", fileName).SingleInstance();
            builder.RegisterType<ConfigFileDBConnectionInfo>().AsSelf();

            container = builder.Build();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var cfr = container.Resolve<ConfigFileReader>();
            var configValues = cfr.GetAllConfig();
            var connInfo = container.Resolve<ConfigFileDBConnectionInfo>();

            Application.Run(new Form1());
        }
    }
}
