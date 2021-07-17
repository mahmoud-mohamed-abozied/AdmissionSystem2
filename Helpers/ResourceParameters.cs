using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Helpers
{
    public class ResourceParameters
    {
        const int MaxPageSize = 20;

        public int PageNumber = 1;

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

        public string SearchQuery { get; set; }

    }
}
