using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Supabase.Gotrue;

namespace supabaseWebApi.Test;

public class ApiTestSets: IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;
    public ApiTestSets(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task TestDevAPIGetAll()
    {
        var response = await _client.GetAsync("/api/devs");
        
        // var responseString = await response.Content.ReadAsStringAsync();
        // Console.WriteLine("❗️: ", responseString);
        Console.WriteLine(response);
        Assert.Equal("OK", response.StatusCode.ToString());
    }
}