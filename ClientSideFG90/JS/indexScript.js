$(document).ready(function () {
    Top5Books();
    if (localStorage.getItem('loggedUser')) {
        $("#recommendedHeader").show();
        GetRecommandedBooks();
    }
});
function Top5Books() {

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/Top5`;
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
    RenderBooks(data, bookContainer);
}
function GetRecommandedBooks() {
    let loggedUserID = localStorage.getItem('loggedUser');
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/UserBooks/recommend/` + loggedUserID;
    ajaxCall("GET", api, "", reccommandSCBF, reccommandECBF);

}
function reccommandSCBF(result) {
    const bookContainer = document.getElementById('recommended');
    const bookCount = Array.isArray(result) ? result.length : 0;

    if (bookCount >= 5) { // הצגת הספרים המומלצים רק כאשר יש מספיק ספרים להצגה
        RenderBooks(result, bookContainer);
    }
    else {
        $("#recommendedHeader").hide();
    }
    console.log(result);
}
function reccommandECBF(err) {
    console.log(err);
}
