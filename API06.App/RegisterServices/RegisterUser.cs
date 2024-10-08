using API06.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace API06.App.RegisterServices;

public static class RegisterUser
{
    public static IServiceCollection RegisterUserServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(opt =>
        {
            opt.Password.RequiredUniqueChars = 1;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = true;


//lockout
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.Lockout.AllowedForNewUsers = true;

            opt.User.RequireUniqueEmail = false;
            opt.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        });
        return services;
    }
}