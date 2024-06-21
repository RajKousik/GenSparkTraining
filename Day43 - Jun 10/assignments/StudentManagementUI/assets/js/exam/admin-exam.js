AOS.init({ duration: 1000 });

const addExamNav = document.getElementById("add-exam-nav");
const updateExamNav = document.getElementById("update-exam-nav");
const deleteExamNav = document.getElementById("delete-exam-nav");
const viewAllExamsNav = document.getElementById("view-all-exams-nav");

const addExamView = document.getElementById("add-exam-form");
const updateExamView = document.getElementById("update-exam-form");
const deleteExamView = document.getElementById("delete-exam-form");
const viewAllExamsView = document.getElementById("view-all-exams");

addExamNav.addEventListener("click", () => {
  addExamView.classList.remove("d-none");
  updateExamView.classList.add("d-none");
  deleteExamView.classList.add("d-none");
  viewAllExamsView.classList.add("d-none");

  addExamNav.classList.add("active");
  updateExamNav.classList.remove("active");
  deleteExamNav.classList.remove("active");
  viewAllExamsNav.classList.remove("active");
});

updateExamNav.addEventListener("click", () => {
  addExamView.classList.add("d-none");
  updateExamView.classList.remove("d-none");
  deleteExamView.classList.add("d-none");
  viewAllExamsView.classList.add("d-none");

  addExamNav.classList.remove("active");
  updateExamNav.classList.add("active");
  deleteExamNav.classList.remove("active");
  viewAllExamsNav.classList.remove("active");
});

deleteExamNav.addEventListener("click", () => {
  addExamView.classList.add("d-none");
  updateExamView.classList.add("d-none");
  deleteExamView.classList.remove("d-none");
  viewAllExamsView.classList.add("d-none");

  addExamNav.classList.remove("active");
  updateExamNav.classList.remove("active");
  deleteExamNav.classList.add("active");
  viewAllExamsNav.classList.remove("active");
});

viewAllExamsNav.addEventListener("click", () => {
  addExamView.classList.add("d-none");
  updateExamView.classList.add("d-none");
  deleteExamView.classList.add("d-none");
  viewAllExamsView.classList.remove("d-none");

  addExamNav.classList.remove("active");
  updateExamNav.classList.remove("active");
  deleteExamNav.classList.remove("active");
  viewAllExamsNav.classList.add("active");
});

// Function to show modal with dynamic content
function showModal(title, message, isSuccess) {
  var modal = document.getElementById("examModal");
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
  // Add Exam Form Submission
  var addExamForm = document.getElementById("addExamForm");
  addExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true; // Simulated API call

    if (is_api_success) {
      showModal("Success", "Exam added successfully!", true);
    } else {
      showModal("Error", "Failed to add exam. Please try again later.", false);
    }
  });

  // Update Exam Form Submission
  var updateExamForm = document.getElementById("updateExamForm");
  updateExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true; // Simulated API call

    if (is_api_success) {
      showModal("Success", "Exam updated successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to update exam. Please try again later.",
        false
      );
    }
  });

  // Delete Exam Form Submission
  var deleteExamForm = document.getElementById("deleteExamForm");
  deleteExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true; // Simulated API call

    if (is_api_success) {
      showModal("Success", "Exam deleted successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to delete exam. Please try again later.",
        false
      );
    }
  });

  // Initialize DataTable for Exams
  const table = $("#examTable").DataTable({
    columns: [
      null,
      null,
      null,
      null,
      null,
      null,
      { searchable: false, orderable: false },
    ],
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
