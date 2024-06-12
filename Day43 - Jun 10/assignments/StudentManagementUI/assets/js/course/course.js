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

function searchCourses() {
  const input = document.getElementById("searchInput");
  const filter = input.value.toLowerCase();
  const table = document.getElementById("courseTable");
  const rows = table.getElementsByTagName("tr");

  const toggleRowVisibility = (row, isVisible) => {
    row.style.display = isVisible ? "" : "none";
  };

  for (let i = 1; i < rows.length; i++) {
    const cells = rows[i].getElementsByTagName("td");
    const courseIdElement = cells[0];
    const courseNameElement = cells[1];
    const courseId = courseIdElement.innerText.toLowerCase();
    const courseName = courseNameElement.innerText.toLowerCase();

    const isVisible =
      courseId.indexOf(filter) > -1 || courseName.indexOf(filter) > -1;
    toggleRowVisibility(rows[i], isVisible);
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
