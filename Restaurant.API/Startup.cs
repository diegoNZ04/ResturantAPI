using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurant.Application.DTOs.Requests.ReservesRequests;
using Restaurant.Application.DTOs.Requests.TablesRequests;
using Restaurant.Application.Mappings;
using Restaurant.Application.Services;
using Restaurant.Application.Services.Interfaces;
using Restaurant.Application.Validators.ReserveValidators;
using Restaurant.Application.Validators.TableValidators;
using Restaurant.Infra.Data;
using Restaurant.Infra.Repositories;
using Restaurant.Infra.Repositories.Interfaces;

namespace Restaurant.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "ResturantAPI",
                Version = "V2",
                Description = "Application for managing table reservations in restaurants",
                Contact = new OpenApiContact
                {
                    Name = "Diego Amorim",
                    Email = "diegoamorim03152004@gmail.com"
                },
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter the JWT token in the field below."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                }
            });
        });

        services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["JwtSettings:Issuer"],
                ValidAudience = Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]))
            };
        });

        services.AddAuthorization();

        services.AddAutoMapper(
            typeof(AuthProfile),
            typeof(ReserveProfile),
            typeof(TableProfile)
        );

        services.AddScoped<IValidator<CreateReserveRequest>, CreateReserveRequestValidator>();
        services.AddScoped<IValidator<UpdateReserveRequest>, UpdateReserveRequestValidator>();
        services.AddScoped<IValidator<CreateTableRequest>, CreateTableRequestValidator>();
        services.AddScoped<IValidator<UpdateTableRequest>, UpdateTableRequestValidator>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IReserveRepository, ReserveRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IHasherService, HasherService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IReserveService, ReserveService>();
        services.AddScoped<ITableService, TableService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "RestaurantAPI v2");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}