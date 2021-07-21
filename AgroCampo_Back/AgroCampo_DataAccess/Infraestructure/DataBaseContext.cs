using Microsoft.EntityFrameworkCore;
using ESDAVDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroCampo_Common.DTOs;
using AgroCampo_Domain.Domain;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESDAVDataAccess.Infraestructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() { }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        #region schemaDBO


        public DbSet<ExampleEntity> exampleEntities { get; set; }


        #endregion





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var schemaDBO = "dbo";



            base.OnModelCreating(modelBuilder);

            #region SchemaDBO
            modelBuilder.Entity<ExampleEntity>().ToTable("T_Example", schemaDBO);


            #endregion



        }
    }
}
