let currentPage = 1;
const quotesPerPage = 5;
let totalPages = 0;
let allQuotes = []; // To store all quotes fetched initially
let currentFilteredQuotes = []; // To store currently filtered quotes
let currentAuthor = ""; // To store the current author being filtered

document.addEventListener("DOMContentLoaded", function () {
  showHome();
  fetchAllQuotes(); // Fetch all quotes initially
});

function showHome() {
  document.getElementById("home").classList.remove("d-none");
  document.getElementById("quotes").classList.add("d-none");
  document.getElementById("homeLink").classList.add("active");
  document.getElementById("quotesLink").classList.remove("active");
}

function showQuotes() {
  document.getElementById("home").classList.add("d-none");
  document.getElementById("quotes").classList.remove("d-none");
  document.getElementById("homeLink").classList.remove("active");
  document.getElementById("quotesLink").classList.add("active");
  fetchAllQuotes();
}

function fetchAllQuotes() {
  fetch("https://dummyjson.com/quotes?limit=1454")
    .then((response) => response.json())
    .then((data) => {
      console.log("data :>> ", data);
      allQuotes = data.quotes; // Store all quotes in allQuotes array
      totalPages = Math.ceil(allQuotes.length / quotesPerPage);
      currentPage = 1; // Reset currentPage
      displayQuotes(allQuotes.slice(0, quotesPerPage)); // Display first page initially
    })
    .catch((error) => console.error("Error fetching quotes:", error));
}

function fetchQuotesByAuthor(authorName) {
  currentAuthor = authorName.toLowerCase().trim();
  currentFilteredQuotes = allQuotes.filter((quote) =>
    quote.author.toLowerCase().includes(currentAuthor)
  );
  totalPages = Math.ceil(currentFilteredQuotes.length / quotesPerPage);
  currentPage = 1; // Reset currentPage
  displayQuotes(currentFilteredQuotes.slice(0, quotesPerPage));
}

function displayQuotes(quotes) {
  const quotesContainer = document.getElementById("quotesContainer");
  quotesContainer.innerHTML = "";

  quotes.forEach((quote) => {
    const quoteCard = document.createElement("div");
    quoteCard.classList.add("col-md-5", "quote-card", "mb-4");
    quoteCard.setAttribute("data-aos", getRandomAnimation());
    quoteCard.innerHTML = `
      <p>${quote.quote}</p>
      <p class="author">- ${quote.author}</p>
    `;
    quotesContainer.appendChild(quoteCard);
  });

  document.getElementById(
    "pageNumber"
  ).innerText = `Page ${currentPage} of ${totalPages}`;
  document.getElementById("prevBtn").disabled = currentPage === 1;
  document.getElementById("nextBtn").disabled = currentPage === totalPages;
  document.getElementById("firstPageBtn").disabled = currentPage === 1;
  document.getElementById("lastPageBtn").disabled = currentPage === totalPages;
}

function sortByAuthorAsc() {
  currentPage = 1;
  if (currentAuthor) {
    currentFilteredQuotes.sort((a, b) =>
      a.author.toLowerCase().localeCompare(b.author.toLowerCase())
    );
    displayQuotes(currentFilteredQuotes.slice(0, quotesPerPage));
  } else {
    allQuotes.sort((a, b) =>
      a.author.toLowerCase().localeCompare(b.author.toLowerCase())
    );
    displayQuotes(allQuotes.slice(0, quotesPerPage));
  }
}

function sortByAuthorDesc() {
  currentPage = 1;
  if (currentAuthor) {
    currentFilteredQuotes.sort((a, b) =>
      b.author.toLowerCase().localeCompare(a.author.toLowerCase())
    );
    displayQuotes(currentFilteredQuotes.slice(0, quotesPerPage));
  } else {
    allQuotes.sort((a, b) =>
      b.author.toLowerCase().localeCompare(a.author.toLowerCase())
    );
    displayQuotes(allQuotes.slice(0, quotesPerPage));
  }
}

function getRandomAnimation() {
  const animations = ["fade-up", "fade-right", "fade-down", "fade-left"];
  const randomIndex = Math.floor(Math.random() * animations.length);
  return animations[randomIndex];
}

function prevPage() {
  if (currentPage > 1) {
    currentPage--;
    if (currentAuthor) {
      paginateFilteredQuotes();
    } else {
      displayQuotes(
        allQuotes.slice(
          (currentPage - 1) * quotesPerPage,
          currentPage * quotesPerPage
        )
      );
    }
    scrollToTop();
  }
}

function nextPage() {
  if (currentPage < totalPages) {
    currentPage++;
    if (currentAuthor) {
      paginateFilteredQuotes();
    } else {
      displayQuotes(
        allQuotes.slice(
          (currentPage - 1) * quotesPerPage,
          currentPage * quotesPerPage
        )
      );
    }
    scrollToTop();
  }
}

function firstPage() {
  if (currentPage !== 1) {
    currentPage = 1;
    if (currentAuthor) {
      paginateFilteredQuotes();
    } else {
      displayQuotes(allQuotes.slice(0, quotesPerPage));
    }
    scrollToTop();
  }
}

function lastPage() {
  if (currentPage !== totalPages) {
    currentPage = totalPages;
    if (currentAuthor) {
      paginateFilteredQuotes();
    } else {
      displayQuotes(
        allQuotes.slice(
          (totalPages - 1) * quotesPerPage,
          totalPages * quotesPerPage
        )
      );
    }
    scrollToTop();
  }
}

function paginateFilteredQuotes() {
  const start = (currentPage - 1) * quotesPerPage;
  const end = start + quotesPerPage;
  displayQuotes(currentFilteredQuotes.slice(start, end));
}

function searchByAuthor() {
  const authorInput = document.getElementById("authorInput").value.trim();
  console.log("authorInput :>> ", authorInput);
  if (authorInput === "") {
    // If input is empty, show all quotes
    fetchAllQuotes();
  } else {
    // Filter quotes by author name
    fetchQuotesByAuthor(authorInput);
  }
}

function scrollToTop() {
  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
}

function fetchRandomQuote() {
  fetch("https://dummyjson.com/quotes/random")
    .then((response) => response.json())
    .then((data) => {
      const randomQuote = data.quote;
      const author = data.author;

      // Update modal content
      const modalTitle = document.getElementById("randomQuoteModalLabel");
      const modalBody = document.getElementById("randomQuoteContent");

      modalTitle.textContent = `Random Quote - ${author}`;
      modalBody.innerHTML = `
        <p>${randomQuote}</p>
        <p class="text-end">- ${author}</p>
      `;

      // Show the modal
      const randomQuoteModal = new bootstrap.Modal(
        document.getElementById("randomQuoteModal")
      );
      randomQuoteModal.show();
    })
    .catch((error) => console.error("Error fetching random quote:", error));
}
