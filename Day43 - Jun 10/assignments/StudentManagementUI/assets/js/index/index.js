document.addEventListener("DOMContentLoaded", () => {
  if (!checkToken()) {
    return;
  }

  function handleLogout() {
    // Remove the token from localStorage
    localStorage.removeItem("token");

    // Redirect to the login page
    window.top.location.href = "../../../src/auth/user-auth.html";
  }

  // Attach the logout function to the logout link
  const logoutLink = document.getElementById("logout-link");
  logoutLink.addEventListener("click", function (event) {
    event.preventDefault();
    handleLogout();
  });
  // Function to decode JWT token
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

  // Get token from localStorage
  const token = localStorage.getItem("token");

  if (token) {
    // Parse the JWT token
    const decodedToken = parseJwt(token);

    // Check if the FullName exists in the token
    if (decodedToken && decodedToken.FullName) {
      const fullName = decodedToken.FullName;
      // Update all elements with the class 'username'
      const usernameElements = document.querySelectorAll(".username");
      usernameElements.forEach((element) => {
        element.textContent = fullName;
      });
    }
  }
});

function toggleSideBar() {
  var sidebar = document.getElementById("sidebar");
  var togglerIcon = document.getElementById("navbar-toggler-icon");
  if (
    sidebar.classList.contains("collapsing") &&
    togglerIcon.classList.contains("navbar-toggler-icon")
  ) {
    togglerIcon.classList.remove("navbar-toggler-icon");
    togglerIcon.classList.add("btn-close");
  } else {
    togglerIcon.classList.remove("btn-close");
    togglerIcon.classList.add("navbar-toggler-icon");
  }
}

function checkWindowSize() {
  var sidebar = document.getElementById("sidebar");
  var togglerIcon = document.getElementById("navbar-toggler-icon");

  if (window.innerWidth > 576) {
    sidebar.classList.remove("collapse");
    sidebar.classList.remove("show");
    togglerIcon.classList.remove("btn-close");
    togglerIcon.classList.add("navbar-toggler-icon");
  } else {
    sidebar.classList.add("collapse");
  }
}

function setActiveClass(event) {
  var navItems = document.querySelectorAll(".nav-li-items");
  navItems.forEach(function (item) {
    item.querySelector("a").classList.remove("active");
  });
  event.currentTarget.querySelector("a").classList.add("active");
}

// Add event listeners to nav items
var navItems = document.querySelectorAll(".nav-li-items");
navItems.forEach(function (item) {
  item.addEventListener("click", setActiveClass);
});

window.addEventListener("resize", checkWindowSize);
window.addEventListener("load", checkWindowSize);
