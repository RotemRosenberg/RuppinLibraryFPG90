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
    GetAuthors();
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

//get all author
function GetAuthors() {
    let api = `https://localhost:7163/api/Author`;
    ajaxCall("GET", api, "", getSCBF, getECBF);
}
function getSCBF(result) {
    RenderAuthors(result);
    console.log(result);
}
function getECBF(err) {
    console.log(err);
}

function RenderAuthors(data) {
    document.getElementById('searchBooks').style.display = 'block';
    document.getElementById('allAuthors').innerHTML = '';
    document.getElementById('AuthorBooks').innerHTML = '';
    document.getElementById('allBooks').innerHTML = '';
    const authorContainer = document.getElementById('allAuthors');
    for (let author of data) {
        const authorDiv = document.createElement('div');
        authorDiv.className = "bookDiv";

        let authorImg = document.createElement('img');
        if (author.gender == 'M') {
            authorImg.src = '../images/ManAuthor.png';
        }
        else {
            authorImg.src = '../images/GirlAuthor.png';
        }
        authorDiv.appendChild(authorImg);

        let title = document.createElement('h3');
        title.innerText = author.authorName;
        authorDiv.appendChild(title);

        let age = document.createElement('p');
        age.innerText ="Age: " + (2024 - author.yearBirth);
        authorDiv.appendChild(age);

        let authorDescription = document.createElement('p');
        authorDescription.innerText = author.description;
        authorDiv.appendChild(authorDescription);

        let btnAuthorBooks = document.createElement('button');
        btnAuthorBooks.innerText = 'View Books';
        btnAuthorBooks.classList.add('btnStyle');
        btnAuthorBooks.addEventListener('click', function () {
            localStorage.setItem("Author", author.authorName);
            let api = `https://localhost:7163/api/Book/` + author.authorName + `?type=author`;
         ajaxCall("GET", api, "", searchSCBF, searchECBF);
        });
        authorDiv.appendChild(btnAuthorBooks);
        authorContainer.appendChild(authorDiv);
    }
}
function searchSCBF(result) {
    if (result.length === 0) {
        alert("No books found written by the specified author.");
    } else {
        RenderBooks(result);
    }
    console.log(result);
}
function searchECBF(err) {
    alert("No books found written by the specified author.")
    console.log(err);
}

function RenderBooks(data) {
    document.getElementById('searchBooks').style.display = 'none';
    document.getElementById('allAuthors').innerHTML = '';
    document.getElementById('AuthorBooks').innerHTML = '';
    document.getElementById('allBooks').innerHTML = '';
    const AuthorBooksContainer = document.getElementById('AuthorBooks');
    let AuthorTitle = document.createElement('h1')
    AuthorTitle.innerText = localStorage.getItem("Author") + " Books";
    AuthorBooksContainer.appendChild(AuthorTitle)
    localStorage.removeItem("Author");
    const bookContainer = document.getElementById('allBooks');

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
        Name.innerText = "Author: " + book.authorNames[0];
        if (book.authorNames[1] != "") {
            Name.innerText += ", " + book.authorNames[1];
        }
        if (book.authorNames[2] != "") {
            Name.innerText += ", " + book.authorNames[2];
        }
        bookDiv.appendChild(Name);

        let ebook = document.createElement('p');
        ebook.innerText = book.isEbook ? "Digital book" : "Physical book";
        bookDiv.appendChild(ebook);

        let price = document.createElement('p');
        price.innerText = "Price: " + book.price + "$";
        bookDiv.appendChild(price);

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
