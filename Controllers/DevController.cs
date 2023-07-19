using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace supabaseJobAPI.Controllers
{
    [ApiController]
    [Route("api/devs")]
    public class DevController : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAll(Supabase.Client client)
        {
            var response = await client.From<Developer>().Get();

            var developersString = response.Content;
            var developers = JsonConvert.DeserializeObject<List<DeveloperResponse>>(developersString);

            return Results.Ok(developers);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetDeveloperById(Supabase.Client client, Guid id)
        {
        var response = await client
            .From<Developer>()
            .Where(dev => dev.Id == id)
            .Get();
        
        var dev = response.Models.FirstOrDefault();

        if (dev is null)
        {
            return Results.NotFound();
        }

        var developerResponse = new DeveloperResponse
        {
            Id = dev.Id,
            Name = dev.Name,
            Email = dev.Email,
            CreatedAt = dev.CreatedAt,
        };

        return Results.Ok(developerResponse);
        }

        [HttpPost]
        public async Task<IResult> CreateDeveloper(Supabase.Client client, CreateDeveloperRequest request)
        {
        var developer = new Developer
        {
            Name = request.Name,
            Email = request.Email,
        };
        
        var response = await client.From<Developer>().Insert(developer);
        
        var newDeveloper = response.Models.First();

        return Results.Ok(newDeveloper.Id);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateDeveloper(Supabase.Client client, Guid id, CreateDeveloperRequest request)
        {
        var responseOldDeveloper = await client
        .From<Developer>()
        .Where(dev => dev.Id == id)
        .Get();
        
        var oldDeveloper = responseOldDeveloper.Models.FirstOrDefault();

        if (oldDeveloper is null)
        {
            return Results.NotFound();
        }
        
        var update = await client
        .From<Developer>()
        .Where(dev => dev.Id == id)
        .Set(dev => dev.Name!, request.Name)
        .Set(dev => dev.Email!, request.Email)
        .Update();
        
        var response = await client
        .From<Developer>()
        .Where(dev => dev.Id == id)
        .Get();
        
        var updatedDeveloper = response.Models.First();

        return Results.Ok(updatedDeveloper.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteDeveloper(Supabase.Client client, Guid id)
        {
        await client
            .From<Developer>()
            .Where(dev => dev.Id == id)
            .Delete();

        return Results.NoContent();
        }
    }
}