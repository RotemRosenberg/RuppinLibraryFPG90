$(document).ready(function () {
    GetBooks();
    VoiceToText()

    $("#searchButton").click(function () {
        SearchBooksBy();
    });
});
function GetBooks() {

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book`;
    ajaxCall("GET", api, "", getSCBF, getECBF);

}
function getSCBF(result) {
    RenderAllBooks(result);
    console.log(result);
}
function getECBF(err) {
    console.log(err);
}


function RenderAllBooks(data) {
    document.getElementById('allBooks').innerHTML = '';
    const bookContainer = document.getElementById('allBooks');
    RenderBooks(data, bookContainer)
}


//filters
function SearchBooksBy() {
    let searchType = $("#searchType").val();
    let searchInput = $("#searchInput").val();
    let api;
    if (searchType == 'title') api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/` + searchInput +`?type=title`;
    else if (searchType == 'text') api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/` + searchInput + `?type=text`;
    else if (searchType == 'author') api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/` + searchInput + `?type=author`;
    ajaxCall("GET", api, "", searchSCBF, searchECBF);
}
function searchSCBF(result) {
    if (result.length === 0) {
        Swal.fire({
            icon: "error",
            title: "No books found matching your search",
            text: "Something went wrong!",
            footer: '<a href="#">Why do I have this issue?</a>'
        });    } else {
        RenderAllBooks(result);
    }
    console.log(result);
}
function searchECBF(err) {
    Swal.fire({
        icon: "error",
        title: "No books found matching your search",
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