using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;

namespace DesafioBackEndWebApi
{
    public static class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                _log.Info("Iniciando a API");
                var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

                webHost.Build().Run();
            }
            catch (Exception ex)
            {
                _log.Fatal("Erro fatal", ex);
                throw;
            }
        }
    }
}