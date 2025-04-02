using System.Data;
using System.IO;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

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
            return _context.Employees.Where(x=>x.Role=="Empolyee").ToList();
        }

        public DataTable ReadCsvFile(IFormFile file)
        {

            DataTable dataTable = new DataTable();

            using (var stream = file.OpenReadStream())
            using (var csvReader = new TextFieldParser(stream))
            {
                csvReader.SetDelimiters(new[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                var colFields = csvReader.ReadFields();
                foreach (var column in colFields)
                {
                    dataTable.Columns.Add(column);
                }


                while (!csvReader.EndOfData)
                {
                    var fields = csvReader.ReadFields();
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;

        }

        public DataTable ReadExcelFile(IFormFile file)
        {
            DataTable dataTable = new DataTable();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = file.OpenReadStream())
            {
                IExcelDataReader reader = null;

                if (Path.GetExtension(file.FileName).ToLower() == ".xls")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                });

                dataTable = result.Tables[0];
                reader.Close();
            }

            return dataTable;
        }

        public bool UplodeScanDocument(DataTable dataTable)
        {
            var connection = _context.Database.GetDbConnection().ConnectionString;

            try
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    using (SqlConnection _sql = new SqlConnection(connection))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_sql))
                        {

                            sqlBulkCopy.DestinationTableName = "dbo.EmployeeAttendance";
                            sqlBulkCopy.ColumnMappings.Add("Month", "Month");
                            sqlBulkCopy.ColumnMappings.Add("Year", "Year");
                            sqlBulkCopy.ColumnMappings.Add("EmployeeId", "EmployeeId");
                            sqlBulkCopy.ColumnMappings.Add("Days", "Days");
                            sqlBulkCopy.ColumnMappings.Add("LOP", "LOP");
                            sqlBulkCopy.ColumnMappings.Add("RemainingLeave", "RemainingLeave");

                            _sql.Open();
                            sqlBulkCopy.WriteToServer(dataTable);
                            _sql.Close();
                        }
                        return true;
                    }


                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

            return false;

        }
    }
}

