$(document).ready(function () {
    // Show a random question when the page loads
    showNextQuestion();
});


const questions = [
    {
        content: "Who wrote the novel 'Agnon'?",
        answers: ["Shmuel Yosef Agnon", "Avraham Burg", "Nathan Zach", "Amos Oz"],
        correctAnswerIndex: 0, // Shmuel Yosef Agnon
        indexQuestion: 0
    },
    {
        content: "Which books were written by Amos Oz?",
        answers: ["Elsewhere", "Family Story", "The History of the Garden of Eden", "Once Upon a Time"],
        correctAnswerIndex: 0, // Elsewhere
        indexQuestion: 1
    },
    {
        content: "Who is the author of 'The History of the Garden of Eden'?",
        answers: ["Shlomo Avineri", "Nathan Zach", "Yossi Sarid", "A. B. Yehoshua"],
        correctAnswerIndex: 3, // A. B. Yehoshua
        indexQuestion: 2
    },
    {
        content: "Which book was written by Sami Michael?",
        answers: ["A Story of Shuki", "The Messenger", "Someone on the Road", "The Hunters"],
        correctAnswerIndex: 0, // A Story of Shuki
        indexQuestion: 3
    },
    {
        content: "Who wrote the book 'The Lost Year'?",
        answers: ["Uri Orlev", "Yehudit Katzir", "Yuval Noah Harari", "Mia Dagan"],
        correctAnswerIndex: 1, // Yehudit Katzir
        indexQuestion: 4
    },
    {
        content: "Which book is considered a gem of Israeli literature and was written by Isaac Shemi?",
        answers: ["Pandora's Box", "The Fall of the Gods", "The Fall of the Light", "The History of the Garment"],
        correctAnswerIndex: 0, // Pandora's Box
        indexQuestion: 5
    },
    {
        content: "Who is the author of 'A Trumpet in the Wadi'?",
        answers: ["Amos Oz","Sami Michael", "David Grossman", "A. B. Yehoshua", "Amos Oz"],
        correctAnswerIndex: 1, // Sami Michael
        indexQuestion: 6
    },
    {
        content: "Which book was written by David Grossman?",
        answers: ["Scenes from Married Life", "Another Man", "Night of the Knife", "The Music of the Garden of Eden"],
        correctAnswerIndex: 0, // Scenes from Married Life
        indexQuestion: 7
    },
    {
        content: "Who wrote the book 'Dear Sister'?",
        answers: ["Yossi Sarid", "Uri Orlev", "Sami Michael", "Yehudit Katzir"],
        correctAnswerIndex: 1, // Uri Orlev
        indexQuestion: 8
    },
    {
        content: "Which book was written by Yuval Noah Harari?",
        answers: ["A Brief History of Time", "A Free Man", "An Innocent Man", "Sapiens: A Brief History of Humankind"],
        correctAnswerIndex: 3, // Sapiens: A Brief History of Humankind
        indexQuestion: 9
    },
];

let currentLevel = 1;
let points = 0;
let previousQuestionIndexes = [];
let currentQuestion;

function playAgain() {
    currentLevel = 1;
    points = 0;
    previousQuestionIndexes = [];
    showNextQuestion();
}

function getRandomQuestion() {
    const remainingQuestions = questions.filter((_, index) => !previousQuestionIndexes.includes(index));
    const randomQuestion = remainingQuestions[Math.floor(Math.random() * remainingQuestions.length)];
    console.log(randomQuestion);
    return randomQuestion;
}


function showNextQuestion() {
    const questionContainer = document.getElementById("question-container");
    const nextQuestion = getRandomQuestion();
    currentQuestion = nextQuestion;

    // Clear the previous content
    questionContainer.innerHTML = `
        <h2>Level ${currentLevel}: ${nextQuestion.content}</h2>
        <ul>
            <li><button data-answer-index="0">${nextQuestion.answers[0]}</button></li>
            <li><button data-answer-index="1">${nextQuestion.answers[1]}</button></li>
            <li><button data-answer-index="2">${nextQuestion.answers[2]}</button></li>
            <li><button data-answer-index="3">${nextQuestion.answers[3]}</button></li>
        </ul>
    `;

    // Add event listeners to buttons
    const buttons = document.querySelectorAll("#question-container button");
    buttons.forEach(button => {
        button.addEventListener("click", checkAnswer);
        button.disabled = false; // Re-enable buttons
    });
}

function checkAnswer(event) {
    const selectedAnswerIndex = parseInt(event.target.dataset.answerIndex, 10);
    const buttons = document.querySelectorAll("#question-container button");

    if (selectedAnswerIndex === currentQuestion.correctAnswerIndex) {
        event.target.classList.add("correct");
        points++;
    } else {
        event.target.classList.add("incorrect");
        buttons[currentQuestion.correctAnswerIndex].classList.add("correct");
    }

    // Disable buttons temporarily to prevent multiple clicks
    buttons.forEach(button => button.disabled = true);

    setTimeout(() => {
        if (currentLevel < 4) {
            previousQuestionIndexes.push(currentQuestion.indexQuestion);
            currentLevel++;
            showNextQuestion();
        } else {
            showResult();
        }
    }, 2000); // 2 seconds delay to show the next question or result
}



function showResult() {
    const questionContainer = document.getElementById("question-container");
    questionContainer.innerHTML = `
        <h2>Congratulations! You have completed the quiz.</h2>
        <p>Total Points: ${points}</p>
        <button onclick="playAgain()">Play Again</button>
      `;
}

