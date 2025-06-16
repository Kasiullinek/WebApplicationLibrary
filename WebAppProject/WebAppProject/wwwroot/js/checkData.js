// Dynamiczna walidacja daty urodzenia:
document.addEventListener('DOMContentLoaded', function () {
    console.log("DOMContentLoaded event fired");

    const dateOfBirthInput = document.getElementById('dateOfBirth');
    const dateOfBirthError = document.getElementById('dateOfBirthError');

    if (dateOfBirthInput && dateOfBirthError) {
        dateOfBirthInput.addEventListener('change', validateBirthDate);
    }

    function validateBirthDate() {
        const dateOfBirth = new Date(dateOfBirthInput.value);
        const today = new Date();
        const minDate = new Date(today.getFullYear() - 122, today.getMonth(), today.getDate());

        if (dateOfBirth > today) {
            dateOfBirthError.style.display = "block";
            dateOfBirthError.textContent = 'Date of birth cannot be in the future.';
        } else if (dateOfBirth < minDate) {
            dateOfBirthError.style.display = "block";
            dateOfBirthError.textContent = 'Date of birth cannot be more than 122 years ago.';
        } else {
            dateOfBirthError.style.display = "none";
        }
    }
});