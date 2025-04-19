using AspNetCore.Reporting;
using System.Data;
using System.Net.Mail;
using System.Net;


namespace EmpolyeeMangement.Models.Employee
{
    public class ImpEmployee : IEmployee
    {
        private DBContexts _contexts;
        private IWebHostEnvironment _Environment;

        public ImpEmployee(DBContexts contexts, IWebHostEnvironment environment)
        {
            _contexts = contexts;
            _Environment = environment;
        }

     

        public SalaryRecord GetSalaryRecord(int month, int year, int EmployeeId)
        {
            
            var record = _contexts.SalaryRecords.FirstOrDefault(x => x.Month == month && x.Year == year && x.EmployeeId == EmployeeId);
            return record != null ? record : null;
        }
        public DataTable GetDataIntoDT(SalaryRecord record)
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
               (int)record.BasicSalary,
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

        private void MailSenderr(byte[] pdfBytes, string filename, string Email)
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
        public bool SendSalarySlipToEmpolyee(DataTable record)
        {

            string mail = null, name = null;

            if (record.Rows.Count > 0)
            {
                DataRow row = record.Rows[0];
                mail = Convert.ToString(row["Email"]);
                name = Convert.ToString(row["Name"]);
            }

            string mimietype = "";
            int extention = 1;
            var path = $"{_Environment.WebRootPath}\\Reports\\SalarySlip.rdlc";
            LocalReport rpt = new LocalReport(path);
            rpt.AddDataSource("DataSet1", record);
            var result = rpt.Execute(RenderType.Pdf, extention, null, mimietype);
            //pass report path  and filename 

            MailSenderr(result.MainStream, $"{name} SalarySlip", mail);
            return true;
        }


    }
}
