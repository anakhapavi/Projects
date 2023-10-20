$(document).ready(function () {
    $("#dob-datepicker").on("blur", function () {
        var selectedDate = new Date($(this).val());
        var today = new Date();
        var minDate = new Date();
        minDate.setFullYear(minDate.getFullYear() - 18);

        if (selectedDate > today) {
            $("#dob-error-message").text("Date of birth cannot be in the future.");
        } else if (selectedDate > minDate) {
            $("#dob-error-message").text("You must be at least 18 years old.");
        } else {
            $("#dob-error-message").text("");
        }
    });
});
