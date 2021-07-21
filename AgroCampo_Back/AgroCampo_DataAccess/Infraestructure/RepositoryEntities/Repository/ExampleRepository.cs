

using AgroCampo_Common.DTOs;
using ESDAVDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using AgroCampo_Domain.Domain;

namespace ESDAVDataAccess.Infraestructure.RepositoryEntities.Repository
{
    public class ExampleRepository : GenericRepository<ExampleEntity, DataBaseContext>, IExampleRepository
    {
        public ExampleRepository(DataBaseContext context) : base(context)
        {
        }

       


    }
}

