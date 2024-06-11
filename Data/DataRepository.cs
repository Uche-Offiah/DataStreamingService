using DataStreamingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamingService.Data
{
    public class DataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<StreamData>> GetDataAsync()
        {
            return await _context.StreamData.ToListAsync();
        }
    }
}
