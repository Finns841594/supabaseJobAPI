using Newtonsoft.Json;

public class JobResponse
{
    public Guid Id {get; set;}
    public String Url {get; set;} = "";
    
    [JsonProperty("job_text")]
    public String JobText {get; set;} = "";
    public DateTime CreatedAt {get; set;}
}