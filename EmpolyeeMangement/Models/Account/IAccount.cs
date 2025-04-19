namespace EmpolyeeMangement.Models.Account
{
    public interface IAccount
    {
        Empolyee CheckCredentials(Empolyee emp);

        void check();
    }
}
