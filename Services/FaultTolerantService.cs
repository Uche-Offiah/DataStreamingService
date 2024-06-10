using DataStreamingService.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamingService.Services
{
    public class FaultTolerantService
    {
        private readonly DataService _dataService;

        public FaultTolerantService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<StreamData>> GetDataWithFaultToleranceAsync()
        {
            var retryPolicy = Policy.Handle<Exception>()
                .RetryAsync(3);

            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

            return await retryPolicy.ExecuteAsync(async () =>
            {
                return await circuitBreakerPolicy.ExecuteAsync(async () =>
                {
                    return await _dataService.ProcessDataAsync();
                });
            });
        }
    }
}
