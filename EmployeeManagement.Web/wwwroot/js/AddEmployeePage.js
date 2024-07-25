// open comformation models
document.getElementById('EmployeeAllDetails').addEventListener('submit', function (event) {
    event.preventDefault();

    $('#confirmationModal').modal('show');
});


/// data send to controller
document.getElementById('confirmSubmit').addEventListener('click', function () {

    $('#confirmationModal').modal('hide');

    
        // Serialize the form data
    let formData = new FormData(document.getElementById('EmployeeAllDetails'));
    if (validateForm()) {
        let data = {};
        formData.forEach((value, key) => {
            data[key] = value;

        });
        // Send the data using fetch
        fetch('/AddEmployee/CreateEmployeeData', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest'
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    oneStepBackFun();
                    alert('Employee added successfully!');


                    // Show success message in HTML
                    const successMessage = document.createElement('div');
                    successMessage.innerHTML = '<p>Employee added successfully!</p>';
                    document.getElementById('successMessageContainer').appendChild(successMessage);

                } else {
                    alert('Error adding employee: ' + data.message);
                }
            })
            .catch(error => {
                $('#confirmationModal').modal('hide');
                console.error('Error:', error);
                alert('Error adding employee: ' + error.message);
            });
    } else {
       
    }


});
document.getElementById('CancelSubmit').addEventListener('click', function () {
   // window.location.href = '/AddEmployee/AddEmployeePage';  // Replace with the actual URL you want to redirect to
    $('#confirmationModal').modal('hide');
});
document.getElementById('CloseConfirmationModal').addEventListener('click', function () {
    // window.location.href = '/AddEmployee/AddEmployeePage';  // Replace with the actual URL you want to redirect to
    $('#confirmationModal').modal('hide');
});
    document.getElementById('cancelButtonBackPage').addEventListener('click', function () {
        oneStepBackFun();
    });

function oneStepBackFun() {
    window.location.href = '/AddEmployee/AddEmployeeDetails';
}

