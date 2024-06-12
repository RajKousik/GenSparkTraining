AOS.init({ duration: 1000 });

function DeleteCourseRegistration() {
  let IS_API_CALL_SUCCESS = true;
  hideModal("courseRegistrationViewModal");

  if (IS_API_CALL_SUCCESS) {
    showModal("responseSuccessModal");
  } else {
    showModal("responseFailureModal");
  }
}

function UpdateCourseRegistration() {
  const newCourseId = document.getElementById("newCourseId").value;

  if (!newCourseId) {
    alert("Please enter a new Course ID.");
    return;
  }
  let IS_API_CALL_SUCCESS = true;
  hideModal("updateCourseRegistrationModal");

  if (IS_API_CALL_SUCCESS) {
    showModal("responseSuccessModal");
  } else {
    showModal("responseFailureModal");
  }
}

function showModal(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

function hideModal(modalId) {
  const updateModalElement = document.getElementById(modalId);
  const updateModal = bootstrap.Modal.getInstance(updateModalElement);
  updateModal.hide();
}

// function filterCourseRegistrations() {
//   const approvedCheckbox = document.getElementById("filterApproved");
//   const rejectedCheckbox = document.getElementById("filterRejected");
//   const pendingCheckbox = document.getElementById("filterPending");

//   const isApprovedChecked = approvedCheckbox.checked;
//   const isRejectedChecked = rejectedCheckbox.checked;
//   const isPendingChecked = pendingCheckbox.checked;

//   const table = document.getElementById("courseRegistrationTable");
//   const rows = table.getElementsByTagName("tr");

//   for (let i = 1; i < rows.length; i++) {
//     const statusCell = rows[i].getElementsByTagName("td")[2];
//     const statusText = statusCell ? statusCell.innerText.toLowerCase() : "";

//     let shouldDisplayRow = true;

//     if (
//       (isApprovedChecked && statusText.includes("approved")) ||
//       (isRejectedChecked && statusText.includes("rejected")) ||
//       (isPendingChecked && statusText.includes("pending"))
//     ) {
//       shouldDisplayRow = true;
//     } else if (!isApprovedChecked && !isRejectedChecked && !isPendingChecked) {
//       shouldDisplayRow = true;
//     } else {
//       shouldDisplayRow = false;
//     }

//     rows[i].style.display = shouldDisplayRow ? "" : "none";
//   }
// }

// function searchCourseRegistrations() {
//   const input = document.getElementById("searchInput");
//   const filter = input.value.toLowerCase();
//   const table = document.getElementById("courseRegistrationTable");
//   const rows = table.getElementsByTagName("tr");

//   for (let i = 1; i < rows.length; i++) {
//     const cells = rows[i].getElementsByTagName("td");
//     const courseIdElement = cells[0];
//     const courseNameElement = cells[1];
//     const courseId = courseIdElement.innerText.toLowerCase();
//     const courseName = courseNameElement.innerText.toLowerCase();
//     console.log("courseName :>> ", courseName);
//     const isVisible = courseId.includes(filter) || courseName.includes(filter);
//     rows[i].style.display = isVisible ? "" : "none";
//   }
// }

function filterCourseRegistrations() {
  // Call searchCourseRegistrations to apply both search and filter
  searchCourseRegistrations();
}

function searchCourseRegistrations() {
  const approvedCheckbox = document.getElementById("filterApproved");
  const rejectedCheckbox = document.getElementById("filterRejected");
  const pendingCheckbox = document.getElementById("filterPending");

  const isApprovedChecked = approvedCheckbox.checked;
  const isRejectedChecked = rejectedCheckbox.checked;
  const isPendingChecked = pendingCheckbox.checked;

  const input = document.getElementById("searchInput");
  const filter = input.value.toLowerCase();
  const table = document.getElementById("courseRegistrationTable");
  const rows = table.getElementsByTagName("tr");

  for (let i = 1; i < rows.length; i++) {
    const cells = rows[i].getElementsByTagName("td");
    const courseIdElement = cells[0];
    const courseNameElement = cells[1];
    const statusElement = cells[2];
    const courseId = courseIdElement.innerText.toLowerCase();
    const courseName = courseNameElement.innerText.toLowerCase();
    const statusText = statusElement
      ? statusElement.innerText.toLowerCase()
      : "";

    const matchesSearch =
      courseId.includes(filter) || courseName.includes(filter);

    let matchesFilter = false;
    if (
      (isApprovedChecked && statusText.includes("approved")) ||
      (isRejectedChecked && statusText.includes("rejected")) ||
      (isPendingChecked && statusText.includes("pending"))
    ) {
      matchesFilter = true;
    }

    const shouldDisplayRow = matchesSearch && matchesFilter;

    rows[i].style.display = shouldDisplayRow ? "" : "none";
  }
}
