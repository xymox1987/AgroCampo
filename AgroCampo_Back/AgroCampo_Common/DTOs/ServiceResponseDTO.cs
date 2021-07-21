using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Reflection;

namespace ESDAVDomain.DTOs
{
    
    public class ServiceResponseDTO<T>
    {

        public Int32? CountRecords { get; set; } 
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public T Data { get; set; }

        public ServiceResponseDTO()
        {
            Message = "OK";
        }

    }
}

