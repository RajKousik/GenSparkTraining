// import { Input, initMDB } from "mdb-ui-kit";

// initMDB({ Input });

AOS.init({ duration: 1000 });

function registerCourse() {
  let IS_API_CALL_SUCCESS = true;
  hideModal("courseViewModal");

  if (IS_API_CALL_SUCCESS) {
    showModal("responseSuccessModal");
  } else {
    showModal("responseFailureModal");
  }
}

// function searchCourses() {
//   const input = document.getElementById("searchInput");
//   const filter = input.value.toLowerCase();
//   const table = document.getElementById("courseTable");
//   const rows = table.getElementsByTagName("tr");

//   const toggleRowVisibility = (row, isVisible) => {
//     row.style.display = isVisible ? "" : "none";
//   };

//   for (let i = 1; i < rows.length; i++) {
//     const cells = rows[i].getElementsByTagName("td");
//     const courseIdElement = cells[0];
//     const courseNameElement = cells[1];
//     const courseId = courseIdElement.innerText.toLowerCase();
//     const courseName = courseNameElement.innerText.toLowerCase();

//     const isVisible =
//       courseId.indexOf(filter) > -1 || courseName.indexOf(filter) > -1;
//     toggleRowVisibility(rows[i], isVisible);
//   }
// }

function showModal(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

function hideModal(modalId) {
  const updateModalElement = document.getElementById(modalId);
  const updateModal = bootstrap.Modal.getInstance(updateModalElement);
  updateModal.hide();
}

$(document).ready(function () {
  const table = $("#courseTable").DataTable({
    //disable sorting on last column
    columnDefs: [{ orderable: false, targets: 4 }],
    columns: [null, null, null, null, { searchable: false }],
    pagingType: "full_numbers",
    language: {
      //customize pagination prev and next buttons: use arrows instead of words
      paginate: {
        previous: '<span class="fa fa-chevron-left"></span>',
        next: '<span class="fa fa-chevron-right"></span>',
        first: '<span class="fa-solid fa-angles-left"></span>',
        last: '<span class="fa-solid fa-angles-right"></span>',
      },
      //customize number of elements to be displayed
      lengthMenu:
        'Display <select class="form-control input-sm">' +
        '<option value="3">3</option>' +
        '<option value="5">5</option>' +
        '<option value="10">10</option>' +
        '<option value="15">15</option>' +
        '<option value="20">20</option>' +
        '<option value="25">15</option>' +
        '<option value="-1">All</option>' +
        "</select> results",
    },
  });
});
