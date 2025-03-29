using System.IO;
using System.Reflection.PortableExecutable;

namespace EmpolyeeMangement.Models.Admin
{
    public class ImpAdmin : IAdmin
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string Connection;
        private DBContexts _context;
        public ImpAdmin(IConfiguration configuration, DBContexts context, IWebHostEnvironment webHostEnvironment)
        {
            Connection = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool AddEmpolyee(Empolyee emp)
        {
            var file = emp.File;
            var filename = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", filename);

            using (var fileStrem = new FileStream(filepath, FileMode.Create))
            {
                emp.File.CopyTo(fileStrem);
            }
             emp.FileName = filename;
            emp.Role = "Empolyee";
            emp.UserName = emp.Email;
            emp.Password = Genratepassword();
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return true;
        }

        private string Genratepassword()
        {
            Random _random = new Random();
            string character = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            char[] password = new char[8];
            for (int i = 0; i < 8; i++)
            {
                password[i] = character[_random.Next(character.Length)];
            }
            return new string(password);

        }

        public Empolyee checkCreaditioal(Empolyee emp)
        {

            var list = _context.Employees.FirstOrDefault(x => x.UserName == emp.UserName);
            return list == null ? null : list;
        }

        public bool CheckEmpolyeeExist(Empolyee emp)
        {
            var empolyee = _context.Employees.FirstOrDefault(x => x.EmployeeId == emp.EmployeeId);
            if (empolyee != null)
            {
                return true;
            }
            return false;
        }

        public List<Designation> GetDesignation()
        {
            //DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(Connection);
            //SqlDataAdapter ad = new SqlDataAdapter("Select * from Designations", Connection);
            //ad.Fill(dt);
            //var list = new List<Designation>();

            //foreach (DataRow Row in dt.Rows)
            //{

            //    list.Add(new Designation
            //    {
            //        DesiganationId = Convert.ToInt32(Row["DesignationId"]),
            //        DesignationName = Convert.ToString(Row["DesignationName"])
            //    });
            //}
            var list = _context.Designations.ToList();
            return list;
        }

        public List<Empolyee> GetEmpolyeeList()
        {
            return _context.Employees.ToList();
        }
    }
}

