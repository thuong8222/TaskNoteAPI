using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDWebApplication.Models;

namespace VDWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public JsonResponse Success(object? data = null, string? message=null)
        {

            return new JsonResponse() { status = "success", data=data,message=message };
        }
        [HttpGet]
        public JsonResponse Error( string? message = null)
        {

            return new JsonResponse() { status = "error",  message = message };
        }
    }
}
