// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const searchBar = document.querySelector(".search input");
const searchIcon = document.querySelector(".search button");
const usersList = document.querySelector(".users-list");
var dropdown = document.getElementById('friendDropdown');

searchIcon.onclick = function () {
    searchBar.classList.toggle("show");
    searchIcon.classList.toggle("active");
    searchBar.focus();
    if (searchBar.classList.contains("active")) {
        searchBar.value = "";
        dropdown.innerHTML = '';
        dropdown.classList.remove('show')
        searchBar.classList.remove("active");
        searchBar.classList.remove("abc");

    }
}
searchBar.onkeyup = () => {
    let searchTerm = searchBar.value;
    if (searchTerm !== "") {
        searchBar.classList.add("active");
    } else {
        searchBar.classList.remove("active");
        dropdown.classList.remove("show");
        dropdown.innerHTML = '';

    }
}



