using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace JsonHackerApi.UI
{
    class Init
    {
        // pass in IServiceCollection which add Nlog logging as a service which will be injected 
        // as a dependency in the UI
        public Init(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog("nlog.config");
            });

            // services will be provided for each scope
            services.AddScoped<MainForm>();
        }
    }
}
