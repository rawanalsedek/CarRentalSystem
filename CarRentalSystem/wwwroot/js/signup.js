const signupForm = document.getElementById("signupForm");

if (signupForm) {
    const fields = {
        fullName: document.getElementById("fullName"),
        email: document.getElementById("email"),
        password: document.getElementById("password"),
        confirmPassword: document.getElementById("confirmPassword"),
        agreeTerms: document.getElementById("agreeTerms")
    };

    const formStatus = document.getElementById("formStatus");

    function showFieldError(fieldName, message) {
        const field = fields[fieldName];
        const error = document.getElementById(`${fieldName}Error`);
        const inputBox = field.closest(".auth-input");

        error.textContent = message;

        if (inputBox) {
            inputBox.classList.toggle("has-error", Boolean(message));
        }
    }

    function validateForm() {
        const fullName = fields.fullName.value.trim();
        const email = fields.email.value.trim();
        const password = fields.password.value;
        const confirmPassword = fields.confirmPassword.value;

        showFieldError("fullName", fullName ? "" : "Please enter your full name.");
        showFieldError("email", fields.email.validity.valid ? "" : "Please enter a valid email address.");
        showFieldError("password", password.length >= 8 ? "" : "Password must be at least 8 characters.");
        showFieldError("confirmPassword", password === confirmPassword && confirmPassword ? "" : "Passwords do not match.");
        showFieldError("agreeTerms", fields.agreeTerms.checked ? "" : "Please agree to the Terms & Conditions.");

        return Boolean(fullName) && Boolean(email) && fields.email.validity.valid && password.length >= 8 && password === confirmPassword && fields.agreeTerms.checked;
    }

    signupForm.addEventListener("submit", (event) => {
        if (formStatus) {
            formStatus.textContent = "";
        }

        if (!validateForm()) {
            event.preventDefault();
        }
    });

    Object.entries(fields).forEach(([fieldName, field]) => {
        field.addEventListener("input", () => {
            if (fieldName === "agreeTerms") {
                showFieldError(fieldName, field.checked ? "" : "Please agree to the Terms & Conditions.");
                return;
            }

            if (fieldName === "confirmPassword") {
                showFieldError(fieldName, field.value && field.value === fields.password.value ? "" : "Passwords do not match.");
            }
        });
    });
}
