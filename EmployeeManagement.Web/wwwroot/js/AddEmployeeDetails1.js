$(document).ready(function () {
    GetAllEmployeeDetailsInTableFormat();
});

function GetAllEmployeeDetailsInTableFormat() {
    $.ajax({
        url: '/AddEmployee/GetEmployeeDetailsInTableForm',
        method: 'GET',
        success: function (data) {
            console.log("Data received:", data); // Debug: log the received data

            var tableBody = $('.search-table tbody');
            tableBody.empty(); // Clear any existing rows

            if (Array.isArray(data)) {
                data.forEach(function (employee) {
                    var name = employee.firstName + " " + employee.lastName;
                    var row = `<tr class="table-default search-items">
                                <td>${employee.empID}</td>
                                <td>${name}</td>
                                <td>${employee.phoneNo}</td>
                                <td>${employee.salary}</td>
                                <td>${employee.designation}</td>
                                <td>
                                    <div class="dropdown">
                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown"><i class="mdi mdi-dots-vertical"></i></button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item EmployeeDTView" onclick="GetEmployeeAllInformationClickViewButton('${employee.empID}', event)" href="#"><i class="far fa-eye"></i> View</a>
                                            <a class="dropdown-item deleteEmployee" onclick="deleteEmployeeInformation('${employee.empID}', event)" href="#"><i class="mdi mdi-trash-can-outline me-1"></i> Delete</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>`;
                    tableBody.append(row);
                });
            } else {
                console.error("Expected data to be an array but got:", data);
            }
        },
        error: function (error) {
            console.error('Error fetching employee data', error);
        }
    });
}

