using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    //pmc bozulursa ,visual studioda çalışmazken, shellden kullanmak, migration yapmak istersen vs gerekli
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<APIDbContext>
    {
        public APIDbContext CreateDbContext(string[] args)
        {         
            DbContextOptionsBuilder<APIDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<APIDbContext>();
            dbContextOptionsBuilder.UseSqlServer(Configuration.connectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
