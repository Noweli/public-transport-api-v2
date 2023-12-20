using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Data.Models;

namespace PublicTransportApi.Tests.TestHelpers
{
    public class DbContextHelper
    {
        private ApplicationDbContext? _dbContext;
        
        public DbContextHelper()
        {
            PrepareDbContext();
        }

        public ApplicationDbContext GetDbContext()
        {
            var result = _dbContext;
            
            PrepareDbContext();
            
            return result!;
        }

        public DbContextHelper WithTestLineEntries()
        {
            if (_dbContext?.Lines.Any() ?? false)
            {
                return this;
            }
            
            _ = _dbContext!.Lines.Add(GenerateLine());

            _ = _dbContext.SaveChanges();

            return this;
        }

        public static Line GenerateLine(int id = 1, string identifier = "NL1", string name = "North Line 1")
        {
            return new Line
            {
                Id = 1,
                Identifier = "NL1",
                Name = "North Line 1"
            };
        }

        private void PrepareDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;
                
            _dbContext = new ApplicationDbContext(options);
        }
    }
}