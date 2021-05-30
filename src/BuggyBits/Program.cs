// using App.Metrics.AspNetCore;
// using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace BuggyBits
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new MetricServer(port: 9998);
            server.Start();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //Host.CreateDefaultBuilder(args)
            //    .ConfigureMetrics()
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>();
            //    })
            //.UseMetrics();
            Host.CreateDefaultBuilder(args)
                //.UseMetricsWebTracking()
                //.UseMetrics(options => {
                //    options.EndpointOptions = endpointsOptions =>
                //    {
                //        endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                //        endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                //        endpointsOptions.EnvironmentInfoEndpointEnabled = false;
                //    };
                //})
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
