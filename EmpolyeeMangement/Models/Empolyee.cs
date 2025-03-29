using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace EmpolyeeMangement.Models
{
    public class Empolyee
    {
        [Key]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "Select Designation.")]
        public int Designation { get; set; }

        [Required(ErrorMessage = "Please Enter  Name.")]
            [RegularExpression(@"^[A-Za-z-_ \\s]*$", ErrorMessage = "Use letters only please")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter DOB")]
        
       
            public DateOnly DOB { get; set; }
        [Required(ErrorMessage = "Please Enter HireDate")]
        public DateOnly HireDate { get; set; }

        [Required(ErrorMessage = "Please Enter Salary")]
        
            public long Salary { get; set; }

            [Required(ErrorMessage = "Please Enter Mobile No.")]
            [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobile No.")]
            public string MobileNo { get; set; }
        [Required(ErrorMessage = "Enter Account Number")]
        public string AccountNo { get; set; }
        [Required(ErrorMessage = "Enter BankName")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "Enter IFSC Code")]
        public string IFSC { get; set; }
        [Required(ErrorMessage = "Enter Permanent Address")]
        public string PermanentAddress { get; set; }
        [Required(ErrorMessage = "Enter Current Address")]
        public string CurrentAddress { get; set; }

        [Required(ErrorMessage ="Enter Pan Number")]
        
        public string  Pan { get; set; }
        public string UAN { get; set; }
        [Required(ErrorMessage ="Enter Aadhar Number")]
            
        public long AadharNo { get; set; }
        [Required(ErrorMessage = "Please select a file.")]

        [NotMapped]
        public IFormFile File { get; set; }
        public string? FileName {  get; set; }

            public string? UserName { get; set; }


            [Required(ErrorMessage = "Please enter an email.")]
            [EmailAddress (ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }
     
            public string? Password { get; set; }
         
            public string? Role { get; set; }

        }
    }

