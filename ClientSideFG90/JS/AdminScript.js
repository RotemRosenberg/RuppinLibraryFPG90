$(document).ready(function () {
    GetAdminBooks();
    $('#bookForm').hide();
    $("#addBook").click(function () {
        $('#bookForm').toggle();
    });
    $("#insertBook").click(function () {
        AddBook();
    }); 
    $("#BookedBooks").click(function () {
        let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/BookedBooks`;
        ajaxCall("GET", api, "", BBSCBF, BBECBF);

    });
    $("#BookedAuthors").click(function () {
        let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/BookedAuthors`;
        ajaxCall("GET", api, "", BASCBF, BAECBF);
    });
    $("#BookedUsers").click(function () {
        let api =`https://194.90.158.74/cgroup90/test2/tar1/api/Book/BookedUsers`;
        ajaxCall("GET", api, "", BUSCBF, BUECBF);
    });

});

//Books 
function BBSCBF(result) {
    BookedBooks(result);
    console.log(result);
}
function BBECBF(err) {
    console.log(err);
}
function BookedBooks(data) {
    const container = document.getElementById('tableDetails');
    container.innerHTML = '';
    const table = document.createElement('table');
    const th1 = document.createElement('th');
    th1.innerText='Book Name'
    const th2 = document.createElement('th');
    th2.innerText = 'Booked'
    table.appendChild(th1);
    table.appendChild(th2);
    const tbody = document.createElement('tbody');
    for (let book of data) {
        const row = document.createElement('tr');
        //title
        const titleCol = document.createElement('td');
        titleCol.textContent = book.title;
        row.appendChild(titleCol);
        //booked
        const bookedCol = document.createElement('td');
        bookedCol.textContent = book.isBooked;
        row.appendChild(bookedCol);
        tbody.appendChild(row);
    }
    table.appendChild(tbody);
    container.appendChild(table);
}
//Authors 
function BASCBF(result) {
    BookedAuthors(result);
    console.log(result);
}
function BAECBF(err) {
    console.log(err);
}
function BookedAuthors(data) {
    const container = document.getElementById('tableDetails');
    container.innerHTML = '';
    const table = document.createElement('table');
    const th1 = document.createElement('th');
    th1.innerText = 'Author Name'
    const th2 = document.createElement('th');
    th2.innerText = 'Booked'
    table.appendChild(th1);
    table.appendChild(th2);
    const tbody = document.createElement('tbody');
    for (let author of data) {
        const row = document.createElement('tr');
        //title
        const titleCol = document.createElement('td');
        titleCol.textContent = author.author;
        row.appendChild(titleCol);
        //booked
        const bookedCol = document.createElement('td');
        bookedCol.textContent = author.booked;
        row.appendChild(bookedCol);
        tbody.appendChild(row);
    }
    table.appendChild(tbody);
    container.appendChild(table);
}
//Users 
function BUSCBF(result) {
    BookedUsers(result);
    console.log(result);
}
function BUECBF(err) {
    console.log(err);
}
function BookedUsers(data) {
    const container = document.getElementById('tableDetails');
    container.innerHTML = '';
    const table = document.createElement('table');
    const th1 = document.createElement('th');
    th1.innerText = 'User Name'
    const th2 = document.createElement('th');
    th2.innerText = 'Booked'
    table.appendChild(th1);
    table.appendChild(th2);
    const tbody = document.createElement('tbody');
    for (let user of data) {
        const row = document.createElement('tr');
        //title
        const titleCol = document.createElement('td');
        titleCol.textContent = user.userName;
        row.appendChild(titleCol);
        //booked
        const bookedCol = document.createElement('td');
        bookedCol.textContent = user.booked;
        row.appendChild(bookedCol);
        tbody.appendChild(row);
    }
    table.appendChild(tbody);
    container.appendChild(table);
}
function createBook(id, title, subTitle, authorsID, publisher, description, pageCount, categories, averageRating, smallPicURL, picURL, language, preivewLink, isEbook, webReaderLink, price, publishedDate, authorNames, isBooked) {
    return {
        id,
        title,
        subTitle,
        authorsID,
        publisher,
        description,
        pageCount,
        categories,
        averageRating,
        smallPicURL,
        picURL,
        language,
        preivewLink,
        isEbook,
        webReaderLink,
        price,
        publishedDate,
        authorNames,
        isBooked
    };
}

