AOS.init({ duration: 1000 });

const addGradeNav = document.getElementById("add-grade-nav");
const updateGradeNav = document.getElementById("update-grade-nav");
const deleteGradeNav = document.getElementById("delete-grade-nav");
const viewAllGradesNav = document.getElementById("view-all-grades-nav");

const addGradeView = document.getElementById("add-grade-form");
const updateGradeView = document.getElementById("update-grade-form");
const deleteGradeView = document.getElementById("delete-grade-form");
const viewAllGradesView = document.getElementById("view-all-grades");

addGradeNav.addEventListener("click", () => {
  addGradeView.classList.remove("d-none");
  updateGradeView.classList.add("d-none");
  deleteGradeView.classList.add("d-none");
  viewAllGradesView.classList.add("d-none");

  addGradeNav.classList.add("active");
  updateGradeNav.classList.remove("active");
  deleteGradeNav.classList.remove("active");
  viewAllGradesNav.classList.remove("active");
});

updateGradeNav.addEventListener("click", () => {
  addGradeView.classList.add("d-none");
  updateGradeView.classList.remove("d-none");
  deleteGradeView.classList.add("d-none");
  viewAllGradesView.classList.add("d-none");

  addGradeNav.classList.remove("active");
  updateGradeNav.classList.add("active");
  deleteGradeNav.classList.remove("active");
  viewAllGradesNav.classList.remove("active");
});

deleteGradeNav.addEventListener("click", () => {
  addGradeView.classList.add("d-none");
  updateGradeView.classList.add("d-none");
  deleteGradeView.classList.remove("d-none");
  viewAllGradesView.classList.add("d-none");

  addGradeNav.classList.remove("active");
  updateGradeNav.classList.remove("active");
  deleteGradeNav.classList.add("active");
  viewAllGradesNav.classList.remove("active");
});

viewAllGradesNav.addEventListener("click", () => {
  addGradeView.classList.add("d-none");
  updateGradeView.classList.add("d-none");
  deleteGradeView.classList.add("d-none");
  viewAllGradesView.classList.remove("d-none");

  addGradeNav.classList.remove("active");
  updateGradeNav.classList.remove("active");
  deleteGradeNav.classList.remove("active");
  viewAllGradesNav.classList.add("active");
});

// Function to show modal with dynamic content
function showModal(title, message, isSuccess) {
  var modal = document.getElementById("gradeModal");
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

document.addEventListener("DOMContentLoaded", function () {
  if (!checkToken()) {
    return;
  }
  // Add Grade Form Submission
  var addGradeForm = document.getElementById("addGradeForm");
  addGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Grade added successfully!", true);
    } else {
      showModal("Error", "Failed to add grade. Please try again later.", false);
    }
  });

  // Update Grade Form Submission
  var updateGradeForm = document.getElementById("updateGradeForm");
  updateGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Grade updated successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to update grade. Please try again later.",
        false
      );
    }
  });

  // Delete Grade Form Submission
  var deleteGradeForm = document.getElementById("deleteGradeForm");
  deleteGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = false;

    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Grade deleted successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to delete grade. Please try again later.",
        false
      );
    }
  });
});

//table
$(document).ready(function () {
  // Define custom sorting for grades
  const gradeOrder = {
    O: 1,
    "A+": 2,
    A: 3,
    "B+": 4,
    B: 5,
    C: 6,
    F: 7,
    UA: 8,
    RA: 9,
  };

  jQuery.fn.dataTable.ext.type.order["grade-order-pre"] = function (d) {
    return gradeOrder[d] || 10; // default to a high number for unrecognized grades
  };

  // Initialize the DataTable
  const table = $("#gradeTable").DataTable({
    // Disable sorting on the last column
    columnDefs: [
      { orderable: false, targets: 5 },
      { type: "grade-order", targets: 3 }, // Apply custom sorting to the grade column
    ],
    columns: [null, null, null, null, null, { searchable: false }],
    pagingType: "full_numbers",
    // Set default page length
    pageLength: 10,
    language: {
      // Customize pagination prev and next buttons: use arrows instead of words
      paginate: {
        previous: '<span class="fa fa-chevron-left"></span>',
        next: '<span class="fa fa-chevron-right"></span>',
        first: '<span class="fa-solid fa-angles-left"></span>',
        last: '<span class="fa-solid fa-angles-right"></span>',
      },
      // Customize number of elements to be displayed
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
