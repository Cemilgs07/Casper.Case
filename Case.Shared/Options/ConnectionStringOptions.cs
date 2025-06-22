using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Shared.Options
{
    public class ConnectionStringOptions
    {
        public const string CaseAPIDbConnectionStringName = nameof(CaseAPIDb);
        public string CaseAPIDb { get; set; } = string.Empty;

    }
}
