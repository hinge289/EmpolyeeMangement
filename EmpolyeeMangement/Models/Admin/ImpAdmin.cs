using AspNetCore.Reporting;
using ExcelDataReader;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Text;


namespace EmpolyeeMangement.Models.Admin
{
    public class ImpAdmin : IAdmin
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string Connection;
        private DBContexts _context;
        private IWebHostEnvironment _Environment;
        public ImpAdmin(IConfiguration configuration, DBContexts context, IWebHostEnvironment webHostEnvironment, IWebHostEnvironment environment)
        {
            Connection = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _Environment = environment;
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
            return _context.Employees.Where(x => x.Role == "Empolyee").ToList();
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
                            sqlBulkCopy.ColumnMappings.Add("PresentDay", "PresentDay");
                            sqlBulkCopy.ColumnMappings.Add("LOP", "LOP");

                            sqlBulkCopy.ColumnMappings.Add("OverTime", "OverTime");

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

        private void SaveSalaryResult(List<SalaryResult> salaryResults)
        {
            List<SalaryRecord> salaryrecord = salaryResults.Select(sr => new SalaryRecord
            {
                EmployeeId = sr.EmployeeId,
                Name=sr.Name,
                BankName = sr.BankName,
                AccountNo = sr.AccountNo,
                HireDate = sr.HireDate,
                Email = sr.Email,
                PAN = sr.PAN,
                MobileNo = sr.MobileNo,
                UAN = sr.UAN,
                Salary = sr.Salary,
                PresentDay = sr.PresentDay,
                OverTime = sr.OverTime,
                LOP = sr.LOP,
                Month = sr.Month,
                Year = sr.Year,
                ReamaningLeave = sr.ReamaningLeave,
                DesignationName = sr.DesignationName,
                MonthlySalary = sr.MonthlySalary,
                ActualLOP = sr.ActualLOP,
                CalculateSalary = sr.CalculateSalary,
                BasicSalary = (int)sr.BasicSalary,
                ActualGetSalary= (int)sr.ActualGetSalary,
                TotalDeduction= (int)sr.TotalDeduction,
                HRA = (int)sr.HRA,
                STAT = (int)sr.STAT,
                ProfessionalAllowance = (int)sr.ProfessionalAllowance,
                CCA = (int)sr.CCA,
                SpecialAllowance = (int)sr.SpecialAllowance,
                Total = (int)sr.Total,
                PF = (int)sr.PF,
                ProfessionalTax = sr.ProfessionalTax
            }).ToList();
            _context.SalaryRecords.AddRange(salaryrecord);
            _context.SaveChanges();
        }

        public List<SalaryResult> GetSalaryResults(int month, int year)
        {
            var spResult = _context.SalaryResults.FromSqlRaw("EXEC Sp_CalculateSalary @month={0},@year={1}", month, year).ToList();
            SaveSalaryResult(spResult);
            return spResult;
        }

        private DataTable GetDataIntoDT(SalaryResult record)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("EmployeeId", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("BankName", typeof(string));
            dt.Columns.Add("AccountNo", typeof(string));
            dt.Columns.Add("HireDate", typeof(DateTime));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("PAN", typeof(string));
            dt.Columns.Add("MobileNo", typeof(string));
            dt.Columns.Add("UAN", typeof(string));
            dt.Columns.Add("Salary", typeof(decimal));
            dt.Columns.Add("PresentDay", typeof(int));
            dt.Columns.Add("OverTime", typeof(int));
            dt.Columns.Add("LOP", typeof(int));
            dt.Columns.Add("Month", typeof(int));
            dt.Columns.Add("Year", typeof(int));
            dt.Columns.Add("ReamaningLeave", typeof(int));
            dt.Columns.Add("DesignationName", typeof(string));
            dt.Columns.Add("MonthlySalary", typeof(decimal));
            dt.Columns.Add("ActualLOP", typeof(decimal));
            dt.Columns.Add("CalculateSalary", typeof(decimal));
            dt.Columns.Add("BasicSalary", typeof(int));
            dt.Columns.Add("ActualGetSalary", typeof(int));
            dt.Columns.Add("TotalDeduction", typeof(int));
            dt.Columns.Add("HRA", typeof(int));
            dt.Columns.Add("STAT", typeof(int));
            dt.Columns.Add("ProfessionalAllowance", typeof(int));
            dt.Columns.Add("CCA", typeof(int));
            dt.Columns.Add("SpecialAllowance", typeof(int));
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("PF", typeof(int));
            dt.Columns.Add("ProfessionalTax", typeof(int));

            dt.Rows.Add(
                record.EmployeeId,
                record.Name,
                record.BankName,
                record.AccountNo,
                record.HireDate,
                record.Email,
                record.PAN,
                record.MobileNo,
                record.UAN,
                record.Salary,
                record.PresentDay,
                record.OverTime,
                record.LOP,
                record.Month,
                record.Year,
                record.ReamaningLeave,
                record.DesignationName,
                record.MonthlySalary,
                record.ActualLOP,
                record.CalculateSalary,
               (int) record.BasicSalary,
                (int)record.ActualGetSalary,
                (int)record.TotalDeduction,
               (int)record.HRA,
               (int)record.STAT,
               (int)record.ProfessionalAllowance,
                (int)record.CCA,
                (int)record.SpecialAllowance,
                (int)record.Total,
                (int)record.PF,
                record.ProfessionalTax
            );

            return dt;

        }
        private void MailSenderr(byte[] pdfBytes, string filename,string Email)
        {

            // Sender email credentials
            string senderEmail = "tejashinge754@gmail.com";
            string senderPassword = "oezfvaerysltwdjg";

            // Receiver email
            string receiverEmail = Email;



            // Email subject and body
            string subject = "GTH Salary  Slip";
            string body = $"Here Your Salary Slip";

            try
            {
                // Create the MailMessage object
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = body;

                using (MemoryStream ms = new MemoryStream(pdfBytes))
                {
                    //pass MemoryStream , filename and type 
                    mail.Attachments.Add(new Attachment(ms, filename, "application/pdf"));
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    
                }
                // Send the email

            }
            catch (Exception ex)
            {
                throw;
              
            }

        }
        public bool SendSalarySlipToEmpolyee(List<SalaryResult> salaryResults)
        {
            
            foreach (var item in salaryResults)
            {
               
                    var dt = GetDataIntoDT(item);   
                    string mail=null,name=null;

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        mail = Convert.ToString(row["Email"]);
                    name = Convert.ToString(row["Name"]);
                    }
              
                string mimietype = "";
                int extention = 1;
                var path = $"{_Environment.WebRootPath}\\Reports\\SalarySlip.rdlc";
                LocalReport rpt = new LocalReport(path);
                rpt.AddDataSource("DataSet1", dt);
                var result = rpt.Execute(RenderType.Pdf, extention, null, mimietype);
                //pass report path  and filename 

                 MailSenderr(result.MainStream, $"{name} SalarySlip",mail);
            

            }
            return true;
        }
    }
}

