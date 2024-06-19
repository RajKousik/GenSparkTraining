var cancelButton = document.getElementById("cancelButton");
var editButton = document.getElementById("editButton");
var saveButton = document.getElementById("saveButton");
// var togglePassword = document.getElementById("togglePassword");
var editableFields = document.querySelectorAll(".editable");
var originalValues = {};

// Function to toggle the disabled and readonly properties of editable fields
// function toggleEditMode(isEditMode) {
//   editableFields.forEach(function (field) {
//     field.disabled = !isEditMode;
//     field.readOnly = !isEditMode;
//     if (isEditMode) {
//       originalValues[field.id] = field.value;
//     }
//   });
// }

// Function to restore original values of editable fields
function restoreOriginalValues() {
  populateStudentProfile();

  editButton.style.display = "inline-block";
  cancelButton.style.display = "none";
  saveButton.style.display = "none";
}

// togglePassword.addEventListener("click", function () {
//   var passwordInput = document.getElementById("inputPassword4");
//   var icon = this;
//   if (passwordInput.type === "password") {
//     passwordInput.type = "text";
//     icon.classList.remove("fa-eye-slash");
//     icon.classList.add("fa-eye");
//   } else {
//     passwordInput.type = "password";
//     icon.classList.remove("fa-eye");
//     icon.classList.add("fa-eye-slash");
//   }
// });

editButton.addEventListener("click", function () {
  toggleEditMode(false);

  cancelButton.style.display = "inline-block";
  saveButton.style.display = "inline-block";
  editButton.style.display = "none";
});

cancelButton.addEventListener("click", function () {
  restoreOriginalValues();
  toggleEditMode(true);
});

function toggleEditMode(isDisabled) {
  editableFields.forEach(function (field) {
    field.disabled = isDisabled;
    field.readOnly = isDisabled;
  });
}
