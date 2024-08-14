using EmployeeManagement.Common;
using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class EmployeeEntity :  BaseDataObject
    {


            [Required(ErrorMessage = "Employee ID is required")]
            public string empID { get; set; }

            [Required(ErrorMessage = "Phone number is required")]
            [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
            public string phone { get; set; }

            [Required(ErrorMessage = "First name is required")]
            public string firstName { get; set; }

            [Required(ErrorMessage = "Last name is required")]
            public string lastName { get; set; }

            [Required(ErrorMessage = "Designation is required")]
            public string designation { get; set; }

            [Required(ErrorMessage = "Experience is required")]
            public int experience { get; set; }

            [Required(ErrorMessage = "Date of Birth is required")]
            public DateTime dateOfBirth { get; set; }

            [Required(ErrorMessage = "Date of Joining is required")]
            public DateTime dateOfJoining { get; set; }

            [Required(ErrorMessage = "Address is required")]
            public string address { get; set; }
            [Required]
            [RegularExpression(@"\d{6}", ErrorMessage = "Invalid Pin Code format")]
            public string pinCode { get; set; }

            [Required(ErrorMessage = "City is required")]
            public string city { get; set; }

            [Required(ErrorMessage = "State is required")]
            public string state { get; set; }

            [Required(ErrorMessage = "Pan Number is required")]
            public string panNumber { get; set; }

            [Required(ErrorMessage = "Aadhaar Card is required")]
            public string aadhaarCard { get; set; }

            [Required(ErrorMessage = "Contact Site is required")]
            public string contactSite { get; set; }

            [Required(ErrorMessage = "Bank Name is required")]
            public string bankName { get; set; }

            [Required(ErrorMessage = "Bank Address is required")]
            public string bankAddress { get; set; }

            [Required(ErrorMessage = "Account Number is required")]
            public string accountNumber { get; set; }

            [Required(ErrorMessage = "IFSC is required")]
            public string iFSC { get; set; }

            [Required(ErrorMessage = "Salary is required")]
            public decimal salary { get; set; }

            [Required(ErrorMessage = "PF Number is required")]
            public string pFNumber { get; set; }

            [Required(ErrorMessage = "Working Hours are required")]
            public string workingHours { get; set; }

        public string familyContactNumber { get; set; }
        public string nature { get; set; }
        public string esiNumber { get; set; }

        public string vanNumber {  get; set; }





            //public bool checkmeout { get; set; }


    }

  
}
