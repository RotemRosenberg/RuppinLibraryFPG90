const advancedQuestions = [
    {
        question: "Which activity do you find most relaxing?",
        answers: [
            { text: "Painting or sketching", category: "Art" },
            { text: "Cooking a new recipe", category: "Cooking" },
            { text: "Reading about past events", category: "History" },
            { text: "Diving into a fictional world", category: "Fiction" }
        ],
        indexQuestion: 0
    },
    {
        question: "Which of these would you like to explore?",
        answers: [
            { text: "An art museum", category: "Art" },
            { text: "A gourmet restaurant", category: "Cooking" },
            { text: "An ancient ruin", category: "History" },
            { text: "A fantasy novel", category: "Fiction" }
        ],
        indexQuestion: 1
    },
    {
        question: "Which of these topics interests you the most?",
        answers: [
            { text: "Famous artists and their works", category: "Art" },
            { text: "Culinary techniques", category: "Cooking" },
            { text: "Significant historical events", category: "History" },
            { text: "Creative storytelling", category: "Fiction" }
        ],
        indexQuestion: 2
    },
    {
        question: "What type of book would you pick up first?",
        answers: [
            { text: "A beautifully illustrated art book", category: "Art" },
            { text: "A cookbook full of delicious recipes", category: "Cooking" },
            { text: "A biography of a historical figure", category: "History" },
            { text: "A novel with a gripping storyline", category: "Fiction" }
        ],
        indexQuestion: 3
    },
    {
        question: "Which place would you most like to visit?",
        answers: [
            { text: "A famous art gallery", category: "Art" },
            { text: "A culinary school", category: "Cooking" },
            { text: "A historical landmark", category: "History" },
            { text: "A fictional world in a book", category: "Fiction" }
        ],
        indexQuestion: 4
    },
    {
        question: "Which of these hobbies would you enjoy the most?",
        answers: [
            { text: "Learning to paint", category: "Art" },
            { text: "Experimenting with new recipes", category: "Cooking" },
            { text: "Researching historical events", category: "History" },
            { text: "Writing stories", category: "Fiction" }
        ],
        indexQuestion: 5
    },
    {
        question: "If you could spend a day doing anything, what would it be?",
        answers: [
            { text: "Visiting an art exhibition", category: "Art" },
            { text: "Cooking a gourmet meal", category: "Cooking" },
            { text: "Exploring a historical site", category: "History" },
            { text: "Reading a captivating novel", category: "Fiction" }
        ],
        indexQuestion: 6
    },
    {
        question: "Which of these events would you most like to attend?",
        answers: [
            { text: "An art auction", category: "Art" },
            { text: "A cooking class", category: "Cooking" },
            { text: "A historical reenactment", category: "History" },
            { text: "A book reading", category: "Fiction" }
        ],
        indexQuestion: 7
    },
    {
        question: "Which of these would you most enjoy watching?",
        answers: [
            { text: "A documentary about famous artists", category: "Art" },
            { text: "A cooking show", category: "Cooking" },
            { text: "A historical documentary", category: "History" },
            { text: "A film based on a novel", category: "Fiction" }
        ],
        indexQuestion: 8
    },
    {
        question: "What would you prefer to receive as a gift?",
        answers: [
            { text: "A painting kit", category: "Art" },
            { text: "A cookbook", category: "Cooking" },
            { text: "A history book", category: "History" },
            { text: "A bestselling novel", category: "Fiction" }
        ],
        indexQuestion: 9
    }
];

let AcurrentLevel = 1;
let AcategoryScores = {
    Art: 0,
    Cooking: 0,
    History: 0,
    Fiction: 0
};
let ApreviousQuestionIndexes = [];
let AcurrentQuestion;

$(document).ready(function () {
    // Load the first question when the page loads
    AshowNextQuestion();
});

function AplayAgain() {
    AcurrentLevel = 1;
    AcategoryScores = {
        Art: 0,
        Cooking: 0,
        History: 0,
        Fiction: 0
    };
    ApreviousQuestionIndexes = [];
    AshowNextQuestion();
}

function AgetRandomQuestion() {
    // מסנן את השאלות שעדיין לא נשאלו על ידי השחקן
    const remainingQuestions = advancedQuestions.filter((_, index) => !ApreviousQuestionIndexes.includes(index));

    // בוחר שאלה אקראית מתוך השאלות שנותרו
    const randomQuestion = remainingQuestions[Math.floor(Math.random() * remainingQuestions.length)];

    // מציג את השאלה האקראית שנבחרה בקונסול לצורך בדיקה
    console.log(randomQuestion);

    // מחזיר את השאלה האקראית שנבחרה
    return randomQuestion;
}

function AcheckAnswer(event) {
    // מציאת הכפתור שנלחץ
    const selectedButton = event.target;

    // מציאת האינדקס של הכפתור שנבחר ביחס לכפתורים האחרים
    const selectedIndex = Array.from(selectedButton.parentElement.parentElement.children).indexOf(selectedButton.parentElement);

    // מציאת הקטגוריה מהשאלה הנוכחית לפי האינדקס שנבחר
    const selectedCategory = AcurrentQuestion.answers[selectedIndex].category;

    // עדכון הניקוד של הקטגוריה שנבחרה
    AcategoryScores[selectedCategory]++;

    // השבת זמנית את הכפתורים כדי למנוע לחיצות מרובות.
    const buttons = document.querySelectorAll("#advancedQuestion-container button");
    buttons.forEach(button => button.disabled = true);

    setTimeout(() => {
        if (AcurrentLevel < 6) {
            ApreviousQuestionIndexes.push(AcurrentQuestion.indexQuestion);
            AcurrentLevel++;
            AshowNextQuestion();
        } else {
            AshowResult();
        }
    }, 1000); // 2 seconds delay to show the next question or result
}


function AshowNextQuestion() {
    const questionContainer = document.getElementById("advancedQuestion-container");
    const nextQuestion = AgetRandomQuestion();
    AcurrentQuestion = nextQuestion;

    questionContainer.innerHTML = `
        <h2>Level ${AcurrentLevel}: ${nextQuestion.question}</h2>
        <ul>
            <li><button>${nextQuestion.answers[0].text}</button></li>
            <li><button>${nextQuestion.answers[1].text}</button></li>
            <li><button>${nextQuestion.answers[2].text}</button></li>
            <li><button>${nextQuestion.answers[3].text}</button></li>
        </ul>
    `;

    // Add event listeners to buttons
    const buttons = document.querySelectorAll("#advancedQuestion-container button");
    buttons.forEach(button => {
        button.addEventListener("click", AcheckAnswer);
        button.disabled = false; // Re-enable buttons
    });
}

function AshowResult() {
    const questionContainer = document.getElementById("advancedQuestion-container");

    // מציאת הקטגוריה עם הערך הגבוה ביותר
    const highestCategory = Object.keys(AcategoryScores).reduce((a, b) => AcategoryScores[a] > AcategoryScores[b] ? a : b);

    questionContainer.innerHTML = `
        <h2>Congratulations! You have completed the quiz.</h2>
        <p>Your dominant category is: ${highestCategory}</p>
        <button onclick="AplayAgain()">Play Again</button>
      `;
    let api = `https://194.90.158.74/cgroup90/test2/tar1/api/Book/${highestCategory}?type=category`;
    ajaxCall("GET", api, "", PerfectSCBF,  PerfectECBF);
}

function PerfectSCBF(result) {
    RenderPerfectBooks(result);
    console.log(result);
}
function PerfectECBF(err) {
    console.log(err);
}

function RenderPerfectBooks(data) {
    document.getElementById('perfectBook').innerHTML = '';
    document.getElementById('perfectBookTitle').innerHTML = data[0].categories + ' Books'
    const bookContainer = document.getElementById('perfectBook');
    RenderBooks(data, bookContainer);
}