namespace EmpolyeeMangement.Models
{
    public class SalaryResult
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public DateTime HireDate { get; set; }
        public string Email { get; set; }
        public string PAN { get; set; }
        public string MobileNo { get; set; }
        public string UAN { get; set; }
        public long Salary { get; set; }
        public int PresentDay { get; set; }
        public int OverTime { get; set; }
        public int LOP { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int? ReamaningLeave { get; set; }
        public string DesignationName { get; set; }
        public long  MonthlySalary { get; set; }
        public int ActualLOP { get; set; }
        public long  CalculateSalary { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal ActualGetSalary { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal HRA { get; set; }
        public decimal STAT { get; set; }
        public decimal ProfessionalAllowance { get; set; }
        public decimal CCA { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal Total { get; set; }
        public decimal PF { get; set; }
        public int ProfessionalTax { get; set; }
    }



}
