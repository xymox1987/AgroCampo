using AgroCampo_Common.Models;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgroCampo_Business.Helper
{
    public static class HelperUrl
    {
        public static string GetCurrentURlbase(IHttpContextAccessor context)
        {
            var request = context.HttpContext.Request;
            var _baseURL = $"{request.Scheme}://{request.Host}";

            return _baseURL;
        }
        
    }

    
}



