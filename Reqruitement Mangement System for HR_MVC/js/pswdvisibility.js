function togglePasswordVisibility(fieldId, iconId) {
    var passwordInput = document.getElementById(fieldId);
    var passwordIcon = document.getElementById(iconId);

    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        passwordIcon.className = "glyphicon glyphicon-eye-close";
    } else {
        passwordInput.type = "password";
        passwordIcon.className = "glyphicon glyphicon-eye-open";
    }
}
