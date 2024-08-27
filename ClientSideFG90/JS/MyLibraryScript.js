$(document).ready(function () {
    //my library func
    $("#myLibraryBTN").click(function () {
        toggleSubmenu();
    });
    let urlParams = new URLSearchParams(window.location.search);
    let section = urlParams.get('section');
    switch (section) {
        case 'wishlist':
            GetWishListBooks();
            break;
        case 'read':
            GetReadBooks();
            break;
        case 'sale':
            GetSaleBooks();
            break;
        case 'requests':
            GetPurchaseRequests();
            break;
        default:
            // Do something if no section is specified, or show a default view
            break;
    }


    // My Wishlist
    $("#wishListBTN").click(function () {
        let loggedUserID = localStorage.getItem('loggedUser');
        if (loggedUserID != null) {
            window.location.href = 'MyLibrary.html?section=wishlist';
            GetWishListBooks();
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please login first!",
            });
        }
    });

    // Done Reading
    $("#booksIReadBTN").click(function () {
        let loggedUserID = localStorage.getItem('loggedUser');
        if (loggedUserID != null) {
            window.location.href = 'MyLibrary.html?section=read';
            GetReadBooks();
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please login first!",
            });
        }
    });

    // Online Books Shop
    $("#onlineShopBTN").click(function () {
        let loggedUserID = localStorage.getItem('loggedUser');
        if (loggedUserID != null) {
            window.location.href = 'MyLibrary.html?section=sale';
            GetSaleBooks();
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please login first!",
            });
        }
    });


    // Purchase Requests
    $("#purchaseBTN").click(function () {
        let loggedUserID = localStorage.getItem('loggedUser');
        if (loggedUserID != null) {
            window.location.href = 'MyLibrary.html?section=requests';
            GetPurchaseRequests();
        } else {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please login first!",
            });
        }
    });
});


//my library fun
function toggleSubmenu() { // הצגת התת נושאים של הספריה
    var submenu = document.getElementById("mylibrary-submenu");
    if (submenu.style.display === "none") {
        submenu.style.display = "block";
    }
    else {
        submenu.style.display = "none";
    }
}

//wish list - user books 
function GetWishListBooks() {
    let loggedUserID = localStorage.getItem('loggedUser');
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks/wishlist/` + loggedUserID;
    ajaxCall("GET", api, "", getSCBF1, getECBF1);
}

function getSCBF1(result) {
    renderBooksOfUser(result);
    console.log(result);
}

function getECBF1(err) {
    console.log(err);
}

//user read book
function GetReadBooks() {
    let loggedUserID = localStorage.getItem('loggedUser');
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks/readed/` + loggedUserID;

    ajaxCall("GET", api, "", readSCBF, readECBF);

}
function readSCBF(result) {
    renderBooksOfUser(result);
    console.log(result);
}

function readECBF(err) {
    console.log(err);
}
function renderBooksOfUser(booksList) {
    let urlParams = new URLSearchParams(window.location.search);
    let section = urlParams.get('section');
    if (section === 'wishlist') document.getElementById('myLibraryTitle').innerText = 'My books';
    else if (section === 'read') document.getElementById('myLibraryTitle').innerText = 'Books I`ve Read';
    else if (section === 'sale') document.getElementById('myLibraryTitle').innerText = 'Online Shop';
    else if (section === 'requests') document.getElementById('myLibraryTitle').innerText = 'My Purchases Requests';
    document.getElementById('booksContainer').innerHTML = '';
    const bookContainer = document.getElementById('booksContainer');

    for (let i = 0; i < booksList.length; i++) {

        let book = booksList[i].book ? booksList[i].book : booksList[i]; // Handle both cases
        let ownerID = booksList[i].owner || "N/A"; // Handle cases where owner may not exist

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
        if (section === 'sale') {
            let bookOwnerID = document.createElement('h4');
            bookOwnerID.innerText = "Owner ID:" + ownerID;
            bookDiv.appendChild(bookOwnerID);
        }
        if (section === 'requests') {
            let bookOwnerID = document.createElement('h4');
            bookOwnerID.innerText = "User ID:" + ownerID;
            bookDiv.appendChild(bookOwnerID);
        }
        let ebook = document.createElement('p');
        ebook.innerText = book.isEbook ? "Digital book" : "Physical book";
        bookDiv.appendChild(ebook);


        if (section === 'wishlist') {
            let markAsReadBtn = document.createElement('button');
            markAsReadBtn.innerText = 'Read Book';
            markAsReadBtn.classList.add('btnStyle', 'bookButton');
            markAsReadBtn.id = `markAsReadBTN-${i}`;
            markAsReadBtn.addEventListener('click', function () {
                //window.open(book.webReaderLink, "_blank");
                markBookAsRead(book.id);
            });
            bookDiv.appendChild(markAsReadBtn);
        }
        if (section === 'wishlist' || section === 'read') {
            let btn = document.createElement('button');
            btn.innerText = 'Remove Book';
            btn.classList.add('btnStyle');
            btn.id = `divBTN-${i}`; // unique id for every button in every rendered div
            btn.addEventListener('click', function () {
                removeFromWishList(book, section);
            });
            bookDiv.appendChild(btn);
        }
        if (section === 'sale') {
            let price = document.createElement('p');
            price.innerText = "Price: " + book.price + "$"
            bookDiv.appendChild(price);
            
            let buyBookBtn = document.createElement('button');
            buyBookBtn.innerText = 'Buy Book';
            buyBookBtn.classList.add('btnStyle', 'bookButton');
            buyBookBtn.id = `buyBookBTN-${i}`;
            buyBookBtn.addEventListener('click', function () {
                if (localStorage.getItem('userBalance') >= book.price) {
                    requestBookPurchase(book, ownerID, buyBookBtn.id);
                }
                else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "You don't have enough money in your account to purchase this book!",
                    });
                }
            });
            bookDiv.appendChild(buyBookBtn);           
        }
        if (section === 'requests') {
            let sellBookBtn = document.createElement('button');
            sellBookBtn.innerText = 'Approve Sell Request';
            sellBookBtn.classList.add('btnStyle', 'bookButton');
            sellBookBtn.id = `sellBookBTN-${i}`;
            sellBookBtn.addEventListener('click', function () {
                ApproveBookPurchaseRequests(book);
            });
            bookDiv.appendChild(sellBookBtn);
        }
        bookContainer.appendChild(bookDiv);

    }
}

