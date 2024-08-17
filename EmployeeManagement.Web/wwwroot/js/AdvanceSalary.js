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
            var formData = {
                empID: $('#inputEmpID').val(),
                name: $('#inputName').val(),
                advanceAmount: $('#inputAdvanceAmount').val(),
                advanceDate: $('#inputAdvanceDate').val(),
                paymentTime: $('#inputPaymentTime').val(),
                paymentMode: $('#inputPaymentMode').val(),
                paymentBy: $('#inputPaymentBy').val(),
                otherCredit: $('#inputOtherCredit').val(),
                otherComment: $('#inputOtherComment').val()
            };

            $.ajax({
                url: '/your-controller-endpoint', // Replace with your server endpoint
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: function (response) {
                    $('#modalBody').html('<p>Submission successful!</p>');
                    $('#resultModal').modal('show');
                },
                error: function (xhr, status, error) {
                    $('#modalBody').html('<p>An error occurred: ' + error + '</p>');
                    $('#resultModal').modal('show');
                }
            });
        }
    });
});
