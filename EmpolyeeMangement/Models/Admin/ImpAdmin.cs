
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmpolyeeMangement.Models.Admin
{
    public class ImpAdmin : IAdmin
    {
        private string Connection;
        public ImpAdmin(IConfiguration configuration)
        {
            Connection = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Designation> GetDesignation()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            SqlDataAdapter ad = new SqlDataAdapter("Select * from Desigantion", Connection);
            ad.Fill(dt);
            var list = new List<Designation>();

            foreach (DataRow Row in dt.Rows)
            {

                list.Add(new Designation
                {
                    DesiganationId = Convert.ToInt32(Row["DesignationId"]),
                    DesignationName = Convert.ToString(Row["DesignationName"])
                });
            }
            return list;
        }

    }
}

