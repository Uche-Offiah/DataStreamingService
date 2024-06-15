using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamingService.Models
{
    public class ProcessedData
    {
        public DateTime TimePeriod { get; set; }
        public int Count { get; set; }
        public List<string>? Values { get; set; }
    }
}
