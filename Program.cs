using Supabase;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Supabase.Client>(_ => 
new Supabase.Client(
   //Insert SupabaseURL below
   Environment.GetEnvironmentVariable("SUPABASE_URL")!,
   //Insert SupabaseAPIKey below
   Environment.GetEnvironmentVariable("SUPABASE_APIKEY"),
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {};