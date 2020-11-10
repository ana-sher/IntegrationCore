using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace IntegrationCore.Data
{
    public class DbInitializer
    {
        public static void Init(IntegratorContext context)
        {
            if (!context.TypeDefinition.Any())
            {
                context.TypeDefinition.AddRange(new TypeDefinition()
                {
                    Name = "string",
                    IsBasic = true
                }, new TypeDefinition()
                {
                    Name = "number",
                    IsBasic = true
                }, new TypeDefinition()
                {
                    Name = "boolean",
                    IsBasic = true
                });
            }

            context.SaveChanges();
        }
    }
}
