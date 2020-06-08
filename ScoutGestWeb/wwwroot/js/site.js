// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var max = 3;
var Chefe = false;

function selectiveCheck() {
    var checkedChecks = document.querySelectorAll(".cargo:checked");
    if (document.querySelector("#guia:checked") || document.querySelector("#chefe:checked")) max = 1;
    else max = 3;
    if (checkedChecks.length > max) return false;
}

document.querySelectorAll('select#chefe')[0].onclick = function () {
    var select = document.querySelectorAll("#chefe");
    Chefe = select.options[select.selectedIndex].text == "Sim";
}

/*function chefe() {
    var select = document.querySelectorAll("#chefe");
    Chefe = select.options[select.selectedIndex].text == "Sim";
}*/