$(document).ready(function () {
    $("#loginBTN").click(openLoginForm);
    $("#registerBTN").click(function () {
        openRegistrationForm();
    });
    $("#logoutBTN").click(Logout);
    if (localStorage.getItem("loggedUser")) {
        accountDetails()
        $("#loginBTN").hide();
        $("#registerBTN").hide();
        $("#logoutBTN").show();
    }
    else {
        document.getElementById("accountDetails").innerText = "";
        $("#loginBTN").show();
        $("#registerBTN").show();
        $("#logoutBTN").hide();
    }
});

//login system
function accountDetails() {

    let api = `https://localhost:7163/api/Users/` + localStorage.getItem('loggedUser');
    ajaxCall("GET", api, "", getAccount, failedAccount);

}
function getAccount(account) {
    document.getElementById("accountDetails").innerHTML =
        `Hello <span style="color: red; font-weight: bold;">${account.name}</span> Balance: 
        <span style="color: red; font-weight: bold;">${account.balance}$</span>`;
    console.log(account);
}
function failedAccount(err) {
    console.log(err);
}
//login form
function openLoginForm() {
    var url = "loginForm.html";

    var width = 600;
    var height = 700;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var features = `width=${width},height=${height},left=${left},top=${top},resizable=yes,scrollbars=yes`;

    // Open the registration form page in a pop-up window
    window.open(url, "_blank", features);

}
//Logout
function Logout() {
    if (localStorage.getItem("loggedUser")) {
        localStorage.removeItem("loggedUser");
        $("#libraryBTN").hide();
        alert("Disconnected succefully");
        location.reload();
    }
}
//register form
function openRegistrationForm() {
    var url = "RegisterForm.html";

    var width = 600;
    var height = 700;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var features = `width=${width},height=${height},left=${left},top=${top},resizable=yes,scrollbars=yes`;

    // Open the registration form page in a different window
    window.open(url, "_blank", features);
}
