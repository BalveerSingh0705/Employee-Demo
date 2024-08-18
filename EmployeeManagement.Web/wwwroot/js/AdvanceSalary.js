$(document).ready(function () {

    $('#salaryForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        // Clear previous validation messages
        $('.is-invalid').removeClass('is-invalid');

        // Custom validation
        let isValid = true;

        // Validate EmpID
        if (!$('#inputEmpID').val()) {
            isValid = false;
            $('#inputEmpID').addClass('is-invalid');
        }

        // Validate Name
        if (!$('#inputName').val()) {
            isValid = false;
            $('#inputName').addClass('is-invalid');
        }

        // Validate Advance Amount
        let advanceAmount = $('#inputAdvanceAmount').val();
        if (!advanceAmount || parseFloat(advanceAmount) <= 0) {
            isValid = false;
            $('#inputAdvanceAmount').addClass('is-invalid');
        }

        // Validate Advance Date
        if (!$('#inputAdvanceDate').val()) {
            isValid = false;
            $('#inputAdvanceDate').addClass('is-invalid');
        }

        // Validate Payment Time
        if (!$('#inputPaymentTime').val()) {
            isValid = false;
            $('#inputPaymentTime').addClass('is-invalid');
        }

        // Validate Payment Mode
        if (!$('#inputPaymentMode').val()) {
            isValid = false;
            $('#inputPaymentMode').addClass('is-invalid');
        }

        // Validate Payment By
        if (!$('#inputPaymentBy').val()) {
            isValid = false;
            $('#inputPaymentBy').addClass('is-invalid');
        }

        // If form is valid, proceed with AJAX submission
        if (isValid) {
            var otherCreditValue = $('#inputOtherCredit').val();
            var formData = {
                EmpID: $('#inputEmpID').val(),
                Name: $('#inputName').val(),
                AdvanceAmount: $('#inputAdvanceAmount').val(),
                AdvanceDate: $('#inputAdvanceDate').val(),
                PaymentTime: $('#inputPaymentTime').val(),
                PaymentMode: $('#inputPaymentMode').val(),
                PaymentBy: $('#inputPaymentBy').val(),
                //OtherCredit: $('#inputOtherCredit').val(),
                OtherCredit: otherCreditValue ? otherCreditValue : 0,
                OtherComment: $('#inputOtherComment').val()
            };

            $.ajax({
                url: '/FinanceController/AdvanceSalary', // Ensure this matches your controller route
                type: 'POST',
                contentType: 'application/json', // Set content type to JSON
                data: JSON.stringify(formData), // Serialize the data to JSON
                success: function (response) {
                    if (response.success == true) {
                        $('#modalBody').html('<p>Advance Payment successful!</p>');
                        $('#salaryForm')[0].reset();


                    } else {
                        $('#modalBody').html('<p>Sorry, the employee you are trying to find does not exist.<br>Please check the employee ID and try again.</p>');


                    }
                    $('#resultModal').modal('show');
                },
                error: function (xhr, status, error) {
                    $('#modalBody').html('<p>Oops! Something went wrong. Please try again later. <br> Error details: ' + error + '</p>');

                    $('#resultModal').modal('show');
                }
            });
        }
    });
});
// View and edit single employee when clicking view button
document.getElementById('CloseModel').addEventListener('click', function () {
    $('#resultModal').modal('hide');
});
document.getElementById('CloseModel1').addEventListener('click', function () {
    $('#resultModal').modal('hide');
});
