function initializeEventHandlers() {
    // Toggle password visibility
    $('#togglePassword').on('click', function () {
        togglePasswordVisibility('#password', this);
    });

    $('#toggleConfirmPassword').on('click', function () {
        togglePasswordVisibility('#confirmPassword', this);
    });

    // Form submission with AJAX for registration
    $('#formAuthentication').on('submit', function (e) {
        e.preventDefault();
        handleRegistrationFormSubmission();
    });

    // Form submission with AJAX for login
    $("#loginForm").on('submit', function (event) {
        event.preventDefault();
        handleLoginFormSubmission(); // Ensure the login form submission is handled here
    });
}

// Function to toggle password visibility
function togglePasswordVisibility(passwordFieldSelector, toggleButton) {
    const passwordField = $(passwordFieldSelector);
    const type = passwordField.attr('type') === 'password' ? 'text' : 'password';
    passwordField.attr('type', type);
    $(toggleButton).find('i').toggleClass('mdi-eye-outline mdi-eye-off-outline');
}

// Function to handle registration form submission with AJAX
function handleRegistrationFormSubmission() {
    // Validation
    const password = $('#password').val();
    const confirmPassword = $('#confirmPassword').val();
    if (password !== confirmPassword) {
        showMessage('Passwords do not match.', 'danger');
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
        url: '/Auth/Register', // Adjust the URL to match your API controller and action
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        timeout: 5000, // Timeout after 5 seconds
        success: function (response) {
            if (response.success) {
                showMessage(response.message, 'success');
                // Optionally, reset the form
                $('#formAuthentication')[0].reset();
            } else {
                showMessage(response.message, 'danger');
            }
        },
        error: function (xhr, status, error) {
            let errorMessage = "An error occurred";
            if (status === "timeout") {
                errorMessage = "The request timed out. Please try again.";
            } else if (xhr.responseJSON) {
                errorMessage = xhr.responseJSON.message;
            }
            showMessage(errorMessage, 'danger');
        }
    });
}

// Function to handle login form submission with AJAX
function handleLoginFormSubmission() {
    const formData = {
        emailUsername: $("#email").val(),
        password: $("#password").val(),
        rememberMe: $("#remember-me").is(":checked")
    };

    loginFormSendDataToController(formData); // Corrected function name
}

function loginFormSendDataToController(formData) {
    // Perform the AJAX request
    $.ajax({
        url: "/Auth/Login",
        type: "POST",
        data: JSON.stringify(formData),
        contentType: "application/json",
        success: function (response) {
            if (response.success) {
                window.location.href = response.redirectUrl || '/Home/Index'; // Redirect on success
            } else {
                showMessage(response.message, 'danger'); // Show error message
            }
        },
        error: function (xhr, status, error) {
            showMessage("An error occurred while processing your request: " + error, 'danger');
        }
    });
}

// Function to show a message
function showMessage(message, type) {
    $('#message').html(`<div class="alert alert-${type}">${message}</div>`);
    hideMessageAfterDelay();
}

// Function to hide the message after 5 seconds
function hideMessageAfterDelay() {
    setTimeout(function () {
        $('#message').html('');
    }, 5000);
}

// Initialize event handlers when the document is ready
$(document).ready(function () {
    initializeEventHandlers();
});