function GetAdminBooks() {

    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/Admin`;
    ajaxCall("GET", api, "", adminSCBF, adminECBF);

}
function adminSCBF(result) {
    RenderBooks(result);
    console.log(result);
}
function adminECBF(err) {
    console.log(err);
}


function RenderBooks(data) {
    const container = document.getElementsByTagName('tbody')[0];
    for (let book of data) {
        const row = document.createElement('tr');
        //image
        const imageCol = document.createElement('td');
        const img = document.createElement('img');
        img.src = book.smallPicURL;
        img.alt = book.title;
        img.style.width = '50px'; // Adjust the size as needed
        imageCol.appendChild(img);
        row.appendChild(imageCol);


        //id
        const idCol = document.createElement('td');
        idCol.textContent = book.id;
        row.appendChild(idCol);

        //title
        const titleCol = document.createElement('td');
        titleCol.textContent = book.title;
        row.appendChild(titleCol);

        //subtitle
        const subTitleCol = document.createElement('td');
        subTitleCol.textContent = book.subTitle;
        row.appendChild(subTitleCol);

        //Authors
        for (let i = 0; i < 3; i++) {
            let authorNameCol = document.createElement('td');
            authorNameCol.textContent = book.authorNames[i];
            let authorIdCol = document.createElement('td');
            authorIdCol.textContent = book.authorsID[i];
            row.appendChild(authorNameCol);
            row.appendChild(authorIdCol);
        }
        //Publisher
        const publisherCol = document.createElement('td');
        publisherCol.textContent = book.publisher;
        row.appendChild(publisherCol);

        //Page Count
        const pageCountCol = document.createElement('td');
        pageCountCol.textContent = book.pageCount;
        row.appendChild(pageCountCol);

        //Categories
        const categoriesCol = document.createElement('td');
        categoriesCol.textContent = book.categories;
        row.appendChild(categoriesCol);

        //Rating
        const ratingCol = document.createElement('td');
        ratingCol.textContent = book.rating;
        row.appendChild(ratingCol);

        //Ebook
        const isEbookCol = document.createElement('td');
        isEbookCol.textContent = book.isEbook;
        row.appendChild(isEbookCol);

        //Price
        const PriceCol = document.createElement('td');
        PriceCol.textContent = book.price;
        row.appendChild(PriceCol);

        //PublishedDate
        const publishedDateCol = document.createElement('td');
        publishedDateCol.textContent = book.publishedDate;
        row.appendChild(publishedDateCol);
        //delete btn
        const deleteBtnCol = document.createElement('td');
        let deleteBtn = document.createElement('button');
        deleteBtn.innerText = 'Delete';
        deleteBtn.onclick = function () {
            let apiDelete = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/`+book.id
            ajaxCall("DELETE", apiDelete, '', deleteSCBF, deleteECBF)
        }
        deleteBtnCol.appendChild(deleteBtn);
        row.appendChild(deleteBtnCol); 
        container.appendChild(row);

    }
    new DataTable('#example', {
        paging: false,
        scrollCollapse: true,
        scrollY: '600px'
    });
}
function deleteSCBF(result) {
    Swal.fire({
        title: 'The book has been delete',
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
                console.log(result);
                window.location.reload();
        }
    });

}
function deleteECBF(err) {
    Swal.fire({
        icon: "error",
        title: "Error adding",
        text: "Something went wrong!",
        footer: '<a href="#">Why do I have this issue?</a>'
    });
    console.log(err);
}


function AddBook() {
    // Get the values from the input fields
    const title = document.getElementById('title').value;
    const subtitle = document.getElementById('subtitle').value;
    const authorName = document.getElementById('authorName').value;
    const publisher = document.getElementById('publisher').value;
    const description = document.getElementById('description').value;
    const categories = document.getElementById('categories').value;
    const pageCount = document.getElementById('pageCount').value;
    const imageLink = document.getElementById('imageLink').value;
    const language = document.getElementById('language').value;
    const previewLink = document.getElementById('previewLink').value;
    const isEbook = document.getElementById('isEbook').checked;
    const webReaderLink = document.getElementById('webReaderLink').value;
    const price = document.getElementById('price').value;

    const book = createBook(
        0,
        title,
        subtitle,
        [0, 0, 0],
        publisher,
        description,
        pageCount,
        categories,
        4.0,
        imageLink,
        imageLink,
        language,
        previewLink,
        isEbook,
        webReaderLink,
        price,
        2023,
        [authorName, "", ""],
        0
    );
    console.log(book)
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book`;
    ajaxCall("POST", api, JSON.stringify(book), addSCBF, addECBF);
}

function addSCBF(result) {
    Swal.fire({
        title: result.title + ' has been added',
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
            setTimeout(() => {
                document.getElementById('bookForm').style.display = 'none';
                console.log(result);
                window.location.reload();
            }, 1000); // Adjust the delay (in milliseconds) as needed
        }
    });

}
function addECBF(err) {
    Swal.fire({
        icon: "error",
        title: "Error adding",
        text: "Something went wrong!",
        footer: '<a href="#">Why do I have this issue?</a>'
    });
    console.log(err);
}
