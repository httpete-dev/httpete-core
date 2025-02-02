using HttPete.Infrastructure.Persistence.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.IntegrationTests.Utils
{
    public class HttPeteDbContextFixture
    {
        public HttPeteDbContext Context { get; private set; }

        public HttPeteDbContextFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<HttPeteDbContext>()
                .UseInMemoryDatabase(databaseName: "HttPeteTestDb")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            Context = new HttPeteDbContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
