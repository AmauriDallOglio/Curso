
using curso.Api.Business.Repositories;
using curso.Api.Infra.Data;
using curso.Api.Infra.Data.Repositories;
using curso.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace curso.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.Development.json")
                .Build();


            builder.Services.AddControllers()
                            .ConfigureApiBehaviorOptions(options =>{
                                     options.SuppressModelStateInvalidFilter = true;
                                 });



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


  ;
            var secret = Encoding.ASCII.GetBytes(configuration.GetSection("JwTConfigurations:Secret").Value);
            builder.Services.AddAuthentication(x =>
                                {
                                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                })
                                .AddJwtBearer(x =>
                                {
                                    x.RequireHttpsMetadata = false;
                                    x.SaveToken = true;
                                    x.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                                        ValidateIssuer = false,
                                        ValidateAudience = false
                                    };
                                });



            builder.Services.AddDbContext<CursoDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("connectionString")));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ICursoRepository, CursoRepository>();
            builder.Services.AddScoped<IAuthenticationServices, JwtService>();



            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
   
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
 

            app.Run();
        }
    }
}
