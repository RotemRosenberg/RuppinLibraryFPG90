$(document).ready(function () {
    VoiceToText()
    GetAuthors();
    $("#searchButton").click(function () {
        SearchAuthorsByName();
    });
});

//get all author
function GetAuthors() {
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Author`;
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
            let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/` + author.authorName + `?type=author`;
         ajaxCall("GET", api, "", searchSCBF, searchECBF);
        });
        authorDiv.appendChild(btnAuthorBooks);
        authorContainer.appendChild(authorDiv);
    }
}
function searchSCBF(result) {
    if (result.length === 0) {
        Swal.fire({
            icon: "error",
            title: "No books found written by the specified author.",
            text: "Something went wrong!",
            footer: '<a href="#">Why do I have this issue?</a>'
        });
    } else {
        RenderAuthorBooks(result);
    }
    console.log(result);
}
function searchECBF(err) {
    Swal.fire({
        icon: "error",
        title: "No books found written by the specified author.",
        text: "Something went wrong!",
        footer: '<a href="#">Why do I have this issue?</a>'
    });
    console.log(err);
}

function RenderAuthorBooks(data) {
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
    RenderBooks(data, bookContainer);
   

}


//filters
function SearchAuthorsByName() {
    let searchInput = $("#searchInput").val();
    let api = 'https://194.90.158.74/cgroup90/test2/tar1/api/Author/' + searchInput;

    ajaxCall("GET", api, "", searchAuthorSCBF, searchAuthorECBF);
}
function searchAuthorSCBF(result) {
    if (result.length === 0) {
        Swal.fire({
            icon: "error",
            title: "No Authors found matching your search",
            text: "Something went wrong!",
            footer: '<a href="#">Why do I have this issue?</a>'
        });
    } else {
        RenderAuthors(result);
    }
    console.log(result);
}
function searchAuthorECBF(err) {

    Swal.fire({
        icon: "error",
        title: "No Authors found matching your search",
        text: "Something went wrong!",
        footer: '<a href="#">Why do I have this issue?</a>'
    });
    console.log(err);
}

//voice to text
function VoiceToText() {
    const startButton = document.getElementById('startButton');
    const searchInput = document.getElementById('searchInput');

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