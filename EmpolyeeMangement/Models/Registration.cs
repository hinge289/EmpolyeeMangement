using System.ComponentModel.DataAnnotations;

namespace EmpolyeeMangement.Models
{
    public class Registration
    {
        public int EmpolyeeId { get; set; }
        public int Designation { get; set; }

        [Required(ErrorMessage = "Please Enter  Name.")]
            [RegularExpression(@"^[A-Za-z-_ \\s]*$", ErrorMessage = "Use letters only please")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter DOB")]
       
            public DateOnly DOB { get; set; }
        public DateOnly HireDate { get; set; }

        [Required(ErrorMessage = "Please Enter Salary")]
            [RegularExpression(@"^[A-Za-z-_ \\s]*$", ErrorMessage = "Use letters only please")]
            public long Salary { get; set; }

            [Required(ErrorMessage = "Please Enter Mobile No.")]
            [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobile No.")]
            public string MobileNo { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }    
        public string IFSC { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }

        public string  Pan { get; set; }
        public string UAN { get; set; }
        public long AadharNo { get; set; }
        [Required(ErrorMessage = "Please select a file.")]

            public IFormFile File { get; set; }

            public string? UserName { get; set; }


            [Required(ErrorMessage = "Please enter an email.")]
            [RegularExpression(@"^(?![.-])[a-zA-Z0-9._%+-]+[a-zA-Z0-9]@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
          ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }
            public string? Password { get; set; }
         
            public string Role { get; set; }

        }
    }

