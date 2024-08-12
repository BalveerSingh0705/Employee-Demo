
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

    // Initialize flatpickr on the search input
    flatpickr("#DatePicker", {
        defaultDate: "today",
        dateFormat: "Y-m-d",
    });

    // Search functionality
    document.getElementById('SearchItem').addEventListener('input', function () {
        const searchValue = this.value.toLowerCase();
        document.querySelectorAll('#EmployeeAttendance tbody tr').forEach(row => {
            const text = row.textContent.toLowerCase();
            row.style.display = text.includes(searchValue) ? '' : 'none';
        });
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
        });
    } else {
        console.error("Expected data to be an array but got:", data);
    }
}

// Function to fetch data and show it using the above function
function GetAllEmployeeDetailsInTableFormat() {
    $.ajax({
        url: '/AddEmployee/GetEmployeeDetailsInTableForm',
        method: 'GET',
        success: function (data) {
            showAttendanceData(data);
        },
        error: function (error) {
            console.error('Error fetching employee data', error);
        }
    });
}
function AttendanceDateSendToController(tableData) {
    $.ajax({
        url: '/AttendancesController/SendEmployeeAttendanceDetails',
        type: 'POST',
        data: JSON.stringify(tableData),
        contentType: 'application/json',
        success: function (response) {
            alert('Data submitted successfully!');
        },
        error: function (error) {
            alert('Error submitting data: ' + error.responseText);
        }
    });
}
