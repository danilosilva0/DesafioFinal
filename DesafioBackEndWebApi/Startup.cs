using Microsoft.OpenApi.Models;
using Desafio.Repository.Repositories;
using Desafio.Repository.Interface.IRepositories;
using Desafio.Service.Interface.Services;
using Desafio.Service.Services;
using Microsoft.EntityFrameworkCore;
using Desafio.Repository;
using System;
using DesafioBackEndWebApi.Configuration;
using DesafioBackEndWebApi.Middleware;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;


namespace DesafioBackEndWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           
            services.AddDependencyInjectionConfiguration(Configuration);

            services.AddDatabaseConfiguration(Configuration);

            // services.AddFluentConfiguration();

            // services.AddAuthorizationConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistema de Agendamento de Vacinação",
                    Version = "v1",
                    Description = "APIs para agendamento de vacinação contra COVID-19.",
                    Contact = new() { Name = "Danilo Silva", Url = new Uri("http://google.com.br") },
                    License = new() { Name = "Private", Url = new Uri("http://google.com.br") },
                    TermsOfService = new Uri("http://google.com.br")
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Agendamento de Vacinação v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ApiMiddleware>();
            app.UseMiddleware<PacienteContextoMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
