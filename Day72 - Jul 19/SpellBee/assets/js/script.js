document.addEventListener("DOMContentLoaded", () => {
  const letters = ["T", "B", "O", "I", "L", "G", "S"];
  const predefinedWords = ["BOIL", "SOIL", "TOIL", "GILL"];
  const hexLetters = document.querySelectorAll(".hex-letter");
  const wordInput = document.getElementById("word-cursor");
  const foundWordsList = document.getElementById("found-words-list");
  const scoreElement = document.getElementById("score");
  const progressBar = document.getElementById("progress");
  let currentWord = "";
  let score = 0;

  hexLetters.forEach((hex) => {
    hex.parentElement.addEventListener("click", () => {
      currentWord += hex.textContent.toUpperCase();
      updateCursor();
      validateInput();
    });
  });

  document.addEventListener("keydown", (e) => {
    if (e.key.length === 1 && /[a-zA-Z]/.test(e.key)) {
      currentWord += e.key.toUpperCase();
    } else if (e.key === "Backspace") {
      currentWord = currentWord.slice(0, -1);
    }
    updateCursor();
    validateInput();
  });

  document.getElementById("delete").addEventListener("click", () => {
    currentWord = currentWord.slice(0, -1);
    updateCursor();
    validateInput();
  });

  document.getElementById("reshuffle").addEventListener("click", () => {
    const shuffledLetters = letters
      .filter((l) => l !== "I")
      .sort(() => Math.random() - 0.5);
    shuffledLetters.splice(3, 0, "I");
    hexLetters.forEach((hex, index) => {
      hex.textContent = shuffledLetters[index];
    });
  });

  document.getElementById("enter").addEventListener("click", () => {
    if (predefinedWords.includes(currentWord)) {
      if (!isWordAlreadyFound(currentWord)) {
        const wordItem = document.createElement("li");
        wordItem.textContent = currentWord;
        foundWordsList.appendChild(wordItem);
        score += currentWord.length;
        scoreElement.textContent = score;
        currentWord = "";
        updateCursor();
        updateProgress();
      } else {
        newToast("bg-danger", "This word has already been found.");
        currentWord = "";
        updateCursor();
      }
    } else {
      newToast("bg-danger", "This word is not in the list.");
      currentWord = "";
      updateCursor();
    }
  });

  // Function to update the cursor
  function updateCursor() {
    wordInput.innerHTML =
      currentWord + `<span id="cursor" style="opacity: 0">|</span>`;
  }

  setInterval(() => {
    const cursor = document.getElementById("cursor");
    cursor.style.opacity = cursor.style.opacity === "0" ? "1" : "0";
  }, 500);

  function validateInput() {
    let valid = true;
    for (let char of currentWord) {
      if (!letters.includes(char)) {
        valid = false;
        break;
      }
    }
    wordInput.style.color = valid ? "black" : "red";
  }

  function isWordAlreadyFound(word) {
    const foundWords = Array.from(foundWordsList.children).map(
      (li) => li.textContent
    );
    return foundWords.includes(word);
  }

  function updateProgress() {
    const totalWords = predefinedWords.length;
    const foundWords = foundWordsList.children.length;
    const progressPercent = (foundWords / totalWords) * 100;
    progressBar.style.width = progressPercent + "%";
  }

  function newToast(classBackground, message) {
    const toastNotification = new bootstrap.Toast(
      document.getElementById("toastNotification")
    );
    var toast = document.getElementById("toastNotification");
    toast.className = "toast align-items-center text-white border-0";
    toast.classList.add(`${classBackground}`);
    var toastBody = document.querySelector(".toast-body");
    if (toastBody) {
      toastBody.innerHTML = `${message}`;
    }
    toastNotification.show();
  }
});
