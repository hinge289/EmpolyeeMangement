using Microsoft.EntityFrameworkCore;

namespace EmpolyeeMangement.Models
{
    public class DBContexts : DbContext
    {
        public DBContexts(DbContextOptions option) : base(option)
        {

        }
        public DbSet<Empolyee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}



