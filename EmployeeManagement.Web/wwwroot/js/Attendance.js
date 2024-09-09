
$(document).ready(function () {
    GetAllEmployeeDetailsInTableFormat();
});
document.addEventListener('DOMContentLoaded', function () {
    const otModalElement = document.getElementById('otModal');
    const otModal = new bootstrap.Modal(otModalElement, {});
    let selectedEmpRow = null;

    // Clear modal fields when the modal is shown
    otModalElement.addEventListener('show.bs.modal', function () {
        clearModalData();
    });

    // Event delegation for OT checkbox
    document.getElementById('EmployeeAttendance').addEventListener('change', function (event) {
        if (event.target.classList.contains('ot-checkbox')) {
            if (event.target.checked) {
                selectedEmpRow = event.target.closest('tr');
                otModal.show();
            }
        }
    });

    // Save OT details
    document.getElementById('saveOtDetails').addEventListener('click', function () {
        const hours = document.getElementById('otHours').value;
        const comment = document.getElementById('otComment').value;
        if (!hours || isNaN(hours) || hours <= 0) {
            alert('Please enter valid OT hours.');
            return;
        }

        if (selectedEmpRow) {
            const otDetails = {
                Hours: hours,
                Comment: comment
            };

            // Store OT details as a data attribute in the row
            selectedEmpRow.dataset.otDetails = JSON.stringify(otDetails);
        }

        otModal.hide();
    });

    //// Initialize flatpickr on the search input

        const today = new Date();
        const yesterday = new Date(today);
        const tomorrow = new Date(today);

        yesterday.setDate(today.getDate() - 1);
        tomorrow.setDate(today.getDate() + 1);

        const formattedToday = today.toISOString().split('T')[0];
        const formattedYesterday = yesterday.toISOString().split('T')[0];
        const formattedTomorrow = tomorrow.toISOString().split('T')[0];

        flatpickr("#DatePicker", {
            dateFormat: "Y-m-d",
            defaultDate: "today",
            minDate: formattedYesterday,
            maxDate: formattedTomorrow,
            onChange: function (selectedDates, dateStr, instance) {
                if (dateStr !== formattedToday && dateStr !== formattedYesterday && dateStr !== formattedTomorrow) {
                    alert('Please select only Today, Tomorrow, or Yesterday.');
                    instance.setDate(formattedToday, true); // Reset to today if invalid date selected
                }
            }
        });


    // Search functionality
    document.getElementById('SearchItem').addEventListener('input', function () {
        const searchValue = this.value.toLowerCase();
        document.querySelectorAll('#EmployeeAttendance tbody tr').forEach(row => {
            const text = row.textContent.toLowerCase();
            row.style.display = text.includes(searchValue) ? '' : 'none';
        });
    });

    // Event listener for "Fill All Present" button
    document.getElementById('fillAllPresent').addEventListener('click', function () {
        document.querySelectorAll('#EmployeeAttendance tbody tr').forEach(row => {
            const presentCheckbox = row.querySelector('input[name^="attendance"]:first-child');
            if (presentCheckbox) {
                presentCheckbox.checked = true;
            }
            const absentCheckbox = row.querySelector('input[name^="attendance"]:nth-child(2)');
            if (absentCheckbox) {
                absentCheckbox.checked = false;
            }
        });
    });
});
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
});

// Submit all data
function SubmitAllAttendanceData() {
    const tableData = [];
    const selectedDate = document.getElementById('DatePicker').value; // Get selected date

    document.querySelectorAll('#EmployeeAttendance tbody tr').forEach(row => {
        const empId = row.querySelector('td:nth-child(1)').textContent;

        const attendance = row.querySelector('input[name^="attendance"]:checked')?.value || '';
        const otDetailsAttr = row.getAttribute('data-ot-details') || '{}';
        const otDetails = JSON.parse(otDetailsAttr);

        const reqData = {
            EmpID: empId,
            Attendance: attendance,
            AttendanceDate: selectedDate, // Include the date
            OTDetails: {
                EmpID: empId,
                Hours: otDetails.Hours || 0,
                Comment: otDetails.Comment || ''
            }
        };

        tableData.push(reqData);
    });

    AttendanceDateSendToController(tableData);
}

