using System;
using System.Text;

namespace SilentReporting
{
    public class ConfigFileDBConnectionInfo 
    {
        
        [System.Runtime.InteropServices.DllImport(@"C:\Users\Cesar\Dropbox\Work\BAS\recursos\basext.dll", EntryPoint = "BasUncrpEx")]
        public static extern string BasUncrpEx(System.Text.StringBuilder s);

        private const string DATABASE_ODBC_NAME_KEY = "BASE";
        private const string DATABASE_USERNAME_KEY = "USER";
        private const string DATABASE_ENCRYPTED_PASSWORD_KEY = "CLAVE";

        public ConfigFileDBConnectionInfo(ConfigFileReader cfr)
        {
            if (cfr == null)
                throw new ArgumentNullException("cfr");

            Load(cfr);
        }
        
        public string ODBCName { get; protected set; }
        public string User { get; protected set; }
        public string Password { get; protected set; }
        public string EncryptedPassword { get; protected set; }
        public bool UsingIntegratedSecurity { get; protected set; }

        private void Load(ConfigFileReader cfr)
        {
            ODBCName = cfr.GetConfigValue(DATABASE_ODBC_NAME_KEY);
            User = cfr.GetConfigValue(DATABASE_USERNAME_KEY);

            // If password is not set, so it's using integrated security
            if (cfr.TryGetConfigValue(DATABASE_ENCRYPTED_PASSWORD_KEY, out string encryptedPass) || !string.IsNullOrEmpty(encryptedPass))
            {
                UsingIntegratedSecurity = false;
                EncryptedPassword = encryptedPass;
                var sb = new StringBuilder(256);
                sb.Append(encryptedPass);
                Password = BasUncrpEx(sb);
                // We have to find out how to decrypt password
            }
            else
            {
                UsingIntegratedSecurity = true;
                EncryptedPassword = string.Empty;
                Password = string.Empty;
            }
        }
    }
}
