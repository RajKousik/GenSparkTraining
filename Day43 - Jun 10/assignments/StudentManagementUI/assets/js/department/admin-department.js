AOS.init({ duration: 1000 });

const addDepartmentNav = document.getElementById("add-department-nav");
const updateDepartmentNav = document.getElementById("update-department-nav");
const viewAllDepartmentsNav = document.getElementById(
  "view-all-departments-nav"
);

const addDepartmentView = document.getElementById("add-department-form");
const updateDepartmentView = document.getElementById("update-department-form");
const viewAllDepartmentsView = document.getElementById("view-all-departments");

addDepartmentNav.addEventListener("click", () => {
  addDepartmentView.classList.remove("d-none");
  updateDepartmentView.classList.add("d-none");
  viewAllDepartmentsView.classList.add("d-none");

  addDepartmentNav.classList.add("active");
  updateDepartmentNav.classList.remove("active");
  viewAllDepartmentsNav.classList.remove("active");
});

updateDepartmentNav.addEventListener("click", () => {
  addDepartmentView.classList.add("d-none");
  updateDepartmentView.classList.remove("d-none");
  viewAllDepartmentsView.classList.add("d-none");

  addDepartmentNav.classList.remove("active");
  updateDepartmentNav.classList.add("active");
  viewAllDepartmentsNav.classList.remove("active");
});

viewAllDepartmentsNav.addEventListener("click", () => {
  addDepartmentView.classList.add("d-none");
  updateDepartmentView.classList.add("d-none");
  viewAllDepartmentsView.classList.remove("d-none");

  addDepartmentNav.classList.remove("active");
  updateDepartmentNav.classList.remove("active");
  viewAllDepartmentsNav.classList.add("active");
});

// Function to show modal with dynamic content
function showModal(title, message, isSuccess) {
  var modal = document.getElementById("departmentModal");
  var modalTitle = modal.querySelector(".modal-title");
  var modalBody = modal.querySelector(".modal-body");
  var modalHeader = modal.querySelector(".modal-header");

  modalTitle.textContent = title;
  modalBody.textContent = message;

  console.log("object okay");
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

document.addEventListener("DOMContentLoaded", function () {
  // Add Department Form Submission
  var addDepartmentForm = document.getElementById("addDepartmentForm");
  addDepartmentForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Department added successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to add department. Please try again later.",
        false
      );
    }
  });

  // Update Department Form Submission
  var updateDepartmentForm = document.getElementById("updateDepartmentForm");
  updateDepartmentForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Department updated successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to update department. Please try again later.",
        false
      );
    }
  });
});

$(document).ready(function () {
  const table = $("#departmentTable").DataTable({
    columns: [null, null, null],
    pagingType: "full_numbers",
    language: {
      paginate: {
        previous: '<span class="fa fa-chevron-left"></span>',
        next: '<span class="fa fa-chevron-right"></span>',
        first: '<span class="fa-solid fa-angles-left"></span>',
        last: '<span class="fa-solid fa-angles-right"></span>',
      },
      lengthMenu:
        'Display <select class="form-control input-sm">' +
        '<option value="3">3</option>' +
        '<option value="5">5</option>' +
        '<option value="10">10</option>' +
        '<option value="15">15</option>' +
        '<option value="20">20</option>' +
        '<option value="25">25</option>' +
        '<option value="-1">All</option>' +
        "</select> results",
    },
  });
});
