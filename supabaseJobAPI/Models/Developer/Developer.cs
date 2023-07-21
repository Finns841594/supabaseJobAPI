using Postgrest.Attributes;
using Postgrest.Models;


[Table("Developers")]
public class Developer : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id {get; set;}
    [Column("name")]
    public String? Name {get; set;}
    [Column("email")]
    public String? Email {get; set;}
    [Column("created_at")]
    public DateTime CreatedAt {get; set;}
}

