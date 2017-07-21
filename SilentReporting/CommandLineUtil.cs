using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilentReporting
{
    public static class CommandLineUtil
    {
        public static bool HasParameter(string[] commandLineArguments, string parameterName, out string parameterValue)
        {
            for(int i = 0; i < commandLineArguments.Length; i++)
            {
                if (!commandLineArguments[i].Equals(parameterName, StringComparison.OrdinalIgnoreCase))
                    continue;

                parameterValue = (i + 1 == commandLineArguments.Length) 
                    ? string.Empty : commandLineArguments[i + 1];

                return true;
            }

            parameterValue = string.Empty;
            return false;
        }
    }
}
