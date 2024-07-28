$(document).ready(function () {
    function GetAllEmployeeDetailsInTableFormat();
});

function GetAllEmployeeDetailsInTableFormat()
{

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
                                    name=
                                <td>${employee.empID}</td>
                                <td>${name}</td>
                                <td>${employee.phoneNo}</td>
                                <td>${employee.salary}</td>
                                <td>${employee.designation}</td>
                                <td>
                                    <div class="dropdown">
                                        <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown"><i class="mdi mdi-dots-vertical"></i></button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item EmployeeDTView" onclick="GetEmployeeAllInformationClickViewButton('${employee.empID}')" href=""><i class="far fa-eye"></i> View</a>
                                            <a class="dropdown-item deleteEmployee" onclick="deleteEmployeeInformation('${employee.empID}')" ><i class="mdi mdi-trash-can-outline me-1"></i> Delete</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>`;
                    tableBody.append(row);
                });

                //  $('#successMessage').show().delay(3000).fadeOut(); // Display success message
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
function GetEmployeeAllInformationClickViewButton(empId) {
    alert(empId);
    $.ajax({
        url: '/AddEmployee/ViewEmployeeAllDetails', // Replace with your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ EmpID: empId }),
        success: function (response) {
            alert('Data received successfully: ' + response.message);
            OpenModels(response);
        },
        error: function (error) {
            alert('Error: ' + error.responseText);
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
        // showSuccessMessage(); // Uncomment if you want to show success message on search
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

//View  and edit single employee when click view button

document.getElementById('closeModal').addEventListener('click', function () {
    // window.location.href = '/AddEmployee/AddEmployeePage';  // Replace with the actual URL you want to redirect to
    $('#viewEmployeeModal').modal('hide');
});

function OpenModels(data) {
    // Update modal with employee details
    $('#empID').text(data.data.empID);
    $('#empName').text(data.data.empName);
    $('#empPosition').text(data.data.empPosition);
    // Add other fields as needed

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
            empID: data.data.empID,
            empName: $('#empName').text().trim(),
            empPosition: $('#empPosition').text().trim()
            // Add other updated fields here
        };

        saveChanges(updatedData);
    });

    // Initially disable editing
    disableEditMode();
}

function enableEditMode() {
    // Enable editing on fields
    $('#empName').attr('contenteditable', 'true');
    $('#empPosition').attr('contenteditable', 'true');
    // Show save and cancel buttons
    $('#saveChangesBtn').removeClass('hide');
    $('#cancelBtn').removeClass('hide');
    $('#editBtn').addClass('hide');
}

function disableEditMode() {
    // Disable editing on fields
    $('#empName').attr('contenteditable', 'false');
    $('#empPosition').attr('contenteditable', 'false');
    // Hide save and cancel buttons
    $('#saveChangesBtn').addClass('hide');
    $('#cancelBtn').addClass('hide');
    $('#editBtn').removeClass('hide');
}

function saveChanges(updatedData) {
    $.ajax({
        url: '/AddEmployee/SaveEmployeeChanges', // Replace with your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(updatedData),
        success: function (response) {
            if (response.success) {
                console.log("Changes saved successfully:", response);
                // Update the modal with saved changes
                $('#empName').text(response.empName);
                $('#empPosition').text(response.empPosition);
                // Add other fields as needed
                disableEditMode();
                $('#alertSuccess').removeClass('hide');
                setTimeout(function () {
                    $('#alertSuccess').addClass('hide');
                }, 3000); // Hide after 3 seconds
            


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






//Delete Table data 
function deleteEmployeeInformation(empId) {
    $.ajax({
        url: '/AddEmployee/DeleteSingleEmployeeDetails', // Ensure this matches your controller endpoint URL
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ EmpID: empId }), // Send the employee ID in the request body
        success: function (response) {
            if (response == true) {

               
                showSuccessMessage("Table deleted successfully.");

            } else {
                alert('Error: ' + response.error);
            }
        },
        error: function (error) {
          
            showSuccessMessage('Error: ' + error.responseText);
        }
    });
}


// Success And Error massage.
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

// Test the function


