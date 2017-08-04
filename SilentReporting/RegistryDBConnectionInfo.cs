using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilentReporting
{
    public class RegistryDBConnectionInfo
    {
        private const string REGISTRY_KEY_PATH = @"Software\Buenos Aires Software\BASCS\Databases\1";
        private const string DATABASE_NAME_KEY = "Database";
        private const string DATABASE_DESCRIPTION_KEY = "Description";
        private const string DATABASE_SERVER_KEY = "Server";
        private const string DATABASE_TYPE_KEY = "Tipo";
        private const string DATABASE_INTEGRATEDSEC_KEY = "SegIntegrada";
        private const string DATABASE_PREDEFINED_KEY = "Predeterminada";
        private const string DATABASE_QUERY_KEY = "Consulta";

        public RegistryDBConnectionInfo()
        {
        }

        public string Server { get; protected set; }
        public string DBName { get; protected set; }
        public string Description { get; protected set; }
        public string DBType { get; protected set; }
        public bool IntegratedSecurity { get; protected set; }

        protected void Load()
        {
            var rk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            if (rk == null)
                throw new Exception("Can't access to HKEY_CURRENT_USER registry key");

            var _parentKey = rk.OpenSubKey(REGISTRY_KEY_PATH);

            DBName = _parentKey.GetValue(DATABASE_NAME_KEY).ToString(); ;
        }
    }
}
