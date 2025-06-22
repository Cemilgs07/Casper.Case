using Case.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Shared.Options
{
    public class CaseAPIOptions
    {
        public ConnectionStringOptions ConnectionStrings { get; set; } = new();

        // Parameterless constructor gerekli!
        public CaseAPIOptions() { }

        public static CaseAPIOptions GetOptions()
        {
            return ConfigManager.GetOptions<CaseAPIOptions>();
        }
    }
}
