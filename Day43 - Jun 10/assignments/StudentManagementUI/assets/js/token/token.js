// const accessiblePages = [
//   "src/auth/user-auth.html",
//   "src/pages/admin/admin-attendance.html",
//   "src/pages/admin/admin-course.html",
//   "src/pages/admin/admin-courseRegistration.html",
//   "src/pages/admin/admin-department.html",
//   "src/pages/admin/admin-exam.html",
//   "src/pages/admin/admin-faculty.html",
//   "src/pages/admin/admin-grade.html",
//   "src/pages/admin/admin-students.html",
//   "src/pages/admin/index.html",
//   "src/pages/faculty/faculty-attendance.html",
//   "src/pages/faculty/faculty-grade.html",
//   "src/pages/faculty/faculty-profile.html",
//   "src/pages/faculty/index.html",
//   "src/pages/student/attendance.html",
//   "src/pages/student/course.html",
//   "src/pages/student/courseRegistration.html",
//   "src/pages/student/department.html",
//   "src/pages/student/EWallet.html",
//   "src/pages/student/exam.html",
//   "src/pages/student/grade.html",
//   "src/pages/student/index.html",
//   "src/pages/student/profile.html",
// ];

// window.addEventListener("load", function () {
//   const page = window.location.pathname + window.location.hash;
//   loadContent(page);
// });

// function loadContent(page) {
//
//   if (!accessiblePages.includes(page)) {
//     redirectTo404();
//   }
// }

// function redirectTo404() {
//   window.location.href = "../../../src/auth/error.html";
// }

const token = getTokenFromLocalStorage();

function checkToken() {
  const token = localStorage.getItem("token");
  if (!token) {
    showNotLoggedInModal();
    return false;
  }

  const isTokenExpired =
    Date.now() >= JSON.parse(atob(token.split(".")[1])).exp * 1000;
  if (isTokenExpired) {
    showNotLoggedInModal();
    return false;
  }

  // Optionally, you can add further token validation logic here, such as checking the token against a server endpoint.

  return true;
}

function showNotLoggedInModal() {
  var notLoggedInModal = new bootstrap.Modal(
    document.getElementById("notLoggedInModal")
  );
  notLoggedInModal.show();
  document.getElementById("login-btn").addEventListener("click", function () {
    window.top.location.href = "../../../src/auth/user-auth.html";
  });
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

function getUserId() {
  const token = getTokenFromLocalStorage();

  const decodedToken = parseJwt(token);
  if (
    decodedToken &&
    decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  ) {
    const rollNo =
      decodedToken[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
      ];
    return rollNo;
  } else {
    console.error("Failed to decode token.");
  }
  return null;
}

function getUserRole() {
  const token = getTokenFromLocalStorage();

  const decodedToken = parseJwt(token);
  if (
    decodedToken &&
    decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  ) {
    const role =
      decodedToken[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ];
    return role;
  } else {
    console.error("Failed to decode token.");
  }
  return null;
}

function getUserEmail() {
  const token = getTokenFromLocalStorage();

  const decodedToken = parseJwt(token);
  if (
    decodedToken &&
    decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  ) {
    const email =
      decodedToken[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ];
    return email;
  } else {
    console.error("Failed to decode token.");
  }
  return null;
}