//read book
function markBookAsRead(id) {
    let loggedUserID = localStorage.getItem('loggedUser');
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks/` + loggedUserID
    ajaxCall("PUT", api, JSON.stringify(id), MarkSCBF, MarkECBF);
}
function MarkSCBF(res) {
    Swal.fire({
        title: "Book has been marked as read",
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
        // This function is called after the alert is closed
        if (result.isConfirmed || result.isDismissed) {
            window.location.reload(); // Reload the page after the alert is closed
        }
    });
    console.log(res);
}
function MarkECBF(err) {
    console.log(err);
}

//sales books
function GetSaleBooks() {
    let loggedUserID = localStorage.getItem('loggedUser');
    console.log("Logged User ID:", loggedUserID);

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks/sale/` + loggedUserID;

    ajaxCall("GET", api, "", saleSCBF, saleECBF);

}
function saleSCBF(result) {
    console.log(result);
    renderBooksOfUser(result);
}

function saleECBF(err) {
    console.log(err);
}

//send request
function requestBookPurchase(book, SellerID, buyBookBtnID) {
    let loggedUserID = parseInt(localStorage.getItem('loggedUser'));
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/PurchaseRequest/request`;
    const data = {
        BookID: parseInt(book.id),
        SenderID: loggedUserID,
        ReceiverID: parseInt(SellerID)
    };
    console.log(data);

    ajaxCall("POST", api, JSON.stringify(data), requestSuccess, requestError);
    function requestSuccess(response) {
        console.log("Purchase request successful:", response);
        Swal.fire({
            title: "Request sent to the book owner",
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
            // This function is called after the alert is closed
            if (result.isConfirmed || result.isDismissed) {
                window.location.reload(); // Reload the page after the alert is closed
            }
        });
        let buyBookBtn = document.getElementById(buyBookBtnID);
        if (buyBookBtn) {
            buyBookBtn.disabled = true;
            buyBookBtn.textContent = "Request Sent";
        }
    }
    function requestError(err) {
        Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "You’ve already requested this book",
        });
        console.log(err)
    }
}

//get all request
function GetPurchaseRequests() {
    let loggedUserID = localStorage.getItem('loggedUser');
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/PurchaseRequest/` + loggedUserID;
    ajaxCall("GET", api, "", getRequestSCBF, getRequestECBF);
}
function getRequestSCBF(result) {
    console.log(result);
    renderBooksOfUser(result);
}

function getRequestECBF(err) {
    console.log(err);
}


//approve request

function ApproveBookPurchaseRequests(book) {
    let bookOwner = parseInt(localStorage.getItem('loggedUser'));
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/PurchaseRequest/` + book.price;
    console.log(book.title)
    const data = {
        BookID: parseInt(book.id),
        ReceiverID: bookOwner
    };
    console.log(data);

    ajaxCall("PUT", api, JSON.stringify(data), approveSuccess, approveError);
}
function approveSuccess(result) {
    console.log(result);
    Swal.fire({
        position: "center",
        icon: "success",
        title: "The book has been sold",
        showConfirmButton: false,
        timer: 1500
    }).then(() => {
        // Perform action after the alert is closed
        location.reload();
    });
}

function approveError(err) {
    console.log(err);
}

// delete book from my books
function removeFromWishList(book, section) {

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks?bookID=${book.id}&userID=${localStorage.getItem('loggedUser')}`;
    ajaxCall("DELETE", api, "", deleteSCBF, deleteECBF);

    function deleteSCBF(res) {
        alert("Book has been deleted from your " + section);
        location.reload();
    }
    function deleteECBF(err) {
        console.log(err);
    }
}