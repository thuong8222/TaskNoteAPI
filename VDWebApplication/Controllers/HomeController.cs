using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using VDWebApplication.Models;
using VDWebApplication.Services;

namespace VDWebApplication.Controllers
{
   
    public class HomeController : BaseController
    {
        [HttpGet]
        public JsonResponse Divide(int a, int b)
        {
            try {
                if (b == 0) throw new Exception("b khong duoc bang khong");

                return Success((decimal)a / b, "tinh toan thanh cong");
            }
            catch (Exception ex) {
                return Error(ex.Message);
            }
        }

        [HttpGet]
        public JsonResponse GetListUser(string? id, string? nameSearch)
        {
            try
            {
                UserService userService = new UserService();    

                return Success(userService.GetListUser(id, nameSearch));
            }
            catch (Exception ex){
                return Error(ex.Message);

            }
           
        }
        [HttpPost]
        public JsonResponse CreateUser(User model)
        {
            try {
                UserService userService = new UserService();
                model.UserId = Guid.NewGuid().ToString();
                userService.CreateUser(model);
                return Success();


                    }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse UpdateUser(User model)
        {
            try
            {
                UserService userService = new UserService();
                User? user = userService.GetUserById(model.UserId);
                if (user == null) throw new Exception("Khong tim thay nguoi dung");

                user.UserName=model.UserName;
                user.UserAge=model.UserAge;
                user.Role=model.Role;

                userService.UpdateUser(user);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResponse DeleteUser(string id)
        {
            try
            {
                UserService userService = new UserService();
                User? user = userService.GetUserById(id);
                if (user == null) throw new Exception("Khong tim thay nguoi dung");

              

                userService.DeleteUser(id);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}
