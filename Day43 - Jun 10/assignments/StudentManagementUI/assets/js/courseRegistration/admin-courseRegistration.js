AOS.init({ duration: 1000 });

const approveBtn = document.getElementById("approveBtn");
const rejectBtn = document.getElementById("rejectBtn");

function showModal(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

function showModal(title, message, isSuccess) {
  var modal = document.getElementById("registrationResponseModal");
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

function approveCourse() {
  const let_api_success = true; // Simulating API success
  if (let_api_success) {
    hideModal("courseRegistrationViewModal");
    showModal("Course Approved", "The course has been approved.", true);
  } else {
    hideModal("courseRegistrationViewModal");
    showModal("Error", "There was an error processing your request.", false);
  }
}

function rejectCourse() {
  const let_api_success = false; // Simulating API success

  if (let_api_success) {
    hideModal("courseRegistrationViewModal");
    showModal("Course Rejected", "The course has been rejected.", true);
  } else {
    hideModal("courseRegistrationViewModal");
    showModal("Error", "There was an error processing your request.", false);
  }
}

approveBtn.addEventListener("click", approveCourse);
rejectBtn.addEventListener("click", rejectCourse);

$(document).ready(function () {
  const table = $("#courseRegistrationTable").DataTable({
    // Disable sorting on the last column
    columnDefs: [{ orderable: false, targets: 5 }],
    pagingType: "full_numbers",
    columns: [null, null, null, null, null, { searchable: false }],
    language: {
      // Customize pagination prev and next buttons: use arrows instead of words

      paginate: {
        previous: '<span class="fa fa-chevron-left"></span>',
        next: '<span class="fa fa-chevron-right"></span>',
        first: '<span class="fa-solid fa-angles-left"></span>',
        last: '<span class="fa-solid fa-angles-right"></span>',
      },
      pageLength: 5,
      // Customize number of elements to be displayed
      lengthMenu:
        'Display <select class="form-control input-sm">' +
        '<option value="3">3</option>' +
        '<option value="5" selected>5</option>' + // Default selected value
        '<option value="10">10</option>' +
        '<option value="15">15</option>' +
        '<option value="20">20</option>' +
        '<option value="25">25</option>' +
        '<option value="-1">All</option>' +
        "</select> results",
    },
  });

  // Custom filter function for the table
  $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
    const filterApproved = $("#filterApproved").is(":checked");
    const filterRejected = $("#filterRejected").is(":checked");
    const filterPending = $("#filterPending").is(":checked");
    const status = data[4].toLowerCase();

    if (
      (filterApproved && status.includes("approved")) ||
      (filterRejected && status.includes("rejected")) ||
      (filterPending && status.includes("pending"))
    ) {
      return true;
    } else if (!filterApproved && !filterRejected && !filterPending) {
      // Show all rows if no filter is selected
      return true;
    }
    return false;
  });

  // Function to trigger the filtering
  function filterTable() {
    table.draw();
  }

  // Attach change event listeners to the filter checkboxes
  $("#filterApproved, #filterRejected, #filterPending").on(
    "change",
    function () {
      filterTable();
    }
  );

  // Initial filter application
  filterTable();
});
