// Initialize AOS with a duration of 1000ms
AOS.init({ duration: 1000 });

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

let selectedCourseId = null;

// Function to register the course
async function registerCourse() {
  const studentId = getUserId();
  if (!studentId) {
    showModal(
      "responseFailureModal",
      "Registration Failed",
      "Student ID is missing. Please log in and try again."
    );
    return;
  }

  const requestBody = {
    studentId: studentId,
    courseId: selectedCourseId,
  };

  try {
    const response = await fetch(`${config.API_URL}/course-registrations`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(requestBody),
    });
    hideModal("courseViewModal");
    if (response.ok) {
      const responseData = await response.json();
      showModal(
        "responseSuccessModal",
        "Registration Successful",
        "You have successfully registered for the course."
      );

      setTimeout(() => {
        window.location.reload();
      }, 3000);

      // Call the fetch function here
    } else {
      const responseData = await response.json();
      showModal(
        "responseFailureModal",
        "Registration Failed",
        responseData.message ||
          "An error occurred while registering for the course."
      );
    }
  } catch (error) {
    console.error("Error registering for course:", error);
    showModal(
      "responseFailureModal",
      "Registration Failed",
      "An unexpected error occurred. Please try again later."
    );
  }
}

// Function to show a Bootstrap modal by ID with custom title and body content
function showModal(modalId, title, body) {
  const modalElement = document.getElementById(modalId);
  modalElement.querySelector(".modal-title").textContent = title;
  modalElement.querySelector(".modal-body").textContent = body;
  const modalInstance = new bootstrap.Modal(modalElement);
  modalInstance.show();
}

function showModalById(modalId) {
  const modalElement = document.getElementById(modalId);
  const modalInstance = new bootstrap.Modal(modalElement);
  modalInstance.show();
}

// Function to hide a Bootstrap modal by ID
function hideModal(modalId) {
  const modalElement = document.getElementById(modalId);
  const modalInstance = bootstrap.Modal.getInstance(modalElement);
  modalInstance.hide();
}

// Function to fetch courses from the API and populate the DataTable
async function fetchAndPopulateCourses() {
  try {
    const response = await fetch(`${config.API_URL}/courses`);
    const courses = await response.json();

    // Clear the table body
    const tableBody = $("#courseTable tbody");
    tableBody.empty();

    // Populate the table body with course data
    courses.forEach((course, index) => {
      tableBody.append(`
        <tr>
          <th scope="row">${index + 1}</th>
          <td>${course.courseId}</td>
          <td>${course.name}</td>
          <td>${course.courseFees}</td>
          <td>
            <button class="btn btn-primary" onclick="viewCourse(${
              course.courseId
            }, '${course.name}', '${course.description}', ${
        course.facultyId
      }, ${course.courseFees}, ${course.courseVacancy})">
              View Course
            </button>
          </td>
        </tr>
      `);
    });

    // ("object :>> too ");

    // Reinitialize DataTable to apply to new data
    $("#courseTable").DataTable().destroy();
    $("#courseTable").DataTable({
      // Disable sorting on the last column (Action column)
      columnDefs: [{ orderable: false, targets: 4 }],
      // Define column definitions
      columns: [
        null,
        null,
        null,
        null,
        { searchable: false }, // Action column should not be searchable
      ],
      // Set pagination type to full_numbers
      pagingType: "full_numbers",
      // Customize language settings
      language: {
        // Customize pagination button labels with icons
        paginate: {
          previous: '<span class="fa fa-chevron-left"></span>',
          next: '<span class="fa fa-chevron-right"></span>',
          first: '<span class="fa fa-angle-double-left"></span>',
          last: '<span class="fa fa-angle-double-right"></span>',
        },
        // Customize length menu options
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
  } catch (error) {
    console.error("Error fetching courses:", error);
  }
}

// Function to view course details in a modal
async function viewCourse(
  courseId,
  name,
  description,
  facultyId,
  courseFees,
  courseVacancy
) {
  selectedCourseId = courseId;

  // Fetch faculty name
  try {
    const response = await fetch(`${config.API_URL}/faculty/${facultyId}`);
    const facultyData = await response.json();
    const facultyName = facultyData.name;

    document.getElementById(
      "courseViewModalLabel"
    ).textContent = `Course Details: ${name}`;
    document.getElementById("courseViewContent").innerHTML = `
      <div class="container">
        <div class="row mb-2">
          <div class="col-6">
            <strong>Course ID:</strong>
            <p>${courseId}</p>
          </div>
          <div class="col-6">
            <strong>Course Name:</strong>
            <p>${name}</p>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-6">
            <strong>Course Description:</strong>
            <p>${description}</p>
          </div>
          <div class="col-6">
            <strong>Vacancy Left:</strong>
            <p>${courseVacancy}</p>
          </div>
        </div>
        <div class="row mb-2">
          <div class="col-4">
            <strong>Faculty ID:</strong>
            <p>${facultyId}</p>
          </div>
          <div class="col-4">
            <strong>Faculty Name:</strong>
            <p>${facultyName}</p>
          </div>
          <div class="col-4">
            <strong>Course Fee:</strong>
            <p>${courseFees}</p>
          </div>
        </div>
      </div>
    `;
    showModalById("courseViewModal");
  } catch (error) {
    console.error("Error fetching faculty name:", error);
    showModal(
      "responseFailureModal",
      "Error",
      "An error occurred while fetching faculty information. Please try again later."
    );
  }
}

// Document ready function
$(document).ready(function () {
  if (!checkToken()) {
    return;
  }
  fetchAndPopulateCourses();
});
