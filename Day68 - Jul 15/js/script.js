let wordToGuess = "";
const maxAttempts = 6;
let attempts = 0;
let currentGuess = "";
let words = [];

document.addEventListener("DOMContentLoaded", () => {
  const darkModeToggle = document.getElementById("darkModeToggle");
  const darkModeIcon = document.getElementById("darkModeIcon");
  const body = document.body;

  // Check if dark mode is enabled from previous session (optional)
  const isDarkMode = localStorage.getItem("darkMode") === "enabled";
  if (isDarkMode) {
    enableDarkMode();
  }

  darkModeToggle.addEventListener("click", () => {
    if (body.classList.contains("dark-mode")) {
      disableDarkMode();
    } else {
      enableDarkMode();
    }
  });

  function enableDarkMode() {
    body.classList.add("dark-mode");
    darkModeIcon.classList.remove("fa-moon");
    darkModeIcon.classList.add("fa-sun");
    localStorage.setItem("darkMode", "enabled");
    document.getElementById("mode-text").innerText = "Light Mode";
  }

  function disableDarkMode() {
    body.classList.remove("dark-mode");
    darkModeIcon.classList.remove("fa-sun");
    darkModeIcon.classList.add("fa-moon");
    localStorage.removeItem("darkMode");
    document.getElementById("mode-text").innerText = "Dark Mode";
  }
  fetch("../assets/data/words.json")
    .then((response) => response.json())
    .then((data) => {
      words = data.words;
      wordToGuess = words[Math.floor(Math.random() * words.length)];
      // wordToGuess = "happy"; // Testing
      createBoard();
      createKeyboard();
      document.addEventListener("keydown", handlePhysicalKeyPress);
    })
    .catch((error) => console.error("Error fetching words:", error));

  // Add event listener for modal hidden
  $("#resultModal").on("hidden.bs.modal", resetGame);
});

function createBoard() {
  const board = document.getElementById("game-board");
  for (let i = 0; i < maxAttempts; i++) {
    const row = document.createElement("div");
    row.className = "row";
    for (let j = 0; j < wordToGuess.length; j++) {
      const cell = document.createElement("div");
      cell.className = "cell";
      row.appendChild(cell);
    }
    board.appendChild(row);
  }
}

function createKeyboard() {
  const keyboard = document.getElementById("keyboard");
  const keys = [
    "QWERTYUIOP".split(""),
    "ASDFGHJKL".split(""),
    ["ENTER", ..."ZXCVBNM".split(""), "DELETE"],
  ];

  keys.forEach((row) => {
    const rowDiv = document.createElement("div");
    rowDiv.className = "key-row";
    row.forEach((key) => {
      const keyElement = document.createElement("div");
      keyElement.className = "key";
      keyElement.textContent = key;
      keyElement.addEventListener("click", () => handleKeyPress(key));
      if (key === "ENTER" || key === "DELETE") {
        keyElement.classList.add("large");
      }
      rowDiv.appendChild(keyElement);
    });
    keyboard.appendChild(rowDiv);
  });
}

function handleKeyPress(key) {
  if (key === "ENTER") {
    submitGuess();
  } else if (key === "DELETE") {
    handleDelete();
  } else {
    if (currentGuess.length < wordToGuess.length) {
      currentGuess += key;
      const row = document.getElementsByClassName("row")[attempts];
      const cell = row.children[currentGuess.length - 1];
      cell.textContent = key;
    }
  }
}

function handleDelete() {
  if (currentGuess.length > 0) {
    const row = document.getElementsByClassName("row")[attempts];
    const cell = row.children[currentGuess.length - 1];
    cell.textContent = "";
    currentGuess = currentGuess.slice(0, -1);
  }
}

function handlePhysicalKeyPress(event) {
  const key = event.key.toUpperCase();
  if (key === "ENTER") {
    submitGuess();
  } else if (key === "BACKSPACE") {
    handleDelete();
  } else if (/^[A-Z]$/.test(key)) {
    handleKeyPress(key);
  }
}

function isWordValid(word) {
  return words.includes(word.toLowerCase());
}

