using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpolyeeMangement.Models
{
    public class Designation
    {
        [Key]

        public int ?DesignationId { get; set; }
        public string DesignationName { get; set; }

    }

   
}
