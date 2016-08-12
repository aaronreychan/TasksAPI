using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RefactorThis.GraphDiff;
using DataAccess.SearchCriteria;

namespace DataAccess.Repositories
{
    public class TaskRepository: Repository<Task>, IRepository<Task>
    {
        private CBCTestEntities _dbContext;

        #region Constructor
        public TaskRepository(CBCTestEntities context)
            : base(context)
        {
            _dbContext = context;
        }

        #endregion


        protected override IQueryable<Task> ApplyIncludes(IQueryable<Task> query, Enum includes)
        {
            //depending on what it includes, include the relationship
            if(includes.HasFlag(TaskIncludes.SelfOnly))
            {

            }

            return base.ApplyIncludes(query, includes);
        }

        protected override void ApplyChangesToEntity(Task item, Enum includes)
        {
            if (includes == null || includes.HasFlag(TaskIncludes.SelfOnly))
            {
                _dbContext.UpdateGraph(item);
                return;
            }
        }
    }
}
