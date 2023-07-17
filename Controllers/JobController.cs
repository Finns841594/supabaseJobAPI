using Microsoft.AspNetCore.Mvc;
using supabaseJobAPI.Models;

namespace supabaseJobAPI.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Job>> Get()
    {
        return Ok("Get a job!");
    }
}
