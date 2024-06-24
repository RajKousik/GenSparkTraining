// Global variables for commonly used elements
const roleSelect = document.getElementById("roleSelect");
const signInForm = document.getElementById("sign-in-form");
const signUpForm = document.getElementById("sign-up-form");
const errorMessageSignInDiv = document.getElementById("errorSignInMessage");
const errorSignUpMessageDiv = document.getElementById("errorSignUpMessage");

// Event listeners to switch between sign-in and sign-up modes
const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener("click", () => {
  clearErrorMessage();
  signInForm.reset();
  container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
  clearErrorMessage();
  signUpForm.reset();
  container.classList.remove("sign-up-mode");
});

// Initial call to set up the form correctly
handleRoleChange();

document.addEventListener("DOMContentLoaded", function () {
  populateDepartments();
});

function populateDepartments() {
  fetch(`${config.API_URL}/departments`)
    .then((response) => response.json())
    .then((data) => {
      const departmentSelect = document.getElementById("department");
      data.forEach((department) => {
        if (department.name.toLowerCase() !== "admin") {
          const option = document.createElement("option");
          option.value = department.deptId;
          option.textContent = department.name;
          departmentSelect.appendChild(option);
        }
      });
    })
    .catch((error) => {
      console.error("Error fetching departments:", error);
    });
}

// Event listener to handle form submission for login
signInForm.addEventListener("submit", handleLoginFormSubmission);
// Event listener to handle form submission for register
// signUpForm.addEventListener("submit", handleRegisterFormSubmission);
document
  .getElementById("submitSignUpForm")
  .addEventListener("click", handleRegisterFormSubmission);
// Event listener for role dropdown change
roleSelect.addEventListener("change", handleRoleChange);

// Common function to handle role change
function handleRoleChange() {
  const role = roleSelect.value;
  const departmentField = document.getElementById("departmentField");
  const cityField = document.getElementById("cityField");
  if (role === "admin") {
    departmentField.required = false;
    departmentField.style.display = "none";
    cityField.classList.remove("col-md-6", "col-sm-6");
    cityField.classList.add("col-md-12", "col-sm-12");
  } else {
    departmentField.style.display = "block";
    cityField.classList.remove("col-md-12", "col-sm-12");
    cityField.classList.add("col-md-6", "col-sm-6");
  }
}

