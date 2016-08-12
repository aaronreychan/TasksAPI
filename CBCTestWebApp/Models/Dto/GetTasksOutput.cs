using Abp.AutoMapper;
using DataAccess.Models;
using System;

namespace CBCTestWebApp.Models.Dto
{
    [AutoMap(typeof(Task))]
    public class GetTasksOutput
    {
        public int Id { get; set; }
        public int PriorityLookupId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}