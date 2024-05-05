var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
string? connectionString;

if (builder.Environment.IsDevelopment())
    connectionString = configuration.GetConnectionString("DefaultConnection");
else
    connectionString = configuration.GetConnectionString("ServerConnection");

// Add services to the container.
builder.Services
    .AddPresentationServices(configuration)
    .AddInfrastructureServices(connectionString)
    .AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
await DefaultRoles.SeedAsync(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
await DefaultUsers.SeedAdminUserAsync(scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>());

app.MapControllers();

#region MapGroups
app.MapGroup("/v1").VersionOneGroup();
#endregion

app.Run();
