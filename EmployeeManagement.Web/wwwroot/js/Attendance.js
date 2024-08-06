$(document).ready(function () {
    calendar();
});

document.addEventListener('DOMContentLoaded', function () {
    const otRadioButton = document.getElementById('OT_EMP100');
    const otModal = new bootstrap.Modal(document.getElementById('otModal'), {});

    otRadioButton.addEventListener('change', function () {
        if (otRadioButton.checked) {
            otModal.show();
        }
    });

    document.getElementById('saveOtDetails').addEventListener('click', function () {
        // Collect and process form data
        const hours = document.getElementById('otHours').value;
        const comment = document.getElementById('otComment').value;

        console.log('Hours:', hours);
        console.log('Comment:', comment);

        // Close the modal
        otModal.hide();
    });
});

function calendar() {
   
}
document.addEventListener("DOMContentLoaded", function () {
    // Initialize flatpickr on the search input
    flatpickr("#SearchItem", {
        // Set default date to today
        defaultDate: "today",
        // Allow the user to select a date from the calendar
        dateFormat: "Y-m-d", // Adjust the date format as needed
    });
});