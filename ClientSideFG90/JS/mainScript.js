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
    if (localStorage.getItem("loggedUser")==1) {
        document.getElementById("adminLink").style.display = "block";
    }
    else document.getElementById("adminLink").style.display = "none";
});

//login systemff
function accountDetails() {

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Users/` + localStorage.getItem('loggedUser');
    ajaxCall("GET", api, "", getAccount, failedAccount);

}
function getAccount(account) {
    document.getElementById("accountDetails").innerHTML =
        `Hello <span style="color: red; font-weight: bold;">${account.name}</span> Balance: 
        <span style="color: red; font-weight: bold;">${account.balance}$</span>`;
    console.log(account);
    localStorage.setItem('userBalance', account.balance);
    localStorage.setItem('userName', account.name);

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
        localStorage.removeItem("userBalance");
        localStorage.removeItem("userName");
        $("#libraryBTN").hide();
        Swal.fire({
            title: "Disconnected succefully",
            showClass: {
                popup: `
      animate__animated
      animate__fadeInUp
      animate__faster
    `
            },
            hideClass: {
                popup: `
      animate__animated
      animate__fadeOutDown
      animate__faster
    `
            }
        }).then((result) => {
            if (result.isConfirmed) {
                // Add a delay before hiding the form and reloading the page
                location.reload();
            }
        });

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




//render book func
function RenderBooks(data, bookContainer) {
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
            // יצירת אלמנט חדש של כוכב באמצעות תג <i>
            let star = document.createElement('i');

            if (i <= Math.floor(book.averageRating)) {
                // אם 'i' קטן או שווה לחלק השלם של דירוג הספר
                // נוסיף כוכב מלא (מלא לגמרי)
                star.className = 'fas fa-star filled';
            } else if (i === Math.ceil(book.averageRating) && !Number.isInteger(book.averageRating)) {
                // אם 'i' שווה לערך המעוגל כלפי מעלה של דירוג הספר
                // והדירוג אינו מספר שלם, נוסיף חצי כוכב
                star.className = 'fas fa-star-half-alt filled';
            } else {
                // אחרת, נוסיף כוכב ריק
                star.className = 'far fa-star';
            }

            // הוספת הכוכב שנוצר אל הקונטיינר של הדירוג
            rating.appendChild(star);
        }
        bookDiv.appendChild(rating);

        //More details
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

        // add to wishlist button

        let wishListBTN = document.createElement('button');
        wishListBTN.innerText = 'Buy Book';
        wishListBTN.classList.add('btnStyle');
        wishListBTN.addEventListener('click', function () {
            if (localStorage.getItem('loggedUser')) {
                if (localStorage.getItem('userBalance') >= book.price) {
                    if (book.isBooked == 1 && book.isEbook == false) {
                        Swal.fire({
                            icon: "error",
                            title: "The book has already been purchased by another user.\n Please try purchasing it through the online shop.",
                            text: "Something went wrong!",
                        });
                    }
                    else {    // שמירת ה-ID של הספר ב-localStorage
                        localStorage.setItem('selectedBookId', book.id);
                        confirmPurchase(book);
                    }
                }
                else {
                    Swal.fire({
                        icon: "error",
                        title: "You don't have enough money in your account to purchase this book",
                        text: "Something went wrong!",
                    });
                }
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: 'Please login',
                    text: "Something went wrong!",
                });
            }
        });

        bookDiv.appendChild(wishListBTN);
        bookContainer.appendChild(bookDiv);
    }
}

function addToWishList(book) {
    let loggedUserID = localStorage.getItem('loggedUser')
    if (loggedUserID == null) {
        Swal.fire({
            icon: "error",
            title: "You may login in order to add item to your wishlist",
            text: "Something went wrong!",
        });   
    }
    else { // logged in user
        let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks?bookID=${book.id}&userID=${loggedUserID}&bookPrice=${book.price}`;
        ajaxCall("POST", api, "", addTWLSCBF, addTWLECBF);
    }

}

function addTWLSCBF(res) {
    accountDetails();
    showSuccessCheckmark();
    console.log(res);

}
function addTWLECBF(err) {
    console.log(err);
    Swal.fire({
        icon: "error",
        title: "You have already purchased this book",
        text: "Something went wrong!",
    });   
}
function confirmPurchase(book) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success custom-btn',
            cancelButton: 'btn btn-danger custom-btn'
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: `Do you want to purchase this book for ${book.price}$?`,
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Yes, Buy the book!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            if (addToWishList(book)) {
                swalWithBootstrapButtons.fire({

                    title: "Thanks for buying",
                    text: "Book have been added to your presonal library!",
                    icon: "success"
                });
            }
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your purchase has been canceled.",
                icon: "error"
            });
        }
    });
}
function showSuccessCheckmark() {
    Swal.fire({
        position: "center",
        icon: "success",
        title: "Book have been added to your presonal library!",
        showConfirmButton: false,
        timer: 1500
    });
}