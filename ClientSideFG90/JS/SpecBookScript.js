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
    SpecificBook();
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

//DisplaySpecificBook
function SpecificBook() {
    let api = `https://localhost:7163/api/Book/id/` + localStorage.getItem('selectedBookId');
    ajaxCall("GET", api, "", getSCBF, getECBF);
}
function getSCBF(result) {
    RenderSpecificBook(result);
    console.log(result);
}
function getECBF(err) {
    console.log(err);
}

function RenderSpecificBook(book) {

        // מוודא שהאלמנטים קיימים לפני המשך הביצוע
        let bookIMGContainer = document.getElementById('imgBook');
        let bookTextContainer = document.getElementById('bookText');
        let bookReviewContainer = document.getElementById('bookReview');
    bookIMGContainer.innerHTML = '';
    bookTextContainer.innerHTML = '';
    bookReviewContainer.innerHTML = '';
        if (!bookIMGContainer || !bookTextContainer || !bookReviewContainer) {
            console.error('One or more elements were not found in the DOM');
            console.log(document.getElementById('bookIMG'));
            console.log(document.getElementById('bookText'));
            console.log(document.getElementById('bookReview'));

            return;
        }

        // תמונת הספר
        let bookImg = document.createElement('img');
        bookImg.src = book.smallPicURL;
        bookIMGContainer.appendChild(bookImg);

        // כותרת הספר
        let title = document.createElement('h2');
        title.innerText = book.title;
        bookTextContainer.appendChild(title);

        // כותרת משנה
        let subTitle = document.createElement('h3');
        subTitle.innerText = book.subTitle;
        bookTextContainer.appendChild(subTitle);

        // שם המחבר
        let authorName = document.createElement('p');
        authorName.innerText = "Author: " + book.authorsID;
        bookTextContainer.appendChild(authorName);

        // סוג הספר (דיגיטלי או פיזי)
        let ebook = document.createElement('p');
        ebook.innerText = book.isEbook ? "Digital book" : "Physical book";
        bookTextContainer.appendChild(ebook);

        // מחיר הספר
        let price = document.createElement('p');
        price.innerText = "Price: " + book.price + "$";
        bookTextContainer.appendChild(price);

        // ציון הספר (כוכבים)
        let rating = document.createElement('div');
        rating.className = "stars";

        for (let i = 1; i <= 5; i++) {
            let star = document.createElement('i');
            if (i <= Math.floor(book.averageRating)) {
                star.className = 'fas fa-star filled';
            } else if (i === Math.ceil(book.averageRating) && !Number.isInteger(book.averageRating)) {
                star.className = 'fas fa-star-half-alt filled';
            } else {
                star.className = 'far fa-star';
            }
            rating.appendChild(star);
        }

        bookTextContainer.appendChild(rating);  // הוספת הדירוג לקונטיינר הטקסט
}
