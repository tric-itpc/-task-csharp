using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Services;

namespace MyAPI.Controllers.Main
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private IRepository _repo;

        public MainController(IRepository repo, IServiceEventHandler serviceEventHandler)
        {
            _repo = repo;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_repo.GetServicesInfo());
        }
    }
}

