// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const passField = document.querySelector(".form input[type='password']");
const toggleIcon = document.querySelector('form .field i');
toggleIcon.classList.remove("active");

toggleIcon.onclick = function () {
    if (passField.type == "password") {
        passField.type = "Text";
        toggleIcon.classList.add("active");
    }
    else {
        passField.type = "Password";
        toggleIcon.classList.remove("active");
    }
}