function submitGuess() {
  if (currentGuess.length !== wordToGuess.length) {
    showToast(
      "bg-danger",
      `Your guess must be ${wordToGuess.length} letters long`
    );
    return;
  }

  if (!isWordValid(currentGuess)) {
    showToast("bg-danger", `Not a valid word, champ! Try another word.`);
    return;
  }

  const row = document.getElementsByClassName("row")[attempts];
  const keyboardKeys = document.getElementsByClassName("key");
  for (let i = 0; i < currentGuess.length; i++) {
    const cell = row.children[i];
    const key = Array.from(keyboardKeys).find(
      (k) => k.textContent === currentGuess[i]
    );
    if (currentGuess[i].toLowerCase() === wordToGuess[i].toLowerCase()) {
      cell.classList.add("correct");
      if (key) key.classList.add("correct");
    } else if (wordToGuess.includes(currentGuess[i].toLowerCase())) {
      cell.classList.add("present");
      if (key) key.classList.add("present");
    } else {
      cell.classList.add("absent");
      if (key) key.classList.add("absent");
    }
  }

  attempts++;

  if (currentGuess.toLowerCase() === wordToGuess.toLowerCase()) {
    fetchWordMeaning(wordToGuess).then((meaning) => {
      document.getElementById(
        "resultModalLabel"
      ).innerHTML = `Congratulations! <i class="fa-regular fa-face-laugh-wink"></i>`;
      document.getElementById("modal-header").classList.add("bg-success");
      document.getElementById("modal-body").innerHTML = `<p class='fw-bold'>
                        You guessed the word!
                     </p>
                    ${meaning}`;
      $("#resultModal").modal("show");
    });
  } else if (attempts === maxAttempts) {
    document.getElementById(
      "resultModalLabel"
    ).innerHTML = `Hard Luck Champ! Try Again <i class="fa-regular fa-hand-peace"></i>`;
    document.getElementById("modal-header").classList.add("bg-danger");
    document.getElementById(
      "modal-body"
    ).textContent = `Game Over! The word was ${wordToGuess}`;
    $("#resultModal").modal("show");
  }

  currentGuess = "";
}

function resetGame() {
  attempts = 0;
  currentGuess = "";
  wordToGuess = words[Math.floor(Math.random() * words.length)];
  const rows = document.getElementsByClassName("row");
  for (let row of rows) {
    for (let cell of row.children) {
      cell.textContent = "";
      cell.className = "cell";
    }
  }
  const keys = document.getElementsByClassName("key");
  for (let key of keys) {
    key.className = "key";
    if (key.textContent === "ENTER" || key.textContent === "DELETE") {
      key.classList.add("large");
    }
  }
}

async function fetchWordMeaning(word) {
  const response = await fetch(
    `https://api.dictionaryapi.dev/api/v2/entries/en/${word}`
  );

  if (!response.ok) {
    // If API request fails, redirect to a Google search
    return `Sorry Champ! We couldn't fetch the meaning as of now <i class="fa-regular fa-face-frown"></i>
        , but no worries <a href="https://www.google.com/search?q=${word}+meaning" target="_blank">Click here</a>`;
  }

  const data = await response.json();

  if (data.length > 0 && data[0].meanings.length > 0) {
    // If meaning is found, display it
    const meaning = data[0].meanings[0].definitions[0].definition;
    return `<strong>Meaning: </strong>${meaning}`;
  } else {
    // If no meaning found, redirect to a Google search
    return `Sorry Champ! We couldn't fetch the meaning as of now <i class="fa-regular fa-face-frown"></i>
        , but no worries <a href="https://www.google.com/search?q=${word}+meaning" target="_blank">Click here</a>`;
  }
}

function showToast(classBackground, message) {
  const toastElement = document.getElementById("toastNotification");
  const toast = new bootstrap.Toast(toastElement);

  toastElement.classList.remove("bg-success", "bg-danger", "bg-warning");
  toastElement.classList.add(classBackground);
  const toastBody = toastElement.querySelector(".toast-body");
  if (toastBody) {
    toastBody.innerHTML = message;
  }
  console.log(toast);

  toast.show();
}
