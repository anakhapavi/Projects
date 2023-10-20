//validate firstName
function validateFname() {
    const fnameInput = document.getElementById('fname');
    const fnameError = document.getElementById('fnameError');
    const fnameRegex = /^[A-Z][a-z]*$/;

    if (!fnameRegex.test(fnameInput.value)) {
        fnameError.textContent = 'First letter must be in caps';
        fnameError.style.display = 'block';
        fnameInput.classList.add('invalid');
    } else {
        fnameError.style.display = 'none';
        fnameInput.classList.remove('invalid');
    }
}

//validate Lname
function validateLname() {
    const lnameInput = document.getElementById('lname');
    const lnameError = document.getElementById('lnameError');
    const lnameRegex = /^[A-Z][a-z]*$/;

    if (!lnameRegex.test(lnameInput.value)) {
        lnameError.textContent = 'First letter must be in caps';
        lnameError.style.display = 'block';
        lnameInput.classList.add('invalid');
    } else {
        lnameError.style.display = 'none';
        lnameInput.classList.remove('invalid');
    }
}

//validate phone
function validatePhone() {
    const phoneInput = document.getElementById('phone');
    const phoneError = document.getElementById('phoneError');
    const phoneRegex = /^\d{10}$/;

    if (!phoneRegex.test(phoneInput.value)) {
        phoneError.textContent = 'Phone number should contain exactly 10 digits';
        phoneError.style.display = 'block';
        phoneInput.classList.add('invalid');
    } else {
        phoneError.style.display = 'none';
        phoneInput.classList.remove('invalid');
    }
}


//validate email
function validateEmail() {
    const emailInput = document.getElementById('email');
    const emailError = document.getElementById('emailError');
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (!emailRegex.test(emailInput.value)) {
        emailError.textContent = 'Invalid Email Address.';
        emailError.style.display = 'block';
        emailInput.classList.add('invalid');
    } else {
        emailError.style.display = 'none';
        emailInput.classList.remove('invalid');
    }
}

//validate password
function validatePassword() {
    console.log("validatePassword called");
    const passwordInput = document.getElementById('password1');
    const passwordError = document.getElementById('passwordError');
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/;

    if (!passwordRegex.test(passwordInput.value)) {
        passwordError.textContent = 'Password must contain at least one letter, one number, and one special character.';
        passwordError.style.display = 'block';
        passwordInput.classList.add('invalid');
    } else if (passwordInput.value.length < 6) {
        passwordError.textContent = 'Password must be at least 6 characters long.';
        passwordError.style.display = 'block';
        passwordInput.classList.add('invalid');
    } else {
        passwordError.style.display = 'none';
        passwordInput.classList.remove('invalid');
    }
}


//generate username

    function generateUsername() {
        var firstName = document.getElementById("fname").value;
    var usernameTextbox = document.getElementById("username");

    if (firstName) {
            var username = "T" + firstName;

    usernameTextbox.value = username;
        }
    }

  


