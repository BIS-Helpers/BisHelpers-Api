namespace BisHelpers.web.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddSwaggerGenCustom(this IServiceCollection service)
    {
        service.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc(Versions.Version1, new() { Title = $"BisHelpers {Versions.Version1}", Version = Versions.Version1 });
            setupAction.SwaggerDoc(Versions.Version2, new() { Title = $"BisHelpers {Versions.Version2}", Version = Versions.Version2 });

            setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your bearer token in this format: Bearer {your-token}"
            });

            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme{Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "Bearer"}},
                    new List<string>()
                }
            });
        });

        return service;
    }

    public static IServiceCollection AddAuthenticationCustom(this IServiceCollection service, IConfiguration configuration)
    {
        var authenticationIssuer = configuration["Authentication:Issuer"];
        var authenticationAudience = configuration["Authentication:Audience"];
        var authenticationKey = configuration["Authentication:Key"];

        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authenticationIssuer,
                ValidAudience = authenticationAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationKey!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return service;
    }

    public static IServiceCollection AddIdentityCustom(this IServiceCollection service)
    {
        service.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return service;
    }

}
