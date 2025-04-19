using System.Data;

namespace EmpolyeeMangement.Models.Employee
{
    public interface IEmployee
    {
        SalaryRecord GetSalaryRecord(int month, int year, int EmployeeId);
        bool SendSalarySlipToEmpolyee(DataTable record);
        DataTable GetDataIntoDT(SalaryRecord record);
    }
}