// Clear modal data when reopening the modal
function clearModalData() {
    document.getElementById('otHours').value = '';
    document.getElementById('otComment').value = '';
    selectedEmpRow = null; // Clear selected employee row
}

function showAttendanceData(data) {
    const tableBody = document.querySelector('#EmployeeAttendance tbody');
    tableBody.innerHTML = ''; // Clear any existing rows

    if (Array.isArray(data)) {
        data.forEach(function (employee) {
            const name = employee.firstName + " " + employee.lastName;
            const row = `<tr class="table-default search-items">
                            <td>${employee.empID}</td>
                            <td>${name}</td>
                            <td>${employee.designation}</td>
                            <td>${employee.signIn}</td>
                            <td>${employee.signOut}</td>
                            <td>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="attendance${employee.empID}" id="P_${employee.empID}" value="P">
                                    <label class="form-check-label" for="P_${employee.empID}"></label>
                                </div>
                            </td>
                            <td>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="attendance${employee.empID}" id="A_${employee.empID}" value="A">
                                    <label class="form-check-label" for="A_${employee.empID}"></label>
                                </div>
                            </td>
                            <td>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input ot-checkbox" type="checkbox" id="OT_${employee.empID}" data-emp-id="OT_${employee.empID}">
                                    <label class="form-check-label" for="OT_${employee.empID}"></label>
                                </div>
                            </td>
                        </tr>`;
            tableBody.insertAdjacentHTML('beforeend', row);
            // Add event listeners for P and A radio buttons
            const pRadio = document.getElementById(`P_${employee.empID}`);
            const aRadio = document.getElementById(`A_${employee.empID}`);
            const pLabel = pRadio.nextElementSibling;
            const aLabel = aRadio.nextElementSibling;

            pRadio.addEventListener('change', function () {
                if (pRadio.checked) {
                    pLabel.style.color = 'green';
                    aLabel.style.color = ''; // Reset A label color
                }
            });

            aRadio.addEventListener('change', function () {
                if (aRadio.checked) {
                    aLabel.style.color = 'red';
                    pLabel.style.color = ''; // Reset P label color
                }
            });
        });
    } else {
        console.error("Expected data to be an array but got:", data);
    }
}

// Function to fetch data and show it using the above function
function GetAllEmployeeDetailsInTableFormat() {
    $('#loadingSpinner').show(); // Show the spinner
    $.ajax({
        url: '/AddEmployee/GetEmployeeDetailsInTableForm',
        method: 'GET',
        success: function (data) {
            showAttendanceData(data);
        },
        error: function (error) {
            console.error('Error fetching employee data', error);
        },
        complete: function () {
            $('#loadingSpinner').hide(); // Hide the spinner
        }
    });
}
function AttendanceDateSendToController(tableData) {
    $('#loadingSpinner').show(); // Show the spinner

    $.ajax({
        url: '/AttendancesController/SendEmployeeAttendanceDetails',
        type: 'POST',
        data: JSON.stringify(tableData),
        contentType: 'application/json',
        success: function (response) {
           
            // Default message settings
            var message = '';
            var alertClass = '';

            if (response.success === true) {
                message = 'Data submitted successfully!';
                alertClass = 'alert-success'; // Bootstrap class for success
            } else {
                message = 'Data submission failed!';
                alertClass = 'alert-danger'; // Bootstrap class for error
            }

       

            // Update and show the success/error message
            $('#successMessage').text(message).removeClass().addClass('alert ' + alertClass).show();

            // Hide the message after 3 seconds
            setTimeout(function () {
                $('#successMessage').fadeOut('slow');
            }, 3000);
        },
        error: function (err) {
           

            // Handle AJAX errors
            var errorMessage = 'An error occurred while submitting data. Please try again later.';
            $('#successMessage').text(errorMessage).removeClass().addClass('alert alert-danger').show();

            // Hide the message after 3 seconds
            setTimeout(function () {
                $('#successMessage').fadeOut('slow');
            }, 3000);
        },
        complete: function () {
            // Hide the spinner
            $('#loadingSpinner').hide();
        }
    });
}

