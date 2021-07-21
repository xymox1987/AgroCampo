using AgroCampo_Business.Services.Interface;
using AgroCampo_Domain.Domain;
using AgroCampo_Common.DTOs;
using ESDAVDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AgroCampo_Business.Helper;
using Microsoft.AspNetCore.Http;

namespace AgroCampo_Business.Services.Implementation
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _exampleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExampleService(IExampleRepository exampleRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._exampleRepository = exampleRepository;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<ExampleDTO> Create(ExampleDTO entity)
        {
            try
            {
                var usuario = HelperUser.GetUser(this._httpContextAccessor);

                
                var newEntity = new ExampleEntity()
                {
                   Descripcion=entity.Descripcion,
                   State=AgroCampo_Common.Enums.StateEnum.Enable
                };
                this._exampleRepository.Insert(newEntity, usuario.Codigo);
                entity.Id = newEntity.Id;

                return entity;
            }
            catch (Exception)
            {

                throw new Exception("Error, No se encontraron resultados.");
            }

        }

        public async Task<IList<ExampleDTO>> GetList()
        {

            try
            {
                var usuario = HelperUser.GetUser(_httpContextAccessor);

                var result = _exampleRepository
               .GetAll()
               .Select(x => new ExampleDTO()
               {
                   Id = x.Id,
                   Descripcion = x.Descripcion,
                   State = x.State

               })
               //.OrderBy(x => x.)
               .ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("Error, No se encontraron resultados.");
            }

        }
        public async Task<ExampleDTO> GetById(long id)
        {

            try
            {
                var usuario = HelperUser.GetUser(_httpContextAccessor);

                var result = _exampleRepository
               .FindBy(x=>x.Id==id)
               .Select(x => new ExampleDTO()
               {
                   Id = x.Id,
                   Descripcion = x.Descripcion,
                   State = x.State

               })               
               .FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Error, No se encontraron resultados.");
            }

        }



        public async Task<bool> Delete(long id)
        {
            try
            {
                var usuario = HelperUser.GetUser(this._httpContextAccessor);                
                this._exampleRepository.Delete(id, usuario.Codigo);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error, No se logro eliminar el registro");

            }

        }

        public async Task<string> Update(ExampleDTO entity)
        {

            var entityReg = this._exampleRepository.FindBy(x => x.Id == entity.Id).FirstOrDefault();
            var usuario = HelperUser.GetUser(this._httpContextAccessor);
            if (entityReg == null)
            {
                throw new Exception("No existe el registro.");
            }

            entityReg.Descripcion = entity.Descripcion;
            entityReg.State = entity.State;
           


            this._exampleRepository.Update(entityReg, usuario.Codigo);
            return "Se realizó la actualización correctamente";
        }

      
    }
}
