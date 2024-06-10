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

        public async Task<List<StreamData>> ProcessDataAsync()
        {
            var data = await _repository.GetDataAsync();
            // Perform any business logic here
            return data;
        }
    }
}
