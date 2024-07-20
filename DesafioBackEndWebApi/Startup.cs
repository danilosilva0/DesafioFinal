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

            services.AddTransient<ApiMiddleware>();

            services.AddTransient<PacienteContextoMiddleware>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // services.AddFluentConfiguration();

            //Acredito que o JWT/autorização/autenticação esteja impedindo de alguma forma que o sistema execute suas funções
            //básicas, por isso comentei
            // services.AddAutorizacaoConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistema de Agendamento de Vacinação",
                    Version = "v1",
                    Description = "API para agendamento de vacinação contra COVID-19.",
                    Contact = new() { Name = "Danilo Silva - Github", Url = new Uri("https://github.com/danilosilva0") },
                    License = new() { Name = "Private", Url = new Uri("http://google.com.br") },
                    TermsOfService = new Uri("http://google.com.br")
                });

                // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     In = ParameterLocation.Header,
                //     Description = "Insira o token",
                //     Name = "Autorização",
                //     Type = SecuritySchemeType.ApiKey
                // });

                // c.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
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

            app.UseCors("AllowAll");

            // app.UseAuthentication();
            // app.UseAuthorization();

            // app.UseMiddleware<ApiMiddleware>();
            // app.UseMiddleware<PacienteContextoMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
