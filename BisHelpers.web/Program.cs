var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString(
    builder.Environment.IsDevelopment() ? "DefaultConnection" : "ServerConnection");

// Add services to the container.
builder.Services
    .AddPresentationServices(configuration)
    .AddInfrastructureServices(connectionString)
    .AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger().UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/{Versions.Version1}/swagger.json", $"BisHelpers {Versions.Version1}");
        options.SwaggerEndpoint($"/swagger/{Versions.Version2}/swagger.json", $"BisHelpers {Versions.Version2}");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

await app.SeedUserAndRoles(app.Services);

app.MapControllers();

#region MapGroups
app.MapGroup($"/{Versions.Version1}").VersionOneGroup().WithGroupName(Versions.Version1);
app.MapGroup($"/{Versions.Version2}").VersionTwoGroup().WithGroupName(Versions.Version2);
#endregion

app.Run();