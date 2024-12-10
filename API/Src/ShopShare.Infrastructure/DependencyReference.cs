using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopShare.Application.Services;
using ShopShare.Infrastructure.Authentication;
using ShopShare.Infrastructure.Model;
using ShopShare.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using System.Text;
using ShopShare.Application.Services.Repositories;
using ShopShare.Infrastructure.Repositories;
using ShopShare.Infrastructure.Mappers;
using System.Reflection;

namespace ShopShare.Infrastructure
{
    public static class DependencyReference
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AddAuthentication(serviceCollection, configuration);
            AddDatabase(serviceCollection, configuration);
            serviceCollection.RegisterMappers(Assembly.GetExecutingAssembly());

            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<IRolesRepository, RolesRepository>();
            serviceCollection.AddScoped<IShoppingListRepository, ShoppingListRepository>();


            return serviceCollection;
        }

        private static void AddDatabase(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ShopShareDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ShopShareDbContext>();
        }

        private static void AddAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            ConfigurationBinder.Bind(configuration.GetSection(JwtSettings.JwtSettingsName), jwtSettings);
            serviceCollection.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };

            serviceCollection.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            serviceCollection.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.TryGetValue("Authorization", out string? token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
