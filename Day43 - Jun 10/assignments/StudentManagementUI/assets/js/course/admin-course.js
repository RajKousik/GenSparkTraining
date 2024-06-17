AOS.init({ duration: 1000 });

const addCourseNav = document.getElementById("add-course-nav");
const updateCourseNav = document.getElementById("update-course-nav");
const deleteCourseNav = document.getElementById("delete-course-nav");
const viewAllCoursesNav = document.getElementById("view-all-courses-nav");

const addCourseView = document.getElementById("add-course-form");
const updateCourseView = document.getElementById("update-course-form");
// const deleteCourseView = document.getElementById("delete-course-form");
const viewAllCoursesView = document.getElementById("view-all-courses");

addCourseNav.addEventListener("click", () => {
  addCourseView.classList.remove("d-none");
  updateCourseView.classList.add("d-none");
  //   deleteCourseView.classList.add("d-none");
  viewAllCoursesView.classList.add("d-none");

  addCourseNav.classList.add("active");
  updateCourseNav.classList.remove("active");
  //   deleteCourseNav.classList.remove("active");
  viewAllCoursesNav.classList.remove("active");
});

updateCourseNav.addEventListener("click", () => {
  addCourseView.classList.add("d-none");
  updateCourseView.classList.remove("d-none");
  //   deleteCourseView.classList.add("d-none");
  viewAllCoursesView.classList.add("d-none");

  addCourseNav.classList.remove("active");
  updateCourseNav.classList.add("active");
  //   deleteCourseNav.classList.remove("active");
  viewAllCoursesNav.classList.remove("active");
});

// deleteCourseNav.addEventListener("click", () => {
//   addCourseView.classList.add("d-none");
//   updateCourseView.classList.add("d-none");
//   deleteCourseView.classList.remove("d-none");
//   viewAllCoursesView.classList.add("d-none");

//   addCourseNav.classList.remove("active");
//   updateCourseNav.classList.remove("active");
//   deleteCourseNav.classList.add("active");
//   viewAllCoursesNav.classList.remove("active");
// });

viewAllCoursesNav.addEventListener("click", () => {
  addCourseView.classList.add("d-none");
  updateCourseView.classList.add("d-none");
  //   deleteCourseView.classList.add("d-none");
  viewAllCoursesView.classList.remove("d-none");

  addCourseNav.classList.remove("active");
  updateCourseNav.classList.remove("active");
  //   deleteCourseNav.classList.remove("active");
  viewAllCoursesNav.classList.add("active");
});

// Function to show modal with dynamic content
function showModal(title, message, isSuccess) {
  var modal = document.getElementById("courseModal");
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
  // Add Course Form Submission
  var addCourseForm = document.getElementById("addCourseForm");
  addCourseForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Course added successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to add course. Please try again later.",
        false
      );
    }
  });

  // Update Course Form Submission
  var updateCourseForm = document.getElementById("updateCourseForm");
  updateCourseForm.addEventListener("submit", function (event) {
    event.preventDefault();
    let is_api_success = true;
    // Simulated API call
    if (is_api_success) {
      showModal("Success", "Course updated successfully!", true);
    } else {
      showModal(
        "Error",
        "Failed to update course. Please try again later.",
        false
      );
    }
  });
});

$(document).ready(function () {
  const table = $("#courseTable").DataTable({
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
