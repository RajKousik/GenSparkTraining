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
