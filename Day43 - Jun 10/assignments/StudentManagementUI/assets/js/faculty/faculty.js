document.addEventListener("DOMContentLoaded", function () {
  AOS.init({ duration: 1000 });

  const approveFacultyBtn = document.getElementById("activeBtn");
  const rejectFacultyBtn = document.getElementById("rejectBtn");
  const closeFacultyBtn = document.getElementById("closeBtn");

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

  function approveFaculty() {
    let is_api_success = false;
    // Simulated API call
    if (is_api_success) {
      hideModal("facultyViewModal");
      showModal("Success", "Approved Successfully", true);
    } else {
      hideModal("facultyViewModal");
      showModal("Approval Failed!", "Something Went Wrong", false);
    }
  }

  function rejectFaculty() {
    let is_api_success = false;
    // Simulated API call
    if (is_api_success) {
      hideModal("facultyViewModal");
      showModal("Success", "Rejected Successfully", true);
    } else {
      hideModal("facultyViewModal");
      showModal("Rejection Failed!", "Something Went Wrong", false);
    }
  }

  approveFacultyBtn.addEventListener("click", approveFaculty);
  rejectFacultyBtn.addEventListener("click", rejectFaculty);
});

$(document).ready(function () {
  const facultyTable = $("#facultyTable").DataTable({
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

  $("#facultyViewModal").on("show.bs.modal", function (event) {
    const button = $(event.relatedTarget);
    const row = button.closest("tr");
    const data = facultyTable.row(row).data();

    const approveFacultyBtn = document.getElementById("activeBtn");
    const rejectFacultyBtn = document.getElementById("rejectBtn");
    const closeFacultyBtn = document.getElementById("closeBtn");

    $("#facultyId").text(data[1]);
    $("#facultyName").text(data[2]);
    $("#facultyEmail").text(data[3]);
    $("#facultyDepartment").text(data[4]);
    // Add additional data fetching if needed

    const statusText = $("<div>").html(data[5]).text();
    $("#facultyStatus").text(statusText);

    if ($("#facultyStatus").text() === "Active") {
      approveFacultyBtn.style.display = "none";
      rejectFacultyBtn.style.display = "none";
      closeFacultyBtn.style.display = "block";
    } else {
      approveFacultyBtn.style.display = "block";
      rejectFacultyBtn.style.display = "block";
      closeFacultyBtn.style.display = "none";
    }
  });

  $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
    const filterActive = $("#filterActive").is(":checked");
    const filterInactive = $("#filterInactive").is(":checked");
    const status = data[5].toLowerCase();

    if (
      (filterActive && status == "active") ||
      (filterInactive && status.includes("inactive"))
    ) {
      return true;
    }
    return false;
  });

  $("#filterActive, #filterInactive").on("change", function () {
    facultyTable.draw();
  });

  facultyTable.draw(); // Initial filter
});
