using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Helpers
{
    public class ResourceParameters
    {
        const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _PageSize = 10;

        public int PageSize 
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public string Name { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public String SearchQuery { get; set; }
        public string OrderBy { get; set; } = "Name";
        

    }
}
