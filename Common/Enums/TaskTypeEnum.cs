using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum TaskPriorityEnum
    {
        All = -1,
        [Description("High")]
        High = 100,
        [Description("Medium")]
        Medium = 101,
        [Description("Low")]
        Low = 102,
    }
}
