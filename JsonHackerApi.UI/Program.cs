using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace JsonHackerApi.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            _ = new Init(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<MainForm>().Run();
        }
    }
}
