using Autofac.Extras.NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace SilentReporting
{
    public class ConfigFileReader
    {
        private readonly string _absolutePath;
        private readonly ILogger _logger;

        private Dictionary<string, string> _allConfigValues;

        private Dictionary<string, string> AllConfigValues
        {
            get
            {
                if (_allConfigValues == null) LoadConfigValues();
                return _allConfigValues;
            }
        }

        public ConfigFileReader(ILogger logger, string path)
        {
            _absolutePath = Path.IsPathRooted(path) ? path : Path.GetFullPath(path);

            if (!File.Exists(_absolutePath))
                throw new ArgumentException("El archivo específicado no existe");

            _logger = logger;
        }

        public Dictionary<string, string> GetAllConfig()
        {
            return AllConfigValues;
        }

        public string GetConfigValue(string key)
        {
            return AllConfigValues[key];
        }

        public bool HasConfigKey(string key)
        {
            return AllConfigValues.ContainsKey(key);
        }

        public bool TryGetConfigValue(string key, out string value)
        {
            if (!AllConfigValues.ContainsKey(key))
            {
                value = string.Empty;
                return false;
            }

            value = AllConfigValues[key];
            return true;
        }

        private void LoadConfigValues()
        {
            _allConfigValues = new Dictionary<string, string>();
            var lines = File.ReadAllLines(_absolutePath);

            foreach (var line in lines)
            {
                if (!line.StartsWith("#")) // linea mal formada loguear, hacer un for para poder indicar el numero de linea
                {
                    _logger.Warn("ConfigFile: malformed line. '#' symbol missing on the beggining of the line. Current content: " + line);
                    continue;
                }

                int equalIndex = line.IndexOf('=');

                if (equalIndex < 0)
                {
                    _logger.Warn("ConfigFile: malformed line. '=' symbol missing. Current content: " + line);
                    continue;
                }

                string optName = line.Substring(1, equalIndex - 1);
                string optValue = line.Substring(equalIndex + 1);

                _allConfigValues.Add(optName, optValue.TrimEnd());
            }
        }
    }
}
