﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Models
{
    public class DocumentForUpdate
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string Copy { get; set; }
    }
}
