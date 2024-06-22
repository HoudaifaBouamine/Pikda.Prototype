using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pikda.Domain.Interfaces;
using Pikda.Infrastructure;
using System;
using System.Windows.Forms;

namespace Pikda.Win
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
           
        }
        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IOcrRepository , OcrRepository>();
                    services.AddTransient<MainForm>();
                });
        }
    }
}

