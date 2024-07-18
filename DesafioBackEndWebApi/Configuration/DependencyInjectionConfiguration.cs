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
using Desafio.Repository;
using Desafio.Helper.PacienteContexto;
using Desafio.Helper.Configuracoes;

namespace DesafioBackEndWebApi.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuracao)
        {
            InjectRepository(services);
            InjectService(services);
            InjectMiddleware(services);

            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IPacienteContexto, PacienteContexto>();
            services.AddOptions<AutenticacaoConfig>().Bind(configuracao.GetSection("Authorization"));
        }

        private static void InjectMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
            services.AddTransient<PacienteContextoMiddleware>();
        }

        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IPacienteService, PacienteService>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
        }
    }
}