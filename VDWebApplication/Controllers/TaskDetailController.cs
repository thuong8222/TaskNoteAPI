using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using VDWebApplication.Models;
using VDWebApplication.Services;

namespace VDWebApplication.Controllers
{
    public class TaskDetailController : BaseController
    {
        [HttpGet]
        public JsonResponse GetListTaskDetail(string? id)
        {
            try
            {
                TaskDetailService taskDetailService = new TaskDetailService();

                return Success(taskDetailService.GetListTaskDetail(id));
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        }
        [HttpGet]
        public JsonResponse ViewTaskDetailById(string? id)
        {
            try {
                TaskDetailService taskDetailService = new TaskDetailService();
                return Success(taskDetailService.GetTaskDetailById(id));
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse CreateTaskDetail(TaskDetail model)
        {
            try
            {
                TaskDetailService taskDetailService = new TaskDetailService();
                model.TaskDetailId = Guid.NewGuid().ToString();
                taskDetailService.CreateTaskDetail(model);
                int quantity = 1;
                taskDetailService.UpdateTotalJob(model.TaskId, quantity);
               
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse UpdateTaskDetail(TaskDetail model) //model: dl dc truyền từ giao diện lên
        {
            try
            {
                TaskDetailService taskDetailService = new TaskDetailService();
                TaskDetail? taskDetail = taskDetailService.GetTaskDetailById(model.TaskDetailId); //lấy từ trong csdl

                if (taskDetail == null) throw new Exception("Không tìm thấy chi tiết công việc này!");
                if (model.Description == null) throw new Exception("Mô tả không được để trống!");

                taskDetail.Description = model.Description;
                if(model.Status != taskDetail.Status )
                {
                    if(model.Status == "DONE")
                    {
                        taskDetailService.UpdateTotalCompletedJob(taskDetail.TaskId, 1);
                    }else if (model.Status == "PENDING")
                    {
                        taskDetailService.UpdateTotalCompletedJob(taskDetail.TaskId, -1);
                    }
                }    

                taskDetail.Status = model.Status;       
                taskDetailService.UpdateTaskDetail(taskDetail);

                return Success();
            }
            catch (Exception ex)
            {
               return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResponse DeleteTaskDetail(string? id)
        {
            try
            {
                TaskDetailService taskDetailService = new TaskDetailService();
                TaskDetail? taskDetail = taskDetailService.GetTaskDetailById(id);
                if ( taskDetail== null) throw new Exception("Khong tim thay chi tiet cong viec nay");
                taskDetailService.DeleteTaskDetail(id);
                int quantity = -1;
                taskDetailService.UpdateTotalJob(id, quantity);
                taskDetailService.UpdateTotalCompletedJob(id, quantity);
                return Success();

            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
