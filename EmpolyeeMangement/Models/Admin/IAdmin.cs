namespace EmpolyeeMangement.Models.Admin
{
    public interface IAdmin
    {
        List<Designation> GetDesignation();
        bool CheckEmpolyeeExist(Empolyee emp);
        bool AddEmpolyee(Empolyee emp);
        List<Empolyee> GetEmpolyeeList();

        Empolyee checkCreaditioal(Empolyee emp);



    }
}
