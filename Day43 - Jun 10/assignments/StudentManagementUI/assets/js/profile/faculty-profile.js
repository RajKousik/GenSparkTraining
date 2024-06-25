var cancelButton = document.getElementById("cancelButton");
var editButton = document.getElementById("editButton");
var saveButton = document.getElementById("saveButton");
var editableFields = document.querySelectorAll(".editable");
var originalValues = {};

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

// Function to restore original values of editable fields
function restoreOriginalValues() {
  populateFacultyProfile();
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
  restoreOriginalValues();
  toggleEditMode(true);
});

function toggleEditMode(isDisabled) {
  editableFields.forEach(function (field) {
    field.disabled = isDisabled;
    field.readOnly = isDisabled;
  });
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

function setDateValue(dateString) {
  // Extract the date portion from the given date string
  const date = dateString.split("T")[0];
  return date;
}

// Function to populate departments
function populateDepartments() {
  fetch(`${config.API_URL}/departments`)
    .then((response) => response.json())
    .then((data) => {
      const departmentSelect = document.getElementById("inputDepartment");
      data.forEach((department) => {
        const option = document.createElement("option");
        option.value = department.deptId;
        option.textContent = department.name;
        departmentSelect.appendChild(option);
      });
    })
    .catch((error) => {
      console.error("Error fetching departments:", error);
    });
}

function hideModal(modalId) {
  const updateModalElement = document.getElementById(modalId);
  const updateModal = bootstrap.Modal.getInstance(updateModalElement);
  updateModal.hide();
}

document.addEventListener("DOMContentLoaded", function () {
  if (!checkToken()) {
    return;
  }
  populateDepartments();
  populateFacultyProfile();
  document.getElementById("saveButton").addEventListener("click", handleSave);
});

function handleSave(e) {
  e.preventDefault(); // Prevent the default form submission

  const facultyId = document.getElementById("inputFacultyNo").value.trim();
  const email = document.getElementById("inputEmail").value.trim();
  const name = document.getElementById("inputName").value.trim();
  const dob = document.getElementById("inputDOB").value;
  const gender = document.getElementById("inputGender").value;
  const mobile = document.getElementById("inputMobile").value.trim();
  const address = document.getElementById("inputAddress").value.trim();
  const departmentId = document.getElementById("inputDepartment").value;
  const role = document.getElementById("inputRole").value;
  // Construct the request body
  const requestBody = {
    name: name,
    dob: dob,
    gender: gender,
    mobile: mobile,
    address: address,
  };
  const token = localStorage.getItem("token");

  fetch(`${config.API_URL}/faculty/update/${email}`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
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
}

function populateFacultyProfile() {
  const token = localStorage.getItem("token");
  if (token) {
    const decodedToken = parseJwt(token);
    if (
      decodedToken &&
      decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    ) {
      const facultyId =
        decodedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      fetch(`${config.API_URL}/faculty/${facultyId}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      })
        .then((response) => response.json())
        .then((data) => {
          document.getElementById("inputFacultyNo").value = data.facultyId;
          document.getElementById("inputName").value = data.name;
          document.getElementById("inputEmail").value = data.email;
          document.getElementById("inputGender").value = data.gender;
          document.getElementById("inputAddress").value = data.address;
          const formattedDate = setDateValue(data.dob);
          document.getElementById("inputDOB").value = formattedDate;
          document.getElementById("inputMobile").value = data.mobile;
          document.getElementById("inputDepartment").value = data.departmentId;
          document.getElementById("inputRole").value = data.role;
        })
        .catch((error) => {
          console.error("Error fetching student data:", error);
        });
    }
  }
}
