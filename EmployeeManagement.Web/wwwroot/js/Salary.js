// Function to set default values for month and year
function setDefaultValues() {
    const now = new Date();
    const currentMonth = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const currentYear = now.getFullYear();

    const monthSelect = document.getElementById('monthSelect');
    const yearInput = document.getElementById('yearInput');

    if (monthSelect && yearInput) { // Check if elements exist
        monthSelect.value = currentMonth;
        yearInput.value = currentYear;

        // Fetch and display the default data
        GetSalaryInSalaryTable('', currentMonth, currentYear);
    } else {
        console.error("Element not found: monthSelect or yearInput");
    }
}

// Function to handle search and filtering
function searchSalaries() {
    const empID = document.getElementById('empID').value;
    const month = document.getElementById('monthSelect').value;
    const year = document.getElementById('yearInput').value;

    GetSalaryInSalaryTable(empID, month, year);
}

// Function to get salary data based on empID, month, and year
function GetSalaryInSalaryTable(empID, month, year) {
    var Data = {
        empID: empID,
        month: month,
        year: year
    };

    // Show loader
    $('#loadingSpinner').show(); // Show the spinner

    $.ajax({
        url: '/FinanceController/FinalSalary',
        method: 'POST',
        contentType: 'application/json', // Set the content type to JSON
        data: JSON.stringify(Data), // Serialize the data as a JSON string
        success: function (response) {
            var rows = '';
            if (response.success && response.response) {
                $.each(response.response, function (index, employee) {
                    rows += '<tr>' +
                        '<td>' + employee.empID + '</td>' +
                        '<td>' + (employee.firstName + " " + employee.lastName) + '</td>' +
                        '<td>' + employee.designation + '</td>' +
                        '<td>' + employee.salary + '</td>' +
                        '<td>' + employee.advanceAmount + '</td>' +
                        '<td>' + employee.pfAmount + '</td>' +
                        '<td>' + employee.esiAmount + '</td>' +
                        '<td>' + employee.totalAttendances + '</td>' +
                        '<td>' + employee.totalSalaryForMonth + '</td>' +
                        '</tr>';
                });
                $('table tbody').html(rows);
            } else {
                // Handle the case where the response does not include employee data
                $('table tbody').html('<tr><td colspan="9">No data found.</td></tr>');
            }
        },
        error: function (err) {
            console.log("Error fetching salary data:", err);
            // Optionally, display an error message to the user
            $('table tbody').html('<tr><td colspan="9">An error occurred while fetching data. Please try again later.</td></tr>');
        },
        complete: function () {
            // Hide loader
            $('#loadingSpinner').hide();
        }
    });
}

// Call setDefaultValues immediately (you can call this wherever needed)
setDefaultValues();