// Function to validate login form
function validateLoginForm(email, password) {
  if (email === "") {
    displayErrorMessage("Enter your email address!", errorMessageSignInDiv);
    return false;
  }
  if (!isValidEmail(email)) {
    displayErrorMessage("Invalid email format.", errorMessageSignInDiv);
    return false;
  }
  if (password === "") {
    displayErrorMessage("Enter your password!", errorMessageSignInDiv);
    return false;
  }
  return true;
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

// Function to handle form submission for register
function handleRegisterFormSubmission(e) {
  e.preventDefault(); // Prevent default form submission
  clearErrorMessage();

  const username = document.getElementById("username").value.trim();
  const email = document.getElementById("email").value.trim();
  const mobile = document.getElementById("mobile").value.trim();
  const dob = document.getElementById("dob").value.trim();
  const gender = document.getElementById("gender").value;
  const role = roleSelect.value;
  const city = document.getElementById("city").value.trim();
  const department = document.getElementById("department").value;
  const password = document.getElementById("signUpPassword").value;
  const confirmPassword = document.getElementById("signUpRePassword").value;
  const termsAgreeInput = document.getElementById("gridCheck").checked;

  if (
    !username ||
    !email ||
    !mobile ||
    !dob ||
    !gender ||
    !role ||
    !city ||
    (role !== "admin" && !department) ||
    !password ||
    !confirmPassword
  ) {
    displayErrorMessage("All fields are required.", errorSignUpMessageDiv);
    return;
  }
  if (!termsAgreeInput) {
    displayErrorMessage("All fields are required.", errorSignUpMessageDiv);
    return;
  }
  if (!isValidEmail(email)) {
    displayErrorMessage("Invalid email format.", errorSignUpMessageDiv);
    return;
  }
  if (!isValidPassword(password)) {
    displayErrorMessage(
      "Password must be at least 8 characters long and include a mix of uppercase, lowercase, digits, and special characters.",
      errorSignUpMessageDiv
    );
    return;
  }
  if (mobile.length != 10) {
    displayErrorMessage(
      "Mobile No. Should be of length 10",
      errorSignUpMessageDiv
    );
    return;
  }
  if (password !== confirmPassword) {
    displayErrorMessage("Passwords do not match.", errorSignUpMessageDiv);
    return;
  }

  // displayErrorMessage(
  //   "You have successfully registered!",
  //   errorSignUpMessageDiv,
  //   true
  // );
  let endpoint;
  switch (role.toLowerCase().trim()) {
    case "professor":
      endpoint = "/faculty/prof/register";
      break;
    case "assistant_professor":
      endpoint = "/faculty/assistant-prof/register";
      break;
    case "associate_professor":
      endpoint = "/faculty/associate-prof/register";
      break;
    case "admin":
      endpoint = "/faculty/admin/register";
      break;
    case "student":
      endpoint = "/students/register";
      break;
    default:
      displayErrorMessage("Invalid role selected.", errorSignUpMessageDiv);
      return;
  }
  // Make the API call
  fetch(`${config.API_URLs}${endpoint}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      name: username,
      email: email,
      mobile: mobile,
      dob: dob,
      gender: gender,
      address: city,
      departmentId: role.toLowerCase().trim() !== "admin" ? department : null,
      password: password,
    }),
  })
    .then(async (response) => {
      if (response.ok) {
        displayErrorMessage(
          "Successfully Registered....!",
          errorMessageSignInDiv,
          true
        );
        return response.json();
      } else {
        const data = await response.json();
        throw new Error(data.message || "Failed to Register");
      }
    })
    .then((data) => {
      if (data) {
        displayErrorMessage(
          `<div class="spinner-border spinner-border-sm" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
          Successfully registered! Redirecting to login page...`,
          errorSignUpMessageDiv,
          true
        );
        const registerSubmitBtn = document.querySelector("#submitSignUpForm");
        registerSubmitBtn.setAttribute("disabled", true);
        sign_in_btn.setAttribute("disabled", true);
        setTimeout(() => {
          window.location.href = "../../../user-auth.html";
        }, 5000);
      } else {
        displayErrorMessage(
          data.message || "Registration failed.",
          errorSignUpMessageDiv
        );
      }
    })
    .catch((error) => {
      // console.error("Error during registration:", error);
      displayErrorMessage(error.message, errorSignUpMessageDiv);
    });
}

// Function to handle form submission for login
function handleLoginFormSubmission(e) {
  e.preventDefault(); // Prevent default form submission
  clearErrorMessage();
  const floatingInput = document.getElementById("floatingEmail");
  const floatingPassword = document.getElementById("floatingPassword");
  const floatingRole = document.getElementById("floatingRole");
  const email = floatingInput.value.trim();
  const password = floatingPassword.value;
  const role = floatingRole.value;

  if (!validateLoginForm(email, password)) {
    return;
  }

  fetch(`${config.API_URL}/${role}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      password: password,
    }),
  })
    .then(async (response) => {
      if (response.ok) {
        displayErrorMessage(
          `<div class="spinner-border spinner-border-sm" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
          Successfully logged in! Redirecting....`,
          errorMessageSignInDiv,
          true
        );
        return response.json();
      } else {
        const data = await response.json();
        throw new Error(data.message || "Failed to login");
      }
    })
    .then(async (data) => {
      if (data.token) {
        const loginSubmitBtn = document.querySelector("#login-submit");
        loginSubmitBtn.setAttribute("disabled", true);
        sign_up_btn.setAttribute("disabled", true);

        localStorage.setItem("token", data.token);

        setTimeout(() => {
          if (data.role.toLowerCase() === "student") {
            window.location.href = "../../../src/pages/student/index.html";
          } else if (data.role.toLowerCase() === "admin") {
            window.location.href = "../../../src/pages/admin/index.html";
          } else {
            window.location.href = "../../../src/pages/faculty/index.html";
          }
          loginSubmitBtn.setAttribute("disabled", false);
          sign_up_btn.setAttribute("disabled", false);
          signInForm.reset();
        }, 3000);
      } else {
        // displayErrorMessage(
        //   "Something Went Wrong! Please try Again Later",
        //   errorMessageSignInDiv
        // );
        newToast("bg-red", "Something Went Wrong! Please try Again Later");
      }
    })
    .catch((error) => {
      newToast("bg-danger", error.message);
      displayErrorMessage(error.message, errorMessageSignInDiv);
    });
}

function togglePasswordVisibility(event) {
  const icon = event.target;
  const input =
    this.previousElementSibling.tagName === "INPUT"
      ? this.previousElementSibling
      : this.previousElementSibling.querySelector("input");
  if (input.type === "password") {
    input.type = "text";
    icon.classList.remove("fa-eye-slash");
    icon.classList.add("fa-eye");
  } else {
    input.type = "password";
    icon.classList.remove("fa-eye");
    icon.classList.add("fa-eye-slash");
  }
}

// Add event listeners to all password icons
document.querySelectorAll(".password-icon").forEach((icon) => {
  icon.addEventListener("click", togglePasswordVisibility);
});

// Function to display error message
function displayErrorMessage(message, errorMessageDiv, isSuccess = false) {
  if (errorMessageDiv) {
    errorMessageDiv.innerHTML = message;
    errorMessageDiv.classList.remove("d-none");
    if (isSuccess) {
      errorMessageDiv.classList.remove("text-danger");
      errorMessageDiv.classList.add("text-success", "d-block");
    } else {
      errorMessageDiv.classList.remove("text-success");
      errorMessageDiv.classList.add("text-danger", "d-block");
    }
  }
}

function clearErrorMessage() {
  errorMessageSignInDiv.textContent = "";
  errorMessageSignInDiv.classList.remove(
    "text-danger",
    "d-block",
    "text-success"
  );
  errorMessageSignInDiv.classList.add("d-none");

  errorSignUpMessageDiv.textContent = "";
  errorSignUpMessageDiv.classList.remove(
    "text-danger",
    "d-block",
    "text-success"
  );
  errorSignUpMessageDiv.classList.add("d-none");
}

// UTILITY FUNCTIONS

// Function to validate email format
function isValidEmail(email) {
  const emailRegex = /\S+@\S+\.\S+/;
  return emailRegex.test(email);
}

// Function to validate password format
function isValidPassword(password) {
  const passwordRegex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])[A-Za-z\d!@#$%^&*()]{8,}$/;
  return passwordRegex.test(password);
}
