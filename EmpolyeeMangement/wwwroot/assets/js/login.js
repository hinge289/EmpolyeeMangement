document.addEventListener("DOMContentLoaded", function () {
    const passwordField = document.getElementById('myInput');
    const eyeIcon = document.getElementById('eyeIcon');

    if (eyeIcon) {
        eyeIcon.addEventListener("click", function () {
            if (passwordField.type === "password") {
                passwordField.type = "text";
                eyeIcon.classList.replace("fa-eye", "fa-eye-slash");
            } else {
                passwordField.type = "password";
                eyeIcon.classList.replace("fa-eye-slash", "fa-eye");
            }
        });
    }
});
