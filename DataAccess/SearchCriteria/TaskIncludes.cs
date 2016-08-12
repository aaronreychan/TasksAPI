using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SearchCriteria
{
    [Flags]
    public enum TaskIncludes
    {
        SelfOnly = 1 << 0,
    }
}
