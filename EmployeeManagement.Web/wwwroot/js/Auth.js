$(document).ready(function () {
    // Toggle password visibility
    $('#togglePassword').on('click', function () {
        const passwordField = $('#password');
        const type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);
        $(this).find('i').toggleClass('mdi-eye-outline mdi-eye-off-outline');
    });

    $('#toggleConfirmPassword').on('click', function () {
        const confirmPasswordField = $('#confirmPassword');
        const type = confirmPasswordField.attr('type') === 'password' ? 'text' : 'password';
        confirmPasswordField.attr('type', type);
        $(this).find('i').toggleClass('mdi-eye-outline mdi-eye-off-outline');
    });

    // Form submission with AJAX
    $('#formAuthentication').on('submit', function (e) {
        e.preventDefault();

        // Validation
        const password = $('#password').val();
        const confirmPassword = $('#confirmPassword').val();
        if (password !== confirmPassword) {
            alert('Passwords do not match.');
            return;
        }

        const formData = {
            Username: $('#username').val(),
            Email: $('#email').val(),
            Phone: $('#phone').val(),
            Password: password,
            ConfirmPassword: confirmPassword,
            Terms: $('#terms-conditions').is(':checked')
        };

        $.ajax({
            url: '/AuthController/AuthRegister', // Adjust the URL to match your API controller and action
            type: 'POST',
            data: JSON.stringify(formData),
            contentType: 'application/json',
            success: function (response) {
                // Handle success response
                if (response.success) {
                    // Handle success (e.g., redirect or display success message)
                    window.location.href = "/LoginBasic";
                } else {
                    // Display error message
                    $('.error-message').text(response.error).show();
                }
           
            },
            error: function (xhr, status, error) {
                // Handle error response
                $('.error-message').text('An error occurred. Please try again.').show();
            }
        });
    });
});

