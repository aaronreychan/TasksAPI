using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.SearchCriteria;
using DataAccess.Models;

namespace Business.Managers
{
    public class TaskManager
    {
        #region constructor
        protected UnitOfWork _unitOfWork;

        public TaskManager()
            : this(new UnitOfWork())
        {

        }

        public TaskManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork != null ? unitOfWork : (new UnitOfWork());

        }
        #endregion

        public List<Task> GetTaskList(TaskSearchCriteria searchCriteria)
        {
            return GetTaskList(searchCriteria, TaskIncludes.SelfOnly);
        }

        public List<Task> GetTaskList(TaskSearchCriteria searchCriteria, TaskIncludes includes)
        {
            return _unitOfWork.TaskRepo.Matches(searchCriteria, includes).ToList();
        }

        public Task GetTask(TaskSearchCriteria searchCriteria)
        {
            return GetTask(searchCriteria, TaskIncludes.SelfOnly);
        }

        public Task GetTask(TaskSearchCriteria searchCriteria, TaskIncludes includes)
        {
            var TaskLst = GetTaskList(searchCriteria, includes);

            if (TaskLst == null || TaskLst.Count == 0)
                return null;
            else
                return TaskLst.First();

        }

        public Task Save(Task item, TaskIncludes includes)
        {
            if (item == null)
                return null;

            if (item.Id == 0)
                _unitOfWork.TaskRepo.AddObject(item);                    //insert
            else
                _unitOfWork.TaskRepo.ApplyChanges(item, includes);       //update

            _unitOfWork.Commit();

            return item;
        }
    }
}
