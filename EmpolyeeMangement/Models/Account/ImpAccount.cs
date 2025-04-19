

namespace EmpolyeeMangement.Models.Account
{
    public class ImpAccount : IAccount
    {
        private DBContexts _context;

        public ImpAccount(DBContexts context)
        {
            _context = context;
        }

        public ImpAccount()
        {
            
        }
        public Empolyee CheckCredentials(Empolyee emp)
        {
            var list = _context.Employees.FirstOrDefault(x => x.UserName == emp.UserName);
            return list == null ? null : list;

        }

        public void check()
        {
            throw new NotImplementedException();
        }
    }
}
