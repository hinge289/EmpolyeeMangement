using Microsoft.EntityFrameworkCore;

namespace EmpolyeeMangement.Models
{
    public class DBContexts : DbContext
    {
        public DBContexts(DbContextOptions option) : base(option)
        {

        }
        public DbSet<Designation> Designations { get; set; }    
        public DbSet<Empolyee> Employees { get; set; }

        public DbSet<SalaryRecord> SalaryRecords { get; set; } // Real table
        public DbSet<SalaryResult> SalaryResults { get; set; } // SP result

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalaryResult>().HasNoKey(); // SP result mapping
        }

    }
}



