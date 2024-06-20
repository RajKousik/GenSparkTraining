const token = getTokenFromLocalStorage();

if (token == null) {
  var notLoggedInModal = new bootstrap.Modal(
    document.getElementById("notLoggedInModal")
  );
  console.log("notLoggedInModal :>> ", notLoggedInModal);
  notLoggedInModal.show();
  document.getElementById("login-btn").addEventListener("click", function () {
    window.location.href = "../../../src/auth/user-auth.html";
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

function getStudentRollNo() {
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
