using Api.Misc;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Configuration
        builder
            .Services.AddOptionsWithValidateOnStart<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations();
        builder.Services.AddSingleton(_ => TimeProvider.System);
        #endregion

        #region Data Access
        var connectionString = builder.Configuration.GetConnectionString("AppDb");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options
                .UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        #endregion

        // Add services to the container.
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                // Globally require users to be authenticated
                .RequireAuthenticatedUser()
                .Build();
        });
        builder
            .Services.AddIdentityApiEndpoints<IdentityUser>(
                (options) =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddSingleton<IEmailSender<IdentityUser>, AppEmailSender>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
        }
        app.UsePathBase("/api");

        app.UseRouting();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication(); // Security
        app.UseAuthorization();

        app.MapIdentityApi<IdentityUser>().AllowAnonymous();

        app.MapControllers();

        app.Run();
    }
}