// View Employee Information click on view button.
function GetEmployeeAllInformationClickViewButton(empId, event) {
    event.preventDefault(); // Prevent the default link behavior
    // Debugging purpose to ensure function is called

    $.ajax({
        url: '/AddEmployee/GetEmployeeDetailsClickOnEditButton', // Your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ EmpID: empId }),
        success: function (response) {
           
            if (response && response.message) {
           
                OpenModels(response);
            } else {
               error('Unexpected response format.');
            }
        },
        error: function (error) {
            console.error('Error:', error);
            console.error('Error: ' + (error.responseText || 'Unknown error'));
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const rowsPerPage = 7;
    let currentPage = 1;

    const tableBody = document.querySelector("tbody");
    const originalRows = Array.from(tableBody.querySelectorAll("tr"));
    let filteredRows = [...originalRows];
    let totalPages = Math.ceil(filteredRows.length / rowsPerPage);

    function displayRows() {
        tableBody.innerHTML = "";
        const start = (currentPage - 1) * rowsPerPage;
        const end = start + rowsPerPage;
        const rowsToDisplay = filteredRows.slice(start, end);

        rowsToDisplay.forEach(row => tableBody.appendChild(row));
        document.getElementById("pageIndicator").innerText = `Page ${currentPage}`;
    }

    function updatePaginationControls() {
        document.getElementById("prevPage").classList.toggle("disabled", currentPage === 1);
        document.getElementById("nextPage").classList.toggle("disabled", currentPage === totalPages);
    }

    document.getElementById("prevPage").addEventListener("click", function (event) {
        event.preventDefault();
        if (currentPage > 1) {
            currentPage--;
            displayRows();
            updatePaginationControls();
        }
    });

    document.getElementById("nextPage").addEventListener("click", function (event) {
        event.preventDefault();
        if (currentPage < totalPages) {
            currentPage++;
            displayRows();
            updatePaginationControls();
        }
    });

    // Search Specific employee
    function handleSearch() {
        const searchValue = document.getElementById("SearchItem").value.trim().toLowerCase();

        if (searchValue === "") {
            filteredRows = [...originalRows]; // Reset to show all original rows
        } else {
            filteredRows = originalRows.filter(row => {
                const empID = row.querySelector("td:first-child").innerText.toLowerCase();
                const name = row.querySelector("td:nth-child(2)").innerText.toLowerCase();
                return empID.includes(searchValue) || name.includes(searchValue);
            });
        }

        totalPages = Math.ceil(filteredRows.length / rowsPerPage);
        currentPage = 1; // Reset to first page on search
        displayRows();
        updatePaginationControls();
    }

    document.getElementById("SearchItem").addEventListener("keyup", handleSearch);
    document.querySelector("button[type='submit']").addEventListener("click", function (e) {
        e.preventDefault();
        handleSearch();
    });

    function showSuccessMessage() {
        const successMessage = document.getElementById("successMessage");
        successMessage.style.display = "block";
        setTimeout(() => {
            successMessage.style.display = "none";
        }, 2000);
    }

    displayRows();
    updatePaginationControls();
});

// View and edit single employee when clicking view button
document.getElementById('closeModal').addEventListener('click', function () {
    $('#viewEmployeeModal').modal('hide');
});

function OpenModels(data) {
    // Update modal with employee details
    const employee = data.success[0];

    var dateOfJoining = employee.dateOfJoining.split("T")[0];
    var createdDate = employee.createdDate.split("T")[0];
    var modifiedDate = employee.modifiedDate.split("T")[0];
    var dateOfBirth = employee.dateOfBirth.split("T")[0];
    

    $('#empID').text(employee.empID);
    $('#firstName').text(employee.firstName);
    $('#lastName').text(employee.lastName);
    $('#phone').text(employee.phone);
    $('#designation').text(employee.designation);
    $('#experience').text(employee.experience);
    $('#dateOfBirth').text(dateOfBirth);
    $('#dateOfJoining').text(dateOfJoining);
    $('#address').text(employee.address);
    $('#pinCode').text(employee.pinCode);
    $('#city').text(employee.city);
    $('#state').text(employee.state);
    $('#Location').text(employee.location);
    $('#panNumber').text(employee.panNumber);
    $('#aadhaarCard').text(employee.aadhaarCard);
    $('#contactSite').text(employee.contactSite);
    $('#bankName').text(employee.bankName);
    $('#bankAddress').text(employee.bankAddress);
    $('#accountNumber').text(employee.accountNumber);
    $('#iFSC').text(employee.iFSC);
    $('#salary').text(employee.salary);
    $('#pFNumber').text(employee.pFNumber);
    $('#workingHours').text(employee.workingHours);
    $('#createdBy').text(employee.createdBy);
    $('#modifiedBy').text(employee.modifiedBy);
    $('#createdDate').text(createdDate);
    $('#modifiedDate').text(modifiedDate);
    $('#roleCode').text(employee.roleCode);
    $('#userId').text(employee.userId);
    $('#userName').text(employee.userName);

    // Show the modal
    $('#viewEmployeeModal').modal('show');

    // Handle edit button click
    $('#editBtn').off('click').on('click', function () {
        enableEditMode();
    });

    // Handle cancel button click
    $('#cancelBtn').off('click').on('click', function () {
        disableEditMode();
    });

    // Handle save changes button click
    $('#saveChangesBtn').off('click').on('click', function () {
        var updatedData = {
            empID: employee.empID,
            firstName: $('#firstName').text().trim(),
            lastName: $('#lastName').text().trim(),
            phone: $('#phone').text().trim(),
            designation: $('#designation').text().trim(),
            experience: parseInt($('#experience').text().trim(), 10),
            dateOfBirth: new Date($('#dateOfBirth').text().trim()),
            dateOfJoining: new Date($('#dateOfJoining').text().trim()),
            address: $('#address').text().trim(),
            pinCode: $('#pinCode').text().trim(),
            city: $('#city').text().trim(),
            location: $('#Location').text().trim(),
            state: $('#state').text().trim(),
            panNumber: $('#panNumber').text().trim(),
            aadhaarCard: $('#aadhaarCard').text().trim(),
            contactSite: $('#contactSite').text().trim(),
            bankName: $('#bankName').text().trim(),
            bankAddress: $('#bankAddress').text().trim(),
            accountNumber: $('#accountNumber').text().trim(),
            iFSC: $('#iFSC').text().trim(),
            salary: parseFloat($('#salary').text().trim()),
            pFNumber: $('#pFNumber').text().trim(),
            workingHours: $('#workingHours').text().trim(),
            createdBy: $('#createdBy').text().trim(),
            modifiedBy: $('#modifiedBy').text().trim(),
            createdDate: new Date($('#createdDate').text().trim()),
            modifiedDate: new Date($('#modifiedDate').text().trim()),
            roleCode: $('#roleCode').text().trim(),
            userId: $('#userId').text().trim(),
            userName: $('#userName').text().trim(),
        };

        saveChanges(updatedData);
    });

    // Initially disable editing
    disableEditMode();
}

function enableEditMode() {
    // Enable editing on fields
    $('#firstName, #lastName, #phone, #designation, #experience, #dateOfBirth, #dateOfJoining, #address, #pinCode, #city, #state, #panNumber, #aadhaarCard, #contactSite,#Location, #bankName, #bankAddress, #accountNumber, #iFSC, #salary, #pFNumber, #workingHours, #createdBy, #modifiedBy, #createdDate, #modifiedDate, #roleCode, #userId, #userName').attr('contenteditable', 'true');
    // Show save and cancel buttons
    $('#saveChangesBtn').removeClass('hide');
    $('#cancelBtn').removeClass('hide');
    $('#editBtn').addClass('hide');
}

function disableEditMode() {
    // Disable editing on fields
    $('#firstName, #lastName, #phone, #designation, #experience,#Location #dateOfBirth, #dateOfJoining, #address, #pinCode, #city, #state, #panNumber, #aadhaarCard, #contactSite, #bankName, #bankAddress, #accountNumber, #iFSC, #salary, #pFNumber, #workingHours, #createdBy, #modifiedBy, #createdDate, #modifiedDate, #roleCode, #userId, #userName').attr('contenteditable', 'false');
    // Hide save and cancel buttons
    $('#saveChangesBtn').addClass('hide');
    $('#cancelBtn').addClass('hide');
    $('#editBtn').removeClass('hide');
}

function saveChanges(updatedData) {
    $.ajax({
        url: '/AddEmployee/SaveEmployeeChangesInfo', // Your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(updatedData),
        success: function (response) {
            if (response.success) {
                console.log("Changes saved successfully:", response);
                disableEditMode();
                $('#viewEmployeeModal').modal('hide');
                showSuccessMessage("Update Employee... ");
                $('#alertSuccess').removeClass('hide');
            } else {
                console.error("Error saving changes:", response.error);
            }
        },
        error: function (error) {
            console.error("Error saving changes:", error);
            alert('Error: ' + error.responseText);
        }
    });
}



// Delete Table data 
function deleteEmployeeInformation(empId, event) {
    event.preventDefault(); // Prevent the default link behavior

    $.ajax({
        url: '/AddEmployee/DeleteSingleEmployeeDetails', // Ensure this matches your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ EmpID: empId }), // Send the employee ID in the request body
        success: function (response) {
            if (response == true) {
                showSuccessMessage("Employee deleted successfully.");
                // Optionally remove the deleted row from the table
                GetAllEmployeeDetailsInTableFormat();
            } else {
                alert('Error: ' + response.error);
            }
        },
        error: function (error) {
            console.error('Error deleting employee:', error);
            showSuccessMessage('Error: ' + error.responseText);
        }
    });
}

// Success And Error message.
function showSuccessMessage(customMessage) {
    var messageElement = document.getElementById("messageContent");
    var successMessageDiv = document.getElementById("successMessage");

    if (!messageElement || !successMessageDiv) {
        console.error("Message element or success message div not found");
        return;
    }

    // Update the message content
    messageElement.textContent = customMessage;

    // Show the success message div
    successMessageDiv.style.display = "block";

    console.log("Message displayed: " + customMessage);

    // Hide the success message div after 3 seconds
    setTimeout(function () {
        successMessageDiv.style.display = "none";
        console.log("Message hidden");
    }, 5000);
}
