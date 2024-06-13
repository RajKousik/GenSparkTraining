var cancelButton = document.getElementById("cancelButton");
var editButton = document.getElementById("editButton");
var togglePassword = document.getElementById("togglePassword");
var editableFields = document.querySelectorAll(".editable");
var originalValues = {};

// Function to toggle the disabled and readonly properties of editable fields
function toggleEditMode(isEditMode) {
  editableFields.forEach(function (field) {
    field.disabled = !isEditMode;
    field.readOnly = !isEditMode;
    if (isEditMode) {
      originalValues[field.id] = field.value;
    }
  });
}

// Function to restore original values of editable fields
function restoreOriginalValues() {
  editableFields.forEach(function (field) {
    field.value = originalValues[field.id];
    field.disabled = true;
    field.readOnly = true;
  });
  editButton.innerText = "Edit";
  editButton.classList.remove("bg-success");
  editButton.classList.add("bg-info");
  cancelButton.style.display = "none";
}

togglePassword.addEventListener("click", function () {
  var passwordInput = document.getElementById("inputPassword4");
  var icon = this;
  if (passwordInput.type === "password") {
    passwordInput.type = "text";
    icon.classList.remove("fa-eye-slash");
    icon.classList.add("fa-eye");
  } else {
    passwordInput.type = "password";
    icon.classList.remove("fa-eye");
    icon.classList.add("fa-eye-slash");
  }
});

editButton.addEventListener("click", function () {
  var isEditMode = this.innerText === "Edit";

  toggleEditMode(isEditMode);

  if (isEditMode) {
    this.innerText = "Save";
    this.classList.remove("bg-info");
    this.classList.add("bg-success");
    cancelButton.style.display = "inline-block";
  } else {
    var API_SUCCESS = false;
    if (!API_SUCCESS) {
      restoreOriginalValues();
      alert("Failed to save changes. Restored original values.");
    } else {
      editableFields.forEach(function (field) {
        field.disabled = true;
        field.readOnly = true;
      });
      this.innerText = "Edit";
      this.classList.remove("bg-success");
      this.classList.add("bg-info");
      cancelButton.style.display = "none";
    }
  }
});

cancelButton.addEventListener("click", function () {
  restoreOriginalValues();
});
