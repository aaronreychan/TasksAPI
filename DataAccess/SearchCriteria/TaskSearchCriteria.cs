using Common.Enums;
using DataAccess.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.SearchCriteria
{
    public class TaskSearchCriteria : SearchCriteria<Task>
    {
        public int Id { get; set; }
        public List<TaskPriorityEnum> Priority { get; set; }

        public override IQueryable<Task> BuildQueryOver(IQueryable<Task> query)
        {
            if (Priority == null)
                Priority = new List<TaskPriorityEnum>();

            query = query.Where(e => (Id == 0 || e.Id == Id)
                        && (Priority.Count() == 0  
                            || Priority.Contains(TaskPriorityEnum.All)
                            || Priority.Contains((TaskPriorityEnum)e.PriorityLookupId))
                        );

            return base.BuildQueryOver(query);
        }

    }
}
