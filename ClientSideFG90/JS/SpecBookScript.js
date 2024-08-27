const createBookReview = (userId, bookId, rating, review, userName) => ({ userId, bookId, rating, review, userName });

$(document).ready(function () {
    SpecificBook();
    VoiceToText()
    $(".icon--star i").click(function () {
        let rating = $(this).data("value");
        $("#ratingOutput").text("Selected Rating: " + rating);
        console.log("Selected Rating: " + rating);
        localStorage.setItem('rating', rating);
    });
    $("#submitReview").click(function () {
        if (localStorage.getItem("loggedUser")) {
            AddBookReview();

        }
        else {
            Swal.fire({
                icon: "error",
                title: "Please log in to access this feature.",
                text: "Something went wrong!",
            });
} 
  });
});


//DisplaySpecificBook
function SpecificBook() {
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/id/` + localStorage.getItem('selectedBookId');
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
    localStorage.setItem('book', book.id);
        let bookIMGContainer = document.getElementById('imgBook');
        let bookTextContainer = document.getElementById('bookText');
        let bookReviewContainer = document.getElementById('bookReview');
    bookIMGContainer.innerHTML = '';
    bookTextContainer.innerHTML = '';
    bookReviewContainer.innerHTML = '';

        // תמונת הספר
        let bookImg = document.createElement('img');
        bookImg.src = book.smallPicURL;
        bookIMGContainer.appendChild(bookImg);

        // כותרת הספר
        let title = document.createElement('h2');
        title.innerText = book.title;
        bookTextContainer.appendChild(title);

        // כותרת משנה
        let subTitle = document.createElement('h4');
        subTitle.innerText = book.subTitle;
        bookTextContainer.appendChild(subTitle);

    // שם המחבר
    let Name = document.createElement('p');
    Name.innerText = "Author: " + book.authorNames[0];
    if (book.authorNames[1] != "") {
        Name.innerText += ", " + book.authorNames[1];
    }
    if (book.authorNames[2] != "") {
        Name.innerText += ", " + book.authorNames[2];
    }
    bookTextContainer.appendChild(Name);

    // קטגוריות
    let categories = document.createElement('p');
    categories.innerText = "Categories: "+book.categories;
    bookTextContainer.appendChild(categories);

    // שפה
    let language = document.createElement('p');
    language.innerText = "Language: " + book.language;
    bookTextContainer.appendChild(language);

    // הוצאה לאור
    let publishedDate = document.createElement('p');
    publishedDate.innerText = "Published Date: " + book.publishedDate;
    bookTextContainer.appendChild(publishedDate);

    //מפרסם
    let publisher = document.createElement('p');
    publisher.innerText = "Publisher: " + book.publisher;
    bookTextContainer.appendChild(publisher);
    //דפים
    let pageCount = document.createElement('p');
    pageCount.innerText = "Page Count: " + book.pageCount;
    bookTextContainer.appendChild(pageCount);

    //תיאור
    let description = document.createElement('p');
    description.innerText = "Description: " + book.description;
    bookTextContainer.appendChild(description);

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
                    addToWishList(book);
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
    bookTextContainer.appendChild(wishListBTN);
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/BookReview/` + book.id;
    ajaxCall("GET", api, "", bookReviewSCBF, bookReviewECBF);
}
function bookReviewSCBF(result) {
    RenderBookReview(result);
    console.log(result);
}
function bookReviewECBF(err) {
    console.log(err);
}

