using System;
using System.Text;
using AuthGuard.API.Contracts;
using AuthGuard.API.Infrastructure;
using AuthGuard.API.Infrastructure.AuthenticationManager;
using AuthGuard.API.Models.Config;
using AuthGuard.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AuthGuard.API.Extensions
{
    public static class ServiceBuilder
    {
        public static void ConfigureSwaggerGenCollection(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "AuthGuard.API", Version = "v1"});

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
        }
        
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthManager, AuthManager>();
            services.AddSingleton<IUserService, UserService>();
        }
        public static void ConfigureAuthenticationCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetSection("tokenConfig").Get<TokenConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }
        
        public static void ConfigDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfig>(configuration.GetSection("tokenConfig"));
        }
    }
}