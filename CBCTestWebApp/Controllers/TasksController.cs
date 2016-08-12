using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common.Enums;
using Business.Managers;
using DataAccess.Models;
using DataAccess.SearchCriteria;
using CBCTestWebApp.Models.Dto;
using Abp.AutoMapper;
using Abp.Authorization;
using Common;

namespace CBCTestWebApp.Controllers
{
    //[AbpAuthorize("PermissionBased")]
    public class TasksController : BaseController
    {
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }

        //If the goal is to have a separate API that is to be consumed by multiple application internal / external
        //will need to create oauth to check access
        //add permission based restriction
        //[AllowAnonymous]
        //public JsonResult GetTasks(int? priorityType)
        //{
        //    List<Task> retVal = null;

        //    try
        //    {
        //        TaskPriorityEnum priority = TaskPriorityEnum.All;
        //        if (priorityType.HasValue)
        //        {
        //            priority = (TaskPriorityEnum)priorityType.Value;
        //        }
        //        retVal = new TaskManager().GetTaskList(
        //                    new TaskSearchCriteria()
        //                    {
        //                        Priority = new List<TaskPriorityEnum> { priority }
        //                    });
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log
        //    }

        //    return Json(retVal, JsonRequestBehavior.AllowGet); ;
        //}

        //[AbpAuthorize("AllowGet")]
        [AllowAnonymous]
        public JsonResult GetTasks(GetTasksInput input)
        {
            List<GetTasksOutput> retVal = null;

            try
            {
                TaskPriorityEnum priority = TaskPriorityEnum.All;
                if (input.PriorityLookupId.HasValue)
                {
                    priority = (TaskPriorityEnum)input.PriorityLookupId.Value;
                }
                List<Task> tasks = new TaskManager().GetTaskList(
                            new TaskSearchCriteria()
                            {
                                Priority = new List<TaskPriorityEnum> { priority }
                            });
                //retVal = tasks.MapTo<List<GetTasksOutput>>();
                //will create my mappers another time

                //using DTO as not all properties/columns shuold be accesible 
                //properties can also be extended specifically to this dto
                retVal = new List<GetTasksOutput>();
                GetTasksOutput taskOutput = null;
                foreach(Task task in tasks)
                {
                    taskOutput = new GetTasksOutput();
                    taskOutput.Id = task.Id;
                    taskOutput.CreatedBy = task.CreatedBy;
                    if (task.CreatedDate.HasValue)
                    {
                        taskOutput.CreatedDate = DateTimeHelper.DateTimeEasternStandardTime(task.CreatedDate.Value);
                    }
                    taskOutput.Description = task.Description;
                    taskOutput.PriorityLookupId = task.PriorityLookupId;
                    if (task.UpdateDate.HasValue)
                    {
                        taskOutput.UpdateDate = DateTimeHelper.DateTimeEasternStandardTime(task.UpdateDate.Value); ;
                    }
                    taskOutput.UpdatedBy = task.UpdatedBy;
                    retVal.Add(taskOutput);
                }
            }
            catch (Exception ex)
            {
                //Log
            }

            return Json(retVal, JsonRequestBehavior.AllowGet); ;
        }
    }
}