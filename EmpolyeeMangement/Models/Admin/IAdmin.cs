using System.Data;

namespace EmpolyeeMangement.Models.Admin
{
    public interface IAdmin
    {
        List<Designation> GetDesignation();
        bool CheckEmpolyeeExist(Empolyee emp);
        bool AddEmpolyee(Empolyee emp);
        List<Empolyee> GetEmpolyeeList();
       // List<Empolyee> GetListForAttendanceUpload();
     
        DataTable ReadCsvFile(IFormFile file);
        DataTable ReadExcelFile(IFormFile file);
        bool UplodeScanDocument(DataTable dt);
        bool SendSalarySlipToEmpolyee(List<SalaryResult> salaryResults);
        List<SalaryResult> GetSalaryResults(int month, int year);



    }
}
