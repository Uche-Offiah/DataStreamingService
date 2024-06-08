using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<Data>> GetDataAsync()
        {
            return await _context.Data.ToListAsync();
        }
    }
}
