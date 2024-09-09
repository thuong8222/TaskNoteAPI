using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using VDWebApplication.Models;
using VDWebApplication.Services;

namespace VDWebApplication.Controllers
{
    public class TaskController : BaseController
    {
        [HttpGet]
        public JsonResponse ViewDetailTask(string id)
        {
            try
            {
                TaskService taskService = new TaskService();

                return Success(taskService.GetTaskById(id));
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpGet]
        public JsonResponse GetListTask(string? nameSearch)
        {
            try
            {
                TaskService taskService = new TaskService();

                return Success(taskService.GetListTask(nameSearch));
            }
            catch(Exception ex)
            {
                return Error( ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse CreateTask(Tasks model)
        {
            try
            {
                TaskService taskService = new TaskService();
                model.TaskId = Guid.NewGuid().ToString();
                model.UserId = Guid.NewGuid().ToString();
                model.TotalJob = 0;
                model.TotalCompletedJob = 0;
                if (model.TaskTitle == null) throw new Exception("Không được bỏ trống tiêu đề nhiệm vụ!");

               
                taskService.CreateTask(model);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse UpdateTask(Tasks model)
        {
            try
            {
                TaskService taskService = new TaskService();
                Tasks? task = taskService.GetTaskById(model.TaskId);
             
                if (task == null) throw new Exception("Khong tim thay cong viec");

                task.TaskTitle = model.TaskTitle;
                task.TotalJob = model.TotalJob;
                task.TotalCompletedJob = model.TotalCompletedJob;

                taskService.UpdateTask(model);

                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
            

        }
        [HttpGet]
        public JsonResponse DeleteTask(string? id)
        {
            try
            {
                TaskService taskService = new TaskService();
                Tasks task = taskService.GetTaskById(id);
                if (task == null) throw new Exception("Khong tim thay nhiem vu");

                taskService.DeleteTask(id);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
