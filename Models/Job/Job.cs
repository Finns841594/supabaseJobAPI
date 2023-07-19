using Postgrest.Attributes;
using Postgrest.Models;

[Table("JobDescriptions")]
public class Job : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id {get; set;}
    [Column("url")]
    public String? Url {get; set;}
    [Column("job_text")]
    public String? JobText {get; set;}
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
}
