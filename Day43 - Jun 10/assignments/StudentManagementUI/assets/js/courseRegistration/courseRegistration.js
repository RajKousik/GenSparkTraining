// Initialize Animation on Scroll library
AOS.init({ duration: 1000 });

let selectedCourseRegistrationId = null; // Global variable to store selected course registration ID

// Function to update the selected course registration ID
function setSelectedCourseRegistrationId(registrationId) {
  selectedCourseRegistrationId = registrationId;
}

// Function to update course registration
function UpdateCourseRegistration() {
  const newCourseId = document.getElementById("newCourseId").value.trim();
  document.getElementById("newCourseId").value = "";
  if (!selectedCourseRegistrationId) {
    console.error("No course registration ID selected.");
    return;
  }

  if (!newCourseId) {
    console.error("New Course ID cannot be empty.");
    return;
  }

  const apiUrl = `${config.API_URL}/course-registrations/update?courseRegistrationId=${selectedCourseRegistrationId}&courseId=${newCourseId}`;

  fetch(apiUrl, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      // Add any additional headers if required
    },
    // body: JSON.stringify({ courseId: newCourseId }), // If body payload is needed
  })
    .then(async (response) => {
      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
      }
      return response.json(); // Assuming the API returns JSON response
    })
    .then((data) => {
      // Show success modal
      hideModal("updateCourseRegistrationModal");
      showModal(
        "responseSuccessModal",
        "Update Success",
        `Successfully Updated the Course Registration to Course Id ${newCourseId}!`
      );
      setTimeout(() => {
        window.location.reload();
      }, 3000);
    })
    .catch((error) => {
      // Show failure modal
      hideModal("updateCourseRegistrationModal");
      showModal("responseFailureModal", "Update Failed", error.message);
      console.error("Error updating course registration:", error);
    });
}

// Function to delete course registration
function DeleteCourseRegistration() {
  if (!selectedCourseRegistrationId) {
    console.error("No course registration ID selected.");
    return;
  }

  const apiUrl = `${config.API_URL}/course-registrations?courseRegistrationId=${selectedCourseRegistrationId}`;

  fetch(apiUrl, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
      // Add any additional headers if required
    },
  })
    .then(async (response) => {
      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
      }
      return response.json(); // Assuming the API returns JSON response
    })
    .then((data) => {
      // Show success modal or handle success as per your application flow
      hideModal("courseRegistrationViewModal");
      showModal(
        "responseSuccessModal",
        "Delete Success",
        `Successfully Deleted Course Registration with ID ${selectedCourseRegistrationId}!`
      );
      setTimeout(() => {
        window.location.reload();
      }, 3000);
    })
    .catch((error) => {
      // Show failure modal or handle failure as per your application flow
      hideModal("courseRegistrationViewModal");
      showModal("responseFailureModal", "Delete Failed", `${error.message}`);
      console.error("Error deleting course registration:", error);
    });
}

function showModal(modalId, title, body) {
  const modalElement = document.getElementById(modalId);
  modalElement.querySelector(".modal-title").textContent = title;
  modalElement.querySelector(".modal-body").textContent = body;
  const modalInstance = new bootstrap.Modal(modalElement);
  modalInstance.show();
}

function hideModal(modalId) {
  const modalElement = document.getElementById(modalId);
  const modalInstance = bootstrap.Modal.getInstance(modalElement);
  modalInstance.hide();
}

// Function to fetch course registration details and display modal
async function viewCourseRegistrationDetails(
  courseRegistrationId,
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
        courseId,
        courseName,
        facultyId,
        getStatusText(approvalStatus),
        comments
      );
    })
    .catch((error) => console.error("Error fetching course details:", error));
}

// Function to display modal with course registration details
function viewCourseRegistrationModal(
  courseId,
  courseName,
  facultyId,
  approvalStatus,
  comments
) {
  document.getElementById(
    "courseRegistrationViewModalLabel"
  ).textContent = `Course Registration Details`;
  document.getElementById("courseIdView").textContent = courseId;
  document.getElementById("courseNameView").textContent = courseName;
  document.getElementById("approvalStatusView").textContent = approvalStatus;
  document.getElementById("commentsView").textContent = comments;
  document.getElementById("facultyIdView").textContent = facultyId;

  showModalById("courseRegistrationViewModal");
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

// Function to show modal by ID
function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

// Fetch course registrations and populate table
document.addEventListener("DOMContentLoaded", function () {
  if (!checkToken()) {
    return;
  }
  const studentId = getUserId();
  if (!studentId) {
    console.error("Something went wrong! Log in again");
    return;
  }

  const apiUrl = `${config.API_URL}/course-registrations/student?studentId=${studentId}`;

  fetch(apiUrl)
    .then((response) => response.json())
    .then((data) => {
      populateCourseRegistrationTable(data);
    })
    .catch((error) =>
      console.error("Error fetching course registrations:", error)
    );

  // Populate course registration table with data
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
          <td>${registration.courseId}</td>
          <td>${courseName}</td>
          <td id="${statusText.toLowerCase()}"><p>${statusText}</p></td>
          <td>
            <button class="btn btn-primary" onclick="viewCourseRegistrationDetails(
            ${registration.registrationId},
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

    // Initialize DataTable
    const table = $("#courseRegistrationTable").DataTable({
      columnDefs: [{ orderable: false, targets: 5 }],
      pagingType: "full_numbers",
      columns: [null, null, null, null, null, { searchable: false }],
      language: {
        paginate: {
          previous: '<span class="fa fa-chevron-left"></span>',
          next: '<span class="fa fa-chevron-right"></span>',
          first: '<span class="fa-solid fa-angles-left"></span>',
          last: '<span class="fa-solid fa-angles-right"></span>',
        },
        pageLength: 5,
        lengthMenu:
          'Display <select class="form-control input-sm">' +
          '<option value="3">3</option>' +
          '<option value="5" selected>5</option>' +
          '<option value="10">10</option>' +
          '<option value="15">15</option>' +
          '<option value="20">20</option>' +
          '<option value="25">25</option>' +
          '<option value="-1">All</option>' +
          "</select> results",
      },
    });

    // Initialize filter event handlers and apply initial filtering
    $("#filterApproved, #filterRejected, #filterPending").on(
      "change",
      function () {
        table.draw();
      }
    );

    // Custom filter function for DataTable
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
      }
      return false;
    });

    // Apply initial filter
    table.draw();
  }

  // Function to fetch course name based on courseId
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

  // Function to hide modal by ID
});
