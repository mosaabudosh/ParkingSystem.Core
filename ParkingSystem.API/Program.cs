
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ParkingSystem.API.Middleware;
using ParkingSystem.API.Model;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Models;
using ParkingSystem.Application.Security;
using ParkingSystem.Application.Services;
using ParkingSystem.Application.ServicesRegistration;
using ParkingSystem.Domain.IRepositories;
using ParkingSystem.Infrastructure.Data.ServicesRegistration;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    //IHubContext<NotificationHub> notificationHub = new IHubContext<NotificationHub>();

    // Add services to the container.
    builder.Services.AddControllers(p =>
    {
        p.ValueProviderFactories.Insert(0, new CustomQueryStringParserFactory());
        AuthorizationPolicy puthorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        p.Filters.Add(new AuthorizeFilter(puthorizationPolicy));
    }).AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

    AppSettings appSettings = builder.Configuration.Get<AppSettings>();
    string[] origins = appSettings.origins.Split(',');

    //string[] origins = builder.Configuration.GetSection("origins").ToString().Split(',');
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Policy", builder => builder
            .WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );
    });
    ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
    builder.Services.ConfigureInfrastructureDataService(builder.Configuration);
    builder.Services.ConfigureParkingSystemService(serviceProvider);

    builder.Services.Configure<TokenOptions>(options => builder.Configuration.GetSection("TokenOptions").Bind(options));
    SigningCredentialsConfiguration signingConfigurations = new SigningCredentialsConfiguration();
    builder.Services.AddSingleton(signingConfigurations);

    builder.Services.AddScoped<ICheckTheDurationOfReservations, CheckTheDurationOfReservations>();

    TokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                context.Fail("Unauthorized");
                return Task.CompletedTask;
            },
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            RequireExpirationTime = true,
            IssuerSigningKey = signingConfigurations.Key,
        };
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1);
        });
    }

    app.UseCors("Policy");

    app.UseHttpsRedirection();

    app.UseAuthentication();

    //app.UseMiddleware<Interceptor>(builder.Configuration);

    app.MapControllers();

    app.MapHub<NotificationHub>("/hubs/notification");

    app.UseDeveloperExceptionPage();

    app.Run();
}
catch (Exception ex)
{
    while (ex.InnerException != null) ex = ex.InnerException;
    using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\_log.txt", true))
    {
        outputFile.WriteLine($"Program : {ex.Message}");
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            outputFile.WriteLine(ex.Message);
        }
        outputFile.WriteLine("*****************");
    }
}

