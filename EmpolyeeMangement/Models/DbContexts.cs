using Microsoft.EntityFrameworkCore;

namespace EmpolyeeMangement.Models
{
    public class DBContexts:DbContext
    {
        public DBContexts(DbContextOptions option):base(option)
        {

        }
    }
}
