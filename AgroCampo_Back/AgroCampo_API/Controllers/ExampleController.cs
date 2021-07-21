using AgroCampo_Business.Helper;
using AgroCampo_Business.Services.Interface;
using AgroCampo_Common.DTOs;
using ESDAVDomain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESDAVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*Descomentar la linea de Authorize una vez se configure correctamente el acceso por el SSO*/
    //[Authorize]
    public class ExampleController : ControllerBase
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IExampleService _exampleService;
        IHttpContextAccessor _httpContextAccessor;

        public ExampleController(IExampleService exampleService, IHttpContextAccessor httpContextAccessor)
        {
            this._exampleService = exampleService;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {

                var usuario = HelperUser.GetUser(_httpContextAccessor);

                var result = await this._exampleService.GetList();
                var response = new ServiceResponseDTO<IList<ExampleDTO>>()
                {
                    Data = result,
                    Message = "ok",
                    Success = true,
                    CountRecords = result.Count
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponseDTO<string>()
                {
                    Data = null,
                    Message = "Error",
                    Success = false
                };
                return BadRequest(response);

            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var usuario = HelperUser.GetUser(_httpContextAccessor);
                var result = await this._exampleService.GetById(id);
                var response = new ServiceResponseDTO<ExampleDTO>()
                {
                    Data = result,
                    Message = "ok",
                    Success = true
                    
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponseDTO<string>()
                {
                    Data = null,
                    Message = "Error cargando lista de ... completados.",
                    Success = false
                };
                return BadRequest(response);

            }
        }

       

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReminderByNumReminder(long id)
        {
            try
            {
                var result = await this._exampleService.Delete(id);

                var response = new ServiceResponseDTO<bool>()
                {
                    Data = result,
                    Message = "ok",
                    Success = result
                    //CountRecords = (string)true;
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponseDTO<string>()
                {
                    Data = null,
                    Message = "Error->" + ex.Message,
                    Success = false
                };
                return BadRequest(response);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ExampleDTO entity)
        {
            try
            {
                var respuesta = await this._exampleService.Create(entity);

                var response = new ServiceResponseDTO<ExampleDTO>()
                {
                    Data = respuesta,
                    Message = "ok",
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponseDTO<string>()
                {
                    Data = null,
                    Message = "Error->" + ex.Message,
                    Success = false
                };
                return BadRequest(response);

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReminder([FromBody] ExampleDTO entity)
        {
            try
            {

                var result = await this._exampleService.Update(entity);

                var response = new ServiceResponseDTO<string>()
                {
                    Data = result,
                    Message = "ok",
                    Success = true,
                    CountRecords = 1
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponseDTO<string>()
                {
                    Data = null,
                    Message = "Error->" + ex.Message,
                    Success = false
                };
                return BadRequest(response);

            }
        }

      
    }
}

