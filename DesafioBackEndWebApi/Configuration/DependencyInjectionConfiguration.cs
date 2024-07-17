using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Service.Interface.Services;
using Desafio.Service.Services;
using Desafio.Repository.Interface.IRepositories;
using Desafio.Repository.Repositories;
using DesafioBackEndWebApi.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Repository.Interface;

namespace DesafioBackEndWebApi.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuracao)
        {
            InjetarRepositorio(services);
            InjetarNegocio(services);
            InjetarMiddleware(services);

            services.AddScoped<ITransactionManager, GerenciadorTransacao>();
            services.AddScoped<IPacienteContexto, PacienteContexto>();
            services.AddOptions<AutenticacaoConfig>().Bind(configuracao.GetSection("Authorization"));
        }

        private static void InjetarMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
            services.AddTransient<PacienteContextoMiddleware>();
        }

        private static void InjetarNegocio(IServiceCollection services)
        {
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IPacienteService, PacienteService>();
        }

        private static void InjetarRepositorio(IServiceCollection services)
        {
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
        }
    }
}