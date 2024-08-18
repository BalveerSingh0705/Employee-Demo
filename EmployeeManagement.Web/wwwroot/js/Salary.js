// Function to set default values for month and year
function setDefaultValues() {
    const now = new Date();
    const currentMonth = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const currentYear = now.getFullYear();

    document.getElementById('monthSelect').value = currentMonth;
    document.getElementById('yearInput').value = currentYear;

    // Fetch and display the default data
    GetSalaryInSalaryTable('', currentMonth, currentYear);
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
                        '<td>' + employee.name + '</td>' +
                        '<td>' + employee.designation + '</td>' +
                        '<td>' + employee.salary + '</td>' +
                        '<td>' + employee.advance + '</td>' +
                        '<td>' + employee.pf + '</td>' +
                        '<td>' + employee.esif + '</td>' +
                        '<td>' + employee.otherCredit + '</td>' +
                        '<td>' + employee.totalSalary + '</td>' +
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
        }
    });
}


// Call setDefaultValues on page load
setDefaultValues();
