document.addEventListener("DOMContentLoaded", function () {
  AOS.init({ duration: 1000 });

  const approveBtn = document.getElementById("activeBtn");
  const rejectBtn = document.getElementById("rejectBtn");
  const closeBtn = document.getElementById("closeBtn");

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

  function approveStudent() {
    let is_api_success = false;
    // Simulated API call
    if (is_api_success) {
      hideModal("studentViewModal");
      showModal("Success", "Approved Successfully", true);
    } else {
      hideModal("studentViewModal");
      showModal("Approval Failed!", "Something Went Wrong", false);
    }
  }

  function rejectStudent() {
    let is_api_success = false;
    // Simulated API call
    if (is_api_success) {
      hideModal("studentViewModal");
      showModal("Success", "Rejected Successfully", true);
    } else {
      hideModal("studentViewModal");
      showModal("Rejection Failed!", "Something Went Wrong", false);
    }
  }

  approveBtn.addEventListener("click", approveStudent);
  rejectBtn.addEventListener("click", rejectStudent);
});

$(document).ready(function () {
  const table = $("#studentTable").DataTable({
    columns: [
      null,
      null,
      null,
      null,
      null,
      { orderable: false },
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

  $("#studentViewModal").on("show.bs.modal", function (event) {
    const button = $(event.relatedTarget);
    const row = button.closest("tr");
    const data = table.row(row).data();

    const approveBtn = document.getElementById("activeBtn");
    const rejectBtn = document.getElementById("rejectBtn");
    const closeBtn = document.getElementById("closeBtn");

    $("#studentRollNo").text(data[1]);
    $("#studentName").text(data[2]);
    $("#studentEmail").text(data[3]);
    $("#studentDepartment").text(data[4]);
    // Add additional data fetching if needed

    const statusText = $("<div>").html(data[5]).text();
    $("#studentStatus").text(statusText);

    if ($("#studentStatus").text() === "Active") {
      approveBtn.style.display = "none";
      rejectBtn.style.display = "none";
      closeBtn.style.display = "block";
    } else {
      approveBtn.style.display = "block";
      rejectBtn.style.display = "block";
      closeBtn.style.display = "none";
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
    table.draw();
  });

  table.draw(); // Initial filter
});
