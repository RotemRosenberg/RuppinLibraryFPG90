$(document).ready(function () {

    $("#registerForm").submit(Register);
    $("#loginForm").submit(Login);
});

const createUser = (id, name, email, password, isAdmin, balance) => ({ id, name, email, password, isAdmin, balance });
const defaultDate = '14/08/1998'
//---------login---------//
function Login() {
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Users/login?email=` + encodeURIComponent($('#emailTB').val());
    ajaxCall("POST", api, JSON.stringify($('#passwordTB').val()), postLSCBF, postLECBF);
    return false;
}

function postLSCBF(data) {
    if (data == null) {
        alert("User not found");
        document.getElementById('loginForm').reset();
    }
    else {
        localStorage.setItem("loggedUser", data.id);
        alert("Logged in succefully");
        window.opener.location.reload();
        window.close();
    }
    console.log(data);
}
function postLECBF(err) {
    alert('User not found')
    document.getElementById('loginForm').reset();
    console.log(err);
}


/*---------register---------//*/
function Register() {
    let newUser = createUser(0, $('#nameTB').val(), $('#emailTB').val(), $('#passwordTB').val(), false,0);
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Users/register`;
    ajaxCall("POST", api, JSON.stringify(newUser), postRSCBF, postRECBF);
    return false;

}

function postRSCBF(data) {
    console.log(data);
    if (data != null) {
        localStorage.setItem("loggedUser", data.id);
        alert("The user has been registered");
        alert("The user has been logged in");
        window.opener.location.reload();
        window.close();

    }
    else {
        alert("The user is already registered");
        document.getElementById('registerForm').reset();
    }
}
function postRECBF(err) {
    alert('error')
    console.log(err);
}