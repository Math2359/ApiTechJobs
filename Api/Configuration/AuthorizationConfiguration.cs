using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.Options;
using System.Text;

namespace Api.Configuration;

public static class AuthorizationConfiguration
{
    /// <summary>
    /// Configura a autenticação
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>Builder configurado com a autenticação</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static WebApplicationBuilder RegistrarAutenticaco(this WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

        var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
        builder.Services.Configure<JwtSettings>(jwtSection);

        builder.Services
            .AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey ?? throw new NullReferenceException("Chave não identificada!"))),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = [jwtSettings.Audience],
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
                jwt.Audience = jwtSettings.Audience;
                jwt.ClaimsIssuer = jwtSettings.Issuer;
            });
        
        return builder;
    }

    /// <summary>
    /// Configura o swagger com a autenticação
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Services com swagger configurado</returns>
    public static IServiceCollection AdicionarSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opcao =>
        {
            opcao.SwaggerDoc("v1", new OpenApiInfo { Title = "TechJobs API", Version = "v1" });
            opcao.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Insira o token para validar",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            opcao.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        return services;
    }
}