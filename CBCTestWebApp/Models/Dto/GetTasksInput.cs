using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBCTestWebApp.Models.Dto
{
    public class GetTasksInput
    {
        public int? Id { get; set; }
        public int? PriorityLookupId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}