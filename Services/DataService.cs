using DataStreamingService.Data;
using DataStreamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamingService.Services
{
    public class DataService
    {
        private readonly DataRepository _repository;

        public DataService(DataRepository repository)
        {
            _repository = repository;
        }

        //public async Task<List<StreamData>> ProcessDataAsync()
        //{
        //    var data = await _repository.GetDataAsync();
        //    // Perform any business logic here
        //    return data;
        //}

        public async Task<List<ProcessedData>> ProcessDataAsync()
        {
            var data = await _repository.GetDataAsync();

            // Filter data within the last 24 hours
            var filteredData = data.Where(d => d.Timestamp >= DateTime.UtcNow.AddHours(-24)).ToList();

            // Transform data (convert value to uppercase)
            foreach (var entry in filteredData)
            {
                entry.Value = entry.Value?.ToUpper();
            }

            // Aggregate data by hour
            var aggregatedData = filteredData
                .GroupBy(d => new { d.Timestamp.Year, d.Timestamp.Month, d.Timestamp.Day, d.Timestamp.Hour })
                .Select(g => new ProcessedData
                {
                    TimePeriod = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, 0, 0, DateTimeKind.Utc),
                    Count = g.Count(),
                    Values = g.Select(d => d.Value).ToList()
                })
                .ToList();

            return aggregatedData;
        }
    }
}
