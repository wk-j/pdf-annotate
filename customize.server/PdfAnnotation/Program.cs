using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CentralLogProvider;
using Serilog;
using Serilog.Sinks.SystemConsole;

namespace PdfAnnotation {
    public class Program {
        public static void Main(string[] args) {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {

            Log.Logger =
                new LoggerConfiguration().WriteTo.Console().CreateLogger();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(config => {
                    config.ClearProviders();
                    config.AddSerilog();
                    // config.AddCentralLog(new CentralLogOptions("https://central-logger.azurewebsites.net"));
                })
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
        }
    }
}
