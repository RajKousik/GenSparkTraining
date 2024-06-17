AOS.init({ duration: 1000 });

const addAttendanceNav = document.getElementById("add-attendance-nav");
const updateAttendanceNav = document.getElementById("update-attendance-nav");
const deleteAttendanceNav = document.getElementById("delete-attendance-nav");
const viewAllAttendanceNav = document.getElementById("view-all-attendance-nav");

const addAttendanceView = document.getElementById("add-attendance-form");
const updateAttendanceView = document.getElementById("update-attendance-form");
const deleteAttendanceView = document.getElementById("delete-attendance-form");
const viewAllAttendanceView = document.getElementById("view-all-attendance");

addAttendanceNav.addEventListener("click", () => {
  addAttendanceView.classList.remove("d-none");
  updateAttendanceView.classList.add("d-none");
  deleteAttendanceView.classList.add("d-none");
  viewAllAttendanceView.classList.add("d-none");

  addAttendanceNav.classList.add("active");
  updateAttendanceNav.classList.remove("active");
  deleteAttendanceNav.classList.remove("active");
  viewAllAttendanceNav.classList.remove("active");
});

updateAttendanceNav.addEventListener("click", () => {
  addAttendanceView.classList.add("d-none");
  updateAttendanceView.classList.remove("d-none");
  deleteAttendanceView.classList.add("d-none");
  viewAllAttendanceView.classList.add("d-none");

  addAttendanceNav.classList.remove("active");
  updateAttendanceNav.classList.add("active");
  deleteAttendanceNav.classList.remove("active");
  viewAllAttendanceNav.classList.remove("active");
});

deleteAttendanceNav.addEventListener("click", () => {
  addAttendanceView.classList.add("d-none");
  updateAttendanceView.classList.add("d-none");
  deleteAttendanceView.classList.remove("d-none");
  viewAllAttendanceView.classList.add("d-none");

  addAttendanceNav.classList.remove("active");
  updateAttendanceNav.classList.remove("active");
  deleteAttendanceNav.classList.add("active");
  viewAllAttendanceNav.classList.remove("active");
});

viewAllAttendanceNav.addEventListener("click", () => {
  addAttendanceView.classList.add("d-none");
  updateAttendanceView.classList.add("d-none");
  deleteAttendanceView.classList.add("d-none");
  viewAllAttendanceView.classList.remove("d-none");

  addAttendanceNav.classList.remove("active");
  updateAttendanceNav.classList.remove("active");
  deleteAttendanceNav.classList.remove("active");
  viewAllAttendanceNav.classList.add("active");
});

// Function to show modal with dynamic content
function showModal(title, message, isSuccess) {
  var modal = document.getElementById("attendanceModal");
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
  // Add Attendance Form Submission
  var addAttendanceForm = document.getElementById("addAttendanceForm");
  addAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Attendance added successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to add attendance. Please try again later.",
        false
      );
    }
  });

  // Update Attendance Form Submission
  var updateAttendanceForm = document.getElementById("updateAttendanceForm");
  updateAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Attendance updated successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to update attendance. Please try again later.",
        false
      );
    }
  });

  // Delete Attendance Form Submission
  var deleteAttendanceForm = document.getElementById("deleteAttendanceForm");
  deleteAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = false;

    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Attendance deleted successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to delete attendance. Please try again later.",
        false
      );
    }
  });
});

$(document).ready(function () {
  const table = $("#attendanceTable").DataTable({
    // columnDefs: [{ orderable: false, targets: 5 }],
    columns: [null, null, null, null, null],
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
