using AgroCampo_Common.Models;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgroCampo_Business.Helper
{
    public static class HelperUser
    {
        public static UserInfo GetUser(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                var ipRemoteReq = httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

                var item = httpContextAccessor.HttpContext.Items;

                if (identity != null)
                {
                    var user = new UserInfo()
                    {
                        //CodigoDespacho = identity.FindFirst("xxxx").Value,
                        //Codigo = identity.FindFirst("xxx").Value,
                        //Name = identity.FindFirst("xx").Value,
                        //Email = identity.FindFirst(ClaimTypes.Email).Value,
                        //Role = identity.FindFirst(ClaimTypes.Role).Value,
                        //IpRemote = ipRemoteReq.ToString()

                    };
                    return user;
                }
            }
            catch (Exception)
            {

                throw new Exception("Error, No se obtuvo resultados.");
            }
            return null;
        }
        
    }

    
}



