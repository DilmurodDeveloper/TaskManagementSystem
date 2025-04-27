using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Project list");
        }
    }
}
