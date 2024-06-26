var cancelButton = document.getElementById("cancelButton");
var editButton = document.getElementById("editButton");
var saveButton = document.getElementById("saveButton");
var editableFields = document.querySelectorAll(".editable");
var originalValues = {};
var profileForm = document.getElementById("profileForm");

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

// Function to restore original values of editable fields
function restoreOriginalValues() {
  populateStudentProfile();
  editButton.style.display = "inline-block";
  cancelButton.style.display = "none";
  saveButton.style.display = "none";
}

editButton.addEventListener("click", function () {
  toggleEditMode(false);

  cancelButton.style.display = "inline-block";
  saveButton.style.display = "inline-block";
  editButton.style.display = "none";
});

cancelButton.addEventListener("click", function () {
  removeValidations(profileForm);
  restoreOriginalValues();
  toggleEditMode(true);
});

function toggleEditMode(isDisabled) {
  editableFields.forEach(function (field) {
    field.disabled = isDisabled;
    field.readOnly = isDisabled;
  });
}

document.addEventListener("DOMContentLoaded", function () {
  if (!checkToken()) {
    return;
  }
  populateDepartments();
  populateStudentProfile();
  document.getElementById("saveButton").addEventListener("click", handleSave);
});

function populateDepartments() {
  fetch(`${config.API_URL}/departments`)
    .then((response) => response.json())
    .then((data) => {
      const departmentSelect = document.getElementById("inputDepartment");
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

function handleSave(e) {
  e.preventDefault(); // Prevent the default form submission

  const email = document.getElementById("inputEmail4").value.trim();
  const name = document.getElementById("inputName4").value.trim();
  const dob = document.getElementById("inputDOB4").value;
  const gender = document.getElementById("inputGender4").value;
  const mobile = document.getElementById("inputMobile4").value.trim();
  const address = document.getElementById("inputAddress").value.trim();
  const departmentId = document.getElementById("inputDepartment").value;

  // Construct the request body
  const requestBody = {
    name: name,
    dob: dob,
    gender: gender,
    mobile: mobile,
    address: address,
    departmentId: departmentId,
    email: email,
  };

  fetch(`${config.API_URL}/students/update?email=${email}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(requestBody),
  })
    .then(async (response) => {
      if (response.ok) {
        return await response.json();
      } else {
        const data = await response.json();
        throw new Error(data.message || "Failed to Updated");
      }
    })
    .then((data) => {
      if (data) {
        showModal("Success!", "Successfully Updated the information!", true);
        setTimeout(() => {
          parent.postMessage("iframeReloaded", "*");
          populateFacultyProfile();
        }, 2000);
      } else {
        showModal("Failed!", "Something Went wrong!", false);
      }
    })
    .catch((error) => {
      console.error("Error updating profile:", error);
      showModal("Failed!", error.message, false);
    });
  document.getElementById("saveButton").style.display = "none";
  document.getElementById("editButton").style.display = "inline-block";
  document.getElementById("cancelButton").style.display = "none";

  toggleEditMode(true);
  removeValidations(profileForm);
}

// Function to populate student profile
function populateStudentProfile() {
  const token = localStorage.getItem("token");
  if (token) {
    const decodedToken = parseJwt(token);
    if (
      decodedToken &&
      decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    ) {
      const studentRollNo =
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      fetch(`${config.API_URL}/students/id?studentRollNo=${studentRollNo}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      })
        .then((response) => response.json())
        .then((data) => {
          document.getElementById("inputRollNo4").value = data.studentRollNo;
          document.getElementById("inputName4").value = data.name;
          document.getElementById("inputEmail4").value = data.email;
          document.getElementById("inputGender4").value = data.gender;
          document.getElementById("inputAddress").value = data.address;
          const formattedDate = setDateValue(data.dob);
          document.getElementById("inputDOB4").value = formattedDate;
          document.getElementById("inputMobile4").value = data.mobile;
          document.getElementById("inputDepartment").value = data.departmentId;
        })
        .catch((error) => {
          console.error("Error fetching student data:", error);
        });
    }
  }
}

function setDateValue(dateString) {
  // Extract the date portion from the given date string
  const date = dateString.split("T")[0];
  return date;
}

function showModal(title, message, isSuccess) {
  var modal = document.getElementById("responseModal");
  var modalTitle = modal.querySelector(".modal-title");
  var modalBody = modal.querySelector(".modal-body");
  var modalHeader = modal.querySelector(".modal-header");

  modalTitle.textContent = title;
  modalBody.textContent = message;

  // Change modal color based on success or failure
  if (isSuccess) {
    modalHeader.classList.remove("bg-danger");
    modalHeader.classList.add("bg-success");
  } else {
    modalHeader.classList.remove("bg-success");
    modalHeader.classList.add("bg-danger");
  }

  var modalInstance = new bootstrap.Modal(modal);
  modalInstance.show();
}

function hideModal(modalId) {
  const updateModalElement = document.getElementById(modalId);
  const updateModal = bootstrap.Modal.getInstance(updateModalElement);
  updateModal.hide();
}
