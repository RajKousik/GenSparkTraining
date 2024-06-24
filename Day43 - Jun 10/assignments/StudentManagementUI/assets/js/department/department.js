document.addEventListener("DOMContentLoaded", async function () {
  if (!checkToken()) {
    return;
  }

  if (window.top === window.self) {
    // If the page is not in an iframe, redirect to the main page or show an error
    window.location.href = "../../../src/pages/admin/index.html";
  }

  const token = getTokenFromLocalStorage();
  if (token) {
    const decodedToken = parseJwt(token);
    if (
      decodedToken &&
      decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    ) {
      const rollNo =
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      await fetchUserDepartment(rollNo);
    } else {
      console.error("Failed to decode token.");
    }
  } else {
    console.error("Token not found in localStorage.");
  }

  fetchDepartments(); // Fetch all departments
});

async function fetchUserDepartment(rollNo) {
  let api_url = `${config.API_URL}/students/id?studentRollNo=${rollNo}`;

  let role = await getUserRole();

  if (role.toLowerCase() !== "student") {
    api_url = `${config.API_URL}/faculty/${rollNo}`;
  }

  fetch(api_url)
    .then((response) => response.json())
    .then((data) => {
      const userDepartment = data.departmentId; // Assuming this is how you retrieve the department from the response
      // Add 'user-department' class to the corresponding department card
      const departmentCards = document.querySelectorAll(".department-card");
      departmentCards.forEach((card) => {
        const departmentId = card
          .querySelector(".card-id")
          .textContent.split(": ")[1]; // Extract department ID from card text
        if (departmentId == userDepartment) {
          card.classList.add("user-department");
        }
      });

      AOS.refresh(); // Refresh AOS animations after dynamically adding elements
    })
    .catch((error) => {
      console.error("Error fetching user department:", error);
    });
}

function fetchDepartments() {
  fetch(`${config.API_URL}/departments`)
    .then((response) => response.json())
    .then((data) => {
      const departmentContainer = document.getElementById(
        "departmentContainer"
      );
      data.forEach((department) => {
        const departmentCard = createDepartmentCard(department);
        departmentContainer.appendChild(departmentCard);
      });
      AOS.refresh(); // Refresh AOS animations after dynamically adding elements
    })
    .catch((error) => {
      console.error("Error fetching departments:", error);
    });
}

function createDepartmentCard(department) {
  const cardDiv = document.createElement("div");
  cardDiv.classList.add("department-card");
  cardDiv.setAttribute("data-aos", "fade-up");

  const cardId = document.createElement("h5");
  cardId.classList.add("card-id");
  cardId.textContent = `Department ID: ${department.deptId}`;

  const cardName = document.createElement("h4");
  cardName.classList.add("card-name");
  cardName.textContent = department.name;

  const headContainer = document.createElement("div");
  headContainer.classList.add("head-container");

  const cardLabel = document.createElement("p");
  cardLabel.classList.add("card-label");
  cardLabel.textContent = "Head:";

  const cardText = document.createElement("p");
  cardText.classList.add("card-text");
  cardText.textContent = department.headId; // Assuming headId is the name of the head. Adjust if necessary.

  headContainer.appendChild(cardLabel);
  headContainer.appendChild(cardText);

  cardDiv.appendChild(cardId);
  cardDiv.appendChild(cardName);
  cardDiv.appendChild(headContainer);

  return cardDiv;
}

function getTokenFromLocalStorage() {
  return localStorage.getItem("token");
}

function parseJwt(token) {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
  } catch (e) {
    console.error("Invalid token", e);
    return null;
  }
}
