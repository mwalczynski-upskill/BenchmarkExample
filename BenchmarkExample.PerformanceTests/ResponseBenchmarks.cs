namespace BenchmarkExample.PerformanceTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Logging;

    [InProcess]
    [MemoryDiagnoser]
    public class ResponseBenchmarks
    {
        private HttpClient _client;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(configuration =>
                {
                    configuration.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                    });
                });

            _client = factory.CreateClient();
        }

        [Benchmark]
        public Task GetAllValuesResponseTime()
        {
            return _client.GetAsync("/api/values");
        }

        [Benchmark]
        public Task GetValueResponseTime()
        {
            return _client.GetAsync("/api/values/5");
        }
    }
}
