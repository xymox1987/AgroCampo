
using AgroCampo_Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgroCampo_Domain.Domain
{
   public class BasicasEntity
    {

       /*
        otros atributos de auditoria
        */
        public StateEnum State { get; set; }
    }
}
