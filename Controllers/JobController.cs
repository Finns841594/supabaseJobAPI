using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace supabaseJobAPI.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetAll(Supabase.Client client)
    {
        var response = await client.From<Job>().Get();

        var jobsString = response.Content;
        var jobs = JsonConvert.DeserializeObject<List<JobResponse>>(jobsString);

        return Results.Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetJobById(Supabase.Client client, Guid id)
    {
       var response = await client
        .From<Job>()
        .Where(job => job.Id == id)
        .Get();
    
    var job = response.Models.FirstOrDefault();

    if (job is null)
    {
        return Results.NotFound();
    }

    var jobResponse = new JobResponse
    {
        Id = job.Id,
        Url = job.Url,
        JobText = job.JobText,
        CreatedAt = job.CreatedAt,
    };

    return Results.Ok(jobResponse);
    }

    [HttpPost]
     public async Task<IResult> CreateJob(Supabase.Client client, CreateJobRequest request)
    {
   var job = new Job
    {
        Url = request.Url,
        JobText = request.JobText,
    };
    
    var response = await client.From<Job>().Insert(job);
    
    var newJob = response.Models.First();

    return Results.Ok(newJob.Id);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateJob(Supabase.Client client, Guid id, CreateJobRequest request)
    {
     var responseOldJob = await client
    .From<Job>()
    .Where(job => job.Id == id)
    .Get();
    
    var oldJob = responseOldJob.Models.FirstOrDefault();

    if (oldJob is null)
    {
        return Results.NotFound();
    }
    
    var update = await client
    .From<Job>()
    .Where(job => job.Id == id)
    .Set(job => job.Url!, request.Url)
    .Set(job => job.JobText!, request.JobText)
    .Update();
    
    var response = await client
    .From<Job>()
    .Where(job => job.Id == id)
    .Get();
    
    var updatedJob = response.Models.First();

    return Results.Ok(updatedJob.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteJob(Supabase.Client client, Guid id)
    {
       await client
        .From<Job>()
        .Where(job => job.Id == id)
        .Delete();

    return Results.NoContent();
    }
}
