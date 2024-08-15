function GetSalaryInSalaryTable() {
    alert("Click ");

    $.ajax({
        url: '/api/employee/GetEmployees',
        method: 'GET',
        success: function (data) {
            var rows = '';
            $.each(data, function (index, employee) {
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
        },
        error: function (err) {
            console.log(err);
        }
    });
}


// Advance salary