// validation of all employee details filed

    function validateForm() {
        let valid = true;

        if (!validateEmpID()) valid = false;
        if (!validatePhone()) valid = false;
        if (!validateFName()) valid = false;
        if (!validateLName()) valid = false;
        if (!validateDesignation()) valid = false;
        if (!validateExperience()) valid = false;
        if (!validateDOB()) valid = false;
        if (!validateDOJ()) valid = false;
        if (!validateAddress()) valid = false;
        if (!validatePinCode()) valid = false;
        if (!validateCity()) valid = false;
        if (!validateState()) valid = false;
        if (!validatePanNumber()) valid = false;
        if (!validateAadhaarCard()) valid = false;
        if (!validateContactSite()) valid = false;
        if (!validateBankName()) valid = false;
        if (!validateBankAddress()) valid = false;
        if (!validateAccountNumber()) valid = false;
        if (!validateIFSC()) valid = false;
        if (!validateSalary()) valid = false;
        if (!validatePFNumber()) valid = false;
        if (!validateWorkingHours()) valid = false;
        if (!validateCheckMeOut()) valid = false;

        return valid;
    }

    function validateEmpID() {
        var empID = document.getElementById("inputEmpID").value;
        var empIDError = document.getElementById("empID-error");
        if (empID === "") {
            empIDError.textContent = "Employee ID is required";
            return false;
        } else {
            empIDError.textContent = "";
            return true;
        }
    }

    function validatePhone() {
        var phone = document.getElementById("inputPhone").value;
        var phoneError = document.getElementById("phone-error");
        var phonePattern = /^[0-9]{10}$/;
        if (phone === "") {
            phoneError.textContent = "Phone number is required";
            return false;
        } else if (!phonePattern.test(phone)) {
            phoneError.textContent = "Phone number must be 10 digits";
            return false;
        } else {
            phoneError.textContent = "";
            return true;
        }
    }

    function validateFName() {
        var firstName = document.getElementById("inputFirstName").value;
        var fnameError = document.getElementById("fname-error");
        if (firstName === "") {
            fnameError.textContent = "First name is required";
            return false;
        } else {
            fnameError.textContent = "";
            return true;
        }
    }

    function validateLName() {
        var lastName = document.getElementById("inputLastName").value;
        var lnameError = document.getElementById("lname-error");
        if (lastName === "") {
            lnameError.textContent = "Last name is required";
            return false;
        } else {
            lnameError.textContent = "";
            return true;
        }
    }

    function validateDesignation() {
        var designation = document.getElementById("inputDesignation").value;
        var designationError = document.getElementById("designation-error");
        if (designation === "") {
            designationError.textContent = "Designation is required";
            return false;
        } else {
            designationError.textContent = "";
            return true;
        }
    }

    function validateExperience() {
        var experience = document.getElementById("inputExperience").value;
        var experienceError = document.getElementById("experience-error");
        if (experience === "") {
            experienceError.textContent = "Experience is required";
            return false;
        } else {
            experienceError.textContent = "";
            return true;
        }
    }

    function validateDOB() {
        var dob = document.getElementById("inputDateOfBirth").value;
        var dobError = document.getElementById("dob-error");
        if (dob === "") {
            dobError.textContent = "Date of Birth is required";
            return false;
        } else {
            dobError.textContent = "";
            return true;
        }
    }

    function validateDOJ() {
        var doj = document.getElementById("inputDateOfJoin").value;
        var dojError = document.getElementById("doj-error");
        if (doj === "") {
            dojError.textContent = "Date of Joining is required";
            return false;
        } else {
            dojError.textContent = "";
            return true;
        }
    }

    function validateAddress() {
        var address = document.getElementById("inputAddress").value;
        var addressError = document.getElementById("address-error");
        if (address === "") {
            addressError.textContent = "Address is required";
            return false;
        } else {
            addressError.textContent = "";
            return true;
        }
    }
    function validatePinCode() {
        var pincode = document.getElementById("inputPinCode").value;
        var pinCodeError = document.getElementById("pinCode-error");
        var pinCodePattern = /^[0-9]{6}$/;
        if (pincode === "") {
            pinCodeError.textContent = "Phone number is required";
            return false;
        } else if (!pinCodePattern.test(pincode)) {
            pinCodeError.textContent = "Pin Code number must be 6 digits";
            return false;
        } else {
            pinCodeError.textContent = "";
            return true;
        }
    }

    function validateCity() {
        var city = document.getElementById("inputCity").value;
        var cityError = document.getElementById("city-error");
        if (city === "") {
            cityError.textContent = "City is required";
            return false;
        } else {
            cityError.textContent = "";
            return true;
        }
    }

    function validateState() {
        var state = document.getElementById("inputState").value;
        var stateError = document.getElementById("state-error");
        if (state === "" || state === "Choose...") {
            stateError.textContent = "State is required";
            return false;
        } else {
            stateError.textContent = "";
            return true;
        }
    }

    function validatePanNumber() {
        var panNumber = document.getElementById("inputPanNumber").value;
        var panNumberError = document.getElementById("panNumber-error");
        if (panNumber === "") {
            panNumberError.textContent = "Pan Number is required";
            return false;
        } else {
            panNumberError.textContent = "";
            return true;
        }
    }

    function validateAadhaarCard() {
        var aadhaarCard = document.getElementById("inputAadhaarCard").value;
        var aadhaarCardError = document.getElementById("aadhaarCard-error");
        if (aadhaarCard === "") {
            aadhaarCardError.textContent = "Aadhaar Card is required";
            return false;
        } else {
            aadhaarCardError.textContent = "";
            return true;
        }
    }

    function validateContactSite() {
        var contactSite = document.getElementById("inputContactSite").value;
        var contactSiteError = document.getElementById("contactSite-error");
        if (contactSite === "") {
            contactSiteError.textContent = "Contact Site is required";
            return false;
        } else {
            contactSiteError.textContent = "";
            return true;
        }
    }

    function validateBankName() {
        var bankName = document.getElementById("inputBankName").value;
        var bankNameError = document.getElementById("bankName-error");
        if (bankName === "") {
            bankNameError.textContent = "Bank Name is required";
            return false;
        } else {
            bankNameError.textContent = "";
            return true;
        }
    }

    function validateBankAddress() {
        var bankAddress = document.getElementById("inputBankAddress").value;
        var bankAddressError = document.getElementById("bankAddress-error");
        if (bankAddress === "") {
            bankAddressError.textContent = "Bank Address is required";
            return false;
        } else {
            bankAddressError.textContent = "";
            return true;
        }
    }

    function validateAccountNumber() {
        var accountNumber = document.getElementById("inputAccountNumber").value;
        var accountNumberError = document.getElementById("accountNumber-error");
        if (accountNumber === "") {
            accountNumberError.textContent = "Account Number is required";
            return false;
        } else {
            accountNumberError.textContent = "";
            return true;
        }
    }

    function validateIFSC() {
        var ifsc = document.getElementById("inputIFSC").value;
        var ifscError = document.getElementById("ifsc-error");
        if (ifsc === "") {
            ifscError.textContent = "IFSC is required";
            return false;
        } else {
            ifscError.textContent = "";
            return true;
        }
    }

    function validateSalary() {
        var salary = document.getElementById("inputSalary").value;
        var salaryError = document.getElementById("salary-error");
        if (salary === "") {
            salaryError.textContent = "Salary is required";
            return false;
        } else {
            salaryError.textContent = "";
            return true;
        }
    }

    function validatePFNumber() {
        var pfNumber = document.getElementById("inputPFNumber").value;
        var pfNumberError = document.getElementById("pfNumber-error");
        if (pfNumber === "") {
            pfNumberError.textContent = "PF Number is required";
            return false;
        } else {
            pfNumberError.textContent = "";
            return true;
        }
    }


    function validateWorkingHours() {
        var workingHours = document.getElementById("inputWorkingHours").value.trim();
        var workingHoursError = document.getElementById("workingHours-error");

        // Regular expression to match HH:mm format
        var timePattern = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;

        if (workingHours === "") {
            workingHoursError.textContent = "Time is required";
            return false;
        } else if (!timePattern.test(workingHours)) {
            workingHoursError.textContent = "Invalid time format. Use HH:mm";
            return false;
        } else {
            workingHoursError.textContent = "";
            return true;
        }
    }


    function validateCheckMeOut() {
        var checkMeOut = document.getElementById("gridCheck").checked;
        var checkMeOutError = document.getElementById("checkMeOut-error");
        if (!checkMeOut) {
            checkMeOutError.textContent = "You must agree to check me out";
            return false;
        } else {
            checkMeOutError.textContent = "";
            return true;
        }
    }

/// <summary>
///  State dropdown 
/// </summary>
/// <param name="model"></param>
/// <returns></returns>
    // Sample list of states (replace with your actual list)
    const states = [
        "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh",
        "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jharkhand",
        "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur",
        "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab",
        "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura",
        "Uttar Pradesh", "Uttarakhand", "West Bengal"
    ];

    // Function to populate the select element
    function populateStates() {
        const select = document.getElementById("inputState");

        // Clear existing options
        select.innerHTML = '';

        // Add default option
        const defaultOption = document.createElement("option");
        defaultOption.text = "Choose...";
        select.appendChild(defaultOption);

        // Add state options
        states.forEach(state => {
            const option = document.createElement("option");
            option.text = state;
            select.appendChild(option);
        });
    }

    // Call the function to populate the states when needed
populateStates();



