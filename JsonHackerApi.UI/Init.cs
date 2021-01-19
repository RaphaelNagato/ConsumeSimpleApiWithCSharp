using GetApi;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonHackerApi.UI
{
    class Init
    {
        public Init(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog("nlog.config");
            });
            services.AddScoped<MainForm>();
        }
    }
}
