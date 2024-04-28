var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddPresentationServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

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
app.MapGroup("/Auth").GroupAuth();
#endregion

app.Run();
