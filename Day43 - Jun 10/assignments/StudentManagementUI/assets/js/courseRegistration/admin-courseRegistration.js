AOS.init({ duration: 1000 });

const approveBtn = document.getElementById("approveBtn");
const rejectBtn = document.getElementById("rejectBtn");
const closeBtn = document.getElementById("closeBtn");

approveBtn.addEventListener("click", approveCourse);
rejectBtn.addEventListener("click", rejectCourse);

let selectedCourseRegistrationId = null; // Global variable to store selected course registration ID

// Function to update the selected course registration ID
function setSelectedCourseRegistrationId(registrationId) {
  selectedCourseRegistrationId = registrationId;
}

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

async function approveCourse() {
  let api_url = `${config.API_URL}/course-registrations/approve?courseRegistrationId=${selectedCourseRegistrationId}`;

  let response = await fetch(api_url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
  if (response.ok) {
    hideModal("courseRegistrationViewModal");
    showModal(
      "Course Registration Approved",
      "The course Registration has been approved.",
      true
    );
    setTimeout(() => {
      window.location.reload();
    }, 3000);
  } else {
    let error = await response.json();
    hideModal("courseRegistrationViewModal");
    showModal(
      "Error",
      `There was an error processing your request: ${error.message}`,
      false
    );
  }
}

async function rejectCourse() {
  let api_url = `${config.API_URL}/course-registrations/reject?courseRegistrationId=${selectedCourseRegistrationId}`;

  let response = await fetch(api_url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  if (response.ok) {
    hideModal("courseRegistrationViewModal");
    showModal(
      "Course Registration Rejected",
      "The course Registration has been rejected.",
      true
    );
    setTimeout(() => {
      window.location.reload();
    }, 3000);
  } else {
    let error = await response.json();
    hideModal("courseRegistrationViewModal");
    showModal(
      "Error",
      `There was an error processing your request: ${error.message}`,
      false
    );
  }
}

// Function to get status text based on approval status code
function getStatusText(status) {
  switch (status) {
    case 1:
      return "Approved";
    case -1:
      return "Rejected";
    case 0:
      return "Pending";
    default:
      return "Unknown";
  }
}

async function viewCourseRegistrationDetails(
  courseRegistrationId,
  studentId,
  courseId,
  courseName,
  approvalStatus,
  comments
) {
  setSelectedCourseRegistrationId(courseRegistrationId);
  fetch(`${config.API_URL}/courses/${courseId}`)
    .then((response) => response.json())
    .then((courseDetails) => {
      const { facultyId } = courseDetails;
      viewCourseRegistrationModal(
        courseRegistrationId,
        studentId,
        courseId,
        courseName,
        facultyId,
        getStatusText(approvalStatus),
        comments
      );
    })
    .catch((error) => console.error("Error fetching course details:", error));
}

async function getStudentName(studentId) {
  let api_url = `${config.API_URL}/students/id?studentRollNo=${studentId}`;

  let response = await fetch(api_url);

  if (response.ok) {
    let data = await response.json();
    return data.name;
  } else {
    return "Unknown";
  }
}

async function getFacultyName(facultyId) {
  let api_url = `${config.API_URL}/faculty/${facultyId}`;

  let response = await fetch(api_url);

  if (response.ok) {
    let data = await response.json();
    return data.name;
  } else {
    return "Unknown";
  }
}

// Function to show modal by ID
function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

// Function to display modal with course registration details
async function viewCourseRegistrationModal(
  courseRegistrationId,
  studentId,
  courseId,
  courseName,
  facultyId,
  approvalStatus,
  comments
) {
  let facultyName = await getFacultyName(facultyId);
  let studentName = await getStudentName(studentId);

  document.getElementById(
    "courseRegistrationViewModalLabel"
  ).textContent = `Course Registration Id - ${courseRegistrationId}`;
  document.getElementById("courseIdView").textContent = courseId;
  document.getElementById("courseNameView").textContent = courseName;
  document.getElementById("studentIdView").textContent = studentId;
  document.getElementById("studentNameView").textContent = studentName;
  document.getElementById("facultyIdView").textContent = facultyId;
  document.getElementById("facultyNameView").textContent = facultyName;
  document.getElementById("statusView").textContent = approvalStatus;
  document.getElementById("commentsView").textContent = comments;

  if ($("#studentStatus").text().toLowerCase() === "active") {
    approveBtn.style.display = "none";
    rejectBtn.style.display = "none";
    closeBtn.style.display = "block";
  }

  showModalById("courseRegistrationViewModal");
}

$(document).ready(function () {
  if (!checkToken()) {
    return;
  }

  const apiUrl = `${config.API_URL}/course-registrations`;

  fetch(apiUrl, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
    .then((response) => response.json())
    .then((data) => {
      populateCourseRegistrationTable(data);
    })
    .catch((error) =>
      console.error("Error fetching course registrations:", error)
    );

  async function populateCourseRegistrationTable(data) {
    const tableBody = document.querySelector("#courseRegistrationTable tbody");
    tableBody.innerHTML = "";

    for (let index = 0; index < data.length; index++) {
      const registration = data[index];
      const statusText = getStatusText(registration.approvalStatus);
      const courseName = await getCourseNameById(registration.courseId);

      const row = `
        <tr data-registration-id="${registration.registrationId}">
          <th scope="row">${index + 1}</th>
          <td>${registration.registrationId}</td>
          <td>${registration.studentId}</td>
          <td>${registration.courseId}</td>
          <td id="${statusText.toLowerCase()}"><p>${statusText}</p></td>
          <td>
            <button class="btn btn-primary" onclick="viewCourseRegistrationDetails(
            ${registration.registrationId},
            ${registration.studentId},
            ${registration.courseId}, '${courseName}', ${
        registration.approvalStatus
      }, '${registration.comments}')">
              View Details
            </button>
          </td>
        </tr>
      `;
      tableBody.insertAdjacentHTML("beforeend", row);
    }

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

    // Attach change event listeners to the filter checkboxes
    $("#filterApproved, #filterRejected, #filterPending").on(
      "change",
      function () {
        table.draw();
      }
    );

    // Custom filter function for the table
    $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
      const filterApproved = $("#filterApproved").is(":checked");
      const filterRejected = $("#filterRejected").is(":checked");
      const filterPending = $("#filterPending").is(":checked");
      const status = data[4].toLowerCase();

      if (
        (filterApproved && status.toLowerCase().includes("approved")) ||
        (filterRejected && status.toLowerCase().includes("rejected")) ||
        (filterPending && status.toLowerCase().includes("pending"))
      ) {
        return true;
      } else if (!filterApproved && !filterRejected && !filterPending) {
        // Show all rows if no filter is selected
        return true;
      }
      return false;
    });

    // Initial filter application
    table.draw();
  }

  async function getCourseNameById(courseId) {
    try {
      const response = await fetch(`${config.API_URL}/courses/${courseId}`);
      const courseData = await response.json();
      return courseData.name;
    } catch (error) {
      console.error(
        `Error fetching course name for courseId ${courseId}:`,
        error
      );
      return "Unknown Course";
    }
  }
});