function RenderBookReview(data) {
    if (data.length==0) {
        document.getElementById("bookReviewTitle").style.display = 'none'
        document.getElementById('bookReview').style.display = 'none';
    }
    else {
        document.getElementById("bookReviewTitle").style.display = 'block'

        let bookReviewContainer = document.getElementById('bookReview');
        bookReviewContainer.style.backgroundColor = 'f7f7f7';
        for (let review of data) {
            let bookReviewDiv = document.createElement('div');
            bookReviewDiv.className = "bookreviewDiv";

            // יצירת קונטיינר עבור שם המשתמש ואייקון
            let userDiv = document.createElement('div');
            userDiv.className = "userDiv";

            // אייקון של המשתמש
            let userIcon = document.createElement('i');
            userIcon.className = 'fas fa-user user-icon';
            userDiv.appendChild(userIcon);

            // שם המשתמש
            let userName = document.createElement('span');
            userName.className = "userName";
            userName.innerText = review.userName; // assuming 'userName' is a property in 'review'
            userDiv.appendChild(userName);

            bookReviewDiv.appendChild(userDiv); // הוספת קונטיינר המשתמש לקונטיינר הביקורת

            // ציון הספר (כוכבים)
            let rating = document.createElement('div');
            rating.className = "stars";

            for (let i = 1; i <= 5; i++) {
                let star = document.createElement('i');
                if (i <= Math.floor(review.rating)) {
                    star.className = 'fas fa-star filled';
                } else if (i === Math.ceil(review.rating) && !Number.isInteger(review.rating)) {
                    star.className = 'fas fa-star-half-alt filled';
                } else {
                    star.className = 'far fa-star';
                }
                rating.appendChild(star);
            }

            bookReviewDiv.appendChild(rating);  // הוספת הדירוג לקונטיינר הטקסט
            //תיאור
            let description = document.createElement('p');
            description.innerText = review.review;
            bookReviewDiv.appendChild(description);
            bookReviewContainer.appendChild(bookReviewDiv)
        }
    }
}

function AddBookReview() {
    let newBookReview = createBookReview(localStorage.getItem('loggedUser'), localStorage.getItem('book'), localStorage.getItem('rating'), document.getElementById('reviewText').value, localStorage.getItem('userName'));
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/BookReview`;
    ajaxCall("POST", api, JSON.stringify(newBookReview), postRSCBF, postRECBF);
    return false;

}

function postRSCBF(data) {
    console.log(data);
    if (data) {

        Swal.fire({
            title: 'Your review has been successfully submitte.',
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

                    console.log(result);
                    window.location.reload();

            }
        });

    }
    else {
        Swal.fire({
            icon: "error",
            title: "You have already reviewed this book.",
            text: "Something went wrong!",
        });
    }
}
function postRECBF(err) {
    Swal.fire({
        icon: "error",
        title: "You have already reviewed this book.",
        text: "Something went wrong!",
    });
    console.log(err);
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

//buy checkmark
function showSuccessCheckmark() {
    // Create a new div for the checkmark
    let checkmarkDiv = document.createElement('div');

    // Set styles for the checkmark
    checkmarkDiv.innerHTML = '&#10004;'; // Unicode for a checkmark
    checkmarkDiv.style.position = 'fixed';
    checkmarkDiv.style.top = '50%';
    checkmarkDiv.style.left = '50%';
    checkmarkDiv.style.transform = 'translate(-50%, -50%)';
    checkmarkDiv.style.fontSize = '100px';
    checkmarkDiv.style.color = 'green';
    checkmarkDiv.style.zIndex = '1000';
    checkmarkDiv.style.backgroundColor = 'rgba(255, 255, 255, 0.8)';
    checkmarkDiv.style.padding = '20px';
    checkmarkDiv.style.borderRadius = '50%';
    checkmarkDiv.style.textAlign = 'center';
    checkmarkDiv.style.boxShadow = '0 0 10px rgba(0, 0, 0, 0.2)';

    // Append the checkmark to the body
    document.body.appendChild(checkmarkDiv);

    // Remove the checkmark after 2 seconds
    setTimeout(function () {
        checkmarkDiv.remove();
        alert("Purchase Successful! Thank you for your purchase.Enjoy your new book!");
    }, 1500);
}
//voice to text
function VoiceToText() {
    const startButton = document.getElementById('startButton');
    const searchInput = document.getElementById('reviewText');

    const recognition = new (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition)();
    recognition.lang = 'en-US';

    recognition.onstart = () => {
        startButton.innerHTML = '<i class="fas fa-microphone-alt"></i>';
    };

    recognition.onresult = (event) => {
        const transcript = event.results[0][0].transcript;
        searchInput.value = transcript; // Update the search input field with the recognized text
    };

    recognition.onend = () => {
        startButton.innerHTML = '<i class="fas fa-microphone"></i>';
    };

    startButton.addEventListener('click', () => {
        recognition.start();
    });
}