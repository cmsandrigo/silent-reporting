using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilentReporting
{
    public class ConfigFileReader
    {
        private string _absolutePath;

        public ConfigFileReader(string path)
        {
            _absolutePath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path);

            if (!File.Exists(_absolutePath))
                throw new ArgumentException("El archivo específicado no existe");
        }

        public Dictionary<string, string> GetAll()
        {
            var allConfigValues = new Dictionary<string, string>();
            var lines = File.ReadAllLines(_absolutePath);

            foreach (var line in lines)
            {
                int equalIndex = line.IndexOf('=');
                string optName = line.Substring(1, equalIndex);
                string optValue = line.Substring(equalIndex);

                allConfigValues.Add(optName, optValue);
            }

            return allConfigValues;
        }
    }
}
