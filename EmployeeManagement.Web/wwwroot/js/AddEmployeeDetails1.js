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
$(".search-table").on("click", ".delete", function () {
    var row = $(this).closest("tr");
    var  empID = row.querySelector("td:first-child").innerText.toLowerCase(); // Assuming the first column is the EmpID
    alert(empID);
    var empID = row.find("td:first-child").text().trim(); // Assuming the second column is the Name
    alert(empName);
    console.log("Delete employee with EmpID:", empID, "and Name:", empName);
    SendDataToController(empID, empName);
    row.remove(); // Remove the row from the table
});




$(document).ready(function () {
    $('.view').click(function (e) {
        e.preventDefault(); // Prevent the default link behavior

        var empId = $(this).closest('tr').find('td:eq(0)').text();
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
    });
});
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

$(document).ready(function () {
    $('.deleteEmployee').click(function (e) {
        e.preventDefault(); // Prevent the default link behavior

        var empId = $(this).closest('tr').find('td:eq(0)').text();
        alert(empId);
        $.ajax({
            url: '/AddEmployee/DeleteEmployeeDetails', // Ensure this matches your controller endpoint URL
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ EmpID: empId }), // Send the employee ID in the request body
            success: function (response) {
                if (response.success) {
                    $('#successMessage').text(response.message).show();
                    setTimeout(function () {
                        $('#successMessage').text(response.message).hide();

                    }, 3000); // Hide after 3 seconds

                    // Show success message
                  

                    // Optionally, remove employee row from the table or refresh the list
                    // For example, if you have a function to refresh the employee list
                   // refreshEmployeeList(); // Implement this function to refresh the list

                } else {
                    alert('Error: ' + response.error);
                }
            },
            error: function (error) {
                alert('Error: ' + error.responseText);
            }
        });
    });
});
