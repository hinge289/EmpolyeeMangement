
using EmpolyeeMangement.Models.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace EmpolyeeMangement.Models.Report
{
    public class ImpReport:IReport
    {
        private DBContexts _context;

        public ImpReport(DBContexts context)
        {
            _context = context;
        }

        public ImpReport()
        {
            
        }
        public Empolyee CheckCredentials(Empolyee emp)
        {
            var list = _context.Employees.FirstOrDefault(x => x.UserName == emp.UserName);
            return list == null ? null : list;

        }
    }
}
