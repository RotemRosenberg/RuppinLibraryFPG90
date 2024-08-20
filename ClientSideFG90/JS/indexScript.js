$(document).ready(function () {
    Top5Books();
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
function Top5Books() {

    let api = `https://localhost:7163/api/Book/Top5`;
    ajaxCall("GET", api, "", getSCBF, getECBF);

}
function getSCBF(result) {
    RenderTop5Books(result);
    console.log(result);
}
function getECBF(err) {
    console.log(err);
}

function RenderTop5Books(data) {
    document.getElementById('top5').innerHTML = '';
    const bookContainer = document.getElementById('top5');
    for (let book of data) {
        const bookDiv = document.createElement('div');
        bookDiv.className = "bookDiv";
        let bookImg = document.createElement('img');
        bookImg.src = book.smallPicURL;
        bookDiv.appendChild(bookImg);

        let title = document.createElement('h3');
        title.innerText = book.title;
        bookDiv.appendChild(title);

        let subTitle = document.createElement('h4');
        subTitle.innerText = book.subTitle;
        bookDiv.appendChild(subTitle);

        let Name = document.createElement('p');
        Name.innerText = "Author: " + book.authorName1;
        if (book.authorName2 != null) {
            Name.innerText += ", " + book.authorName2;
        }
        if (book.authorName3 != null) {
            Name.innerText += ", " + book.authorName3;
        }
        bookDiv.appendChild(Name);

        let ebook = document.createElement('p');
        if (book.isEbook) ebook.innerText = "Digital book";
        else ebook.innerText = "Physical book";
        bookDiv.appendChild(ebook);

        let price = document.createElement('p');
        price.innerText = "Price: " + book.price + "$"
        bookDiv.appendChild(price);

        // יצירת אלמנט הכוכבים
        let rating = document.createElement('div');
        rating.className = "stars";

        for (let i = 1; i <= 5; i++) {
            let star = document.createElement('i');
            if (i <= Math.floor(book.averageRating)) {
                star.className = 'fas fa-star filled'; // כוכב מלא
            } else if (i === Math.ceil(book.averageRating) && !Number.isInteger(book.averageRating)) {
                star.className = 'fas fa-star-half-alt filled'; // חצי כוכב
            } else {
                star.className = 'far fa-star'; // כוכב ריק
            }
            rating.appendChild(star);
        }
        bookDiv.appendChild(rating);


        let btnBookDetails = document.createElement('button');
        btnBookDetails.innerText = 'More Details';
        btnBookDetails.classList.add('btnStyle');
        btnBookDetails.addEventListener('click', function () {
            // שמירת ה-ID של הספר ב-localStorage
            localStorage.setItem('selectedBookId', book.id);

            // מעבר לדף SpecificBook.html
            window.location.href = 'SpecificBook.html';
        });
        bookDiv.appendChild(btnBookDetails);

        bookContainer.appendChild(bookDiv);
    }
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