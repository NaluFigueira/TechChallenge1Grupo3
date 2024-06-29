using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Api.Configuration;

using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder();
#if DEBUG
Console.WriteLine("Mode=Debug");
configurationBuilder
    .AddJsonFile("appsettings.Development.json");
#else
Console.WriteLine("Mode=Release"); 
configurationBuilder
    .AddJsonFile("appsettings.json");
#endif

var startup = new Startup(configurationBuilder.Build());
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);
app.ApplyMigrations();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapContactEndpoints();
app.MapUserEndpoints();
app.MapAuthenticationEndpoints();
app.MapMetrics();

app.Run();
