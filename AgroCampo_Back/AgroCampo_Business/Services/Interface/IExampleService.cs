using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgroCampo_Common.DTOs;

namespace AgroCampo_Business.Services.Interface
{
    public interface IExampleService
    {
        Task<ExampleDTO> Create(ExampleDTO entity);
        Task<IList<ExampleDTO>> GetList();
        Task<ExampleDTO> GetById(long id);
        Task<Boolean> Delete(long id);
        Task<string> Update(ExampleDTO entity);
        
        
    }
}
