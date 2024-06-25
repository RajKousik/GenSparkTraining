const addCourseNav = document.getElementById("add-course-nav");
const updateCourseNav = document.getElementById("update-course-nav");
const deleteCourseNav = document.getElementById("delete-course-nav");
const viewAllCoursesNav = document.getElementById("view-all-courses-nav");

const addCourseView = document.getElementById("add-course-form");
const updateCourseView = document.getElementById("update-course-form");
// const deleteCourseView = document.getElementById("delete-course-form");
const viewAllCoursesView = document.getElementById("view-all-courses");

function showModal(title, message, isSuccess) {
  var modal = document.getElementById("courseModal");
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

function showModalById(modalId) {
  const modalElement = document.getElementById(modalId);
  const modalInstance = new bootstrap.Modal(modalElement);
  modalInstance.show();
}

document.getElementById("modalUpdateBtn").addEventListener("click", () => {
  addCourseView.classList.add("d-none");
  updateCourseView.classList.remove("d-none");
  viewAllCoursesView.classList.add("d-none");

  addCourseNav.classList.remove("active");
  updateCourseNav.classList.add("active");
  viewAllCoursesNav.classList.remove("active");

  const courseId = document.getElementById("courseIdModal").innerText;

  populateUpdateForm(courseId);
});

async function populateUpdateForm(courseId) {
  let response = await fetch(`${config.API_URL}/courses/${courseId}`);

  let data;
  if (response.ok) {
    data = await response.json();
  }

  console.log("data.facultyId :>> ", data.facultyId);

  document.getElementById("courseId").value = data.courseId;
  document.getElementById("updateCourseName").value = data.name;
  document.getElementById("updateCourseDescription").value = data.description;
  document.getElementById("updateFacultyId").value = data.facultyId;
  document.getElementById("updateCourseFees").value = data.courseFees;
  document.getElementById("updateVacancy").value = data.courseVacancy;
}

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
            <p id="courseIdModal">${courseId}</p>
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
    console.error("Error fetching course details:", error);
    showModal(
      "responseFailureModal",
      "Error",
      "An error occurred while fetching course information. Please try again later."
    );
  }
}

document.addEventListener("DOMContentLoaded", function () {
  AOS.init({ duration: 1000 });

  if (!checkToken()) {
    return;
  }

  if (window.top === window.self) {
    // If the page is not in an iframe, redirect to the main page or show an error
    window.location.href = "../../../src/pages/admin/index.html";
  }

  const token = getTokenFromLocalStorage();

  populateFaculty("facultyId");
  populateCourseId("courseId");
  populateCourseId("updateFacultyId");

  addCourseNav.addEventListener("click", () => {
    addCourseView.classList.remove("d-none");
    updateCourseView.classList.add("d-none");
    //   deleteCourseView.classList.add("d-none");
    viewAllCoursesView.classList.add("d-none");

    populateFaculty("facultyId");

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

    populateCourseId("courseId");
    populateFaculty("updateFacultyId");

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

    populateCourseTable();

    addCourseNav.classList.remove("active");
    updateCourseNav.classList.remove("active");
    //   deleteCourseNav.classList.remove("active");
    viewAllCoursesNav.classList.add("active");
  });

  // Function to show modal with dynamic content

  function populateFaculty(elementId) {
    fetch(`${config.API_URL}/faculty`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const facultySelect = document.getElementById(elementId);
        facultySelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select Faculty";
        option.disabled = true;
        option.selected = true;
        facultySelect.appendChild(option);

        data.forEach((faculty) => {
          const option = document.createElement("option");
          option.value = faculty.facultyId;
          option.textContent = faculty.facultyId + " - " + faculty.name;
          facultySelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching departments:", error);
      });
  }

  async function populateCourseTable() {
    const response = await fetch(`${config.API_URL}/courses`);
    const courses = await response.json();

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

    $("#courseTable").DataTable().destroy();
    $("#courseTable").DataTable({
      columns: [
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
  }

  function populateCourseId(elementId) {
    fetch(`${config.API_URL}/courses`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const courseSelect = document.getElementById(elementId);
        courseSelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select Course Id";
        option.disabled = true;
        option.selected = true;
        courseSelect.appendChild(option);

        data.forEach((course) => {
          const option = document.createElement("option");
          option.value = course.courseId;
          option.textContent = course.courseId;
          courseSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching courses:", error);
      });
  }
  // Add Course Form Submission
  var addCourseForm = document.getElementById("addCourseForm");
  addCourseForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const name = document.getElementById("courseName").value;
    const description = document.getElementById("courseDescription").value;
    const facultyId = document.getElementById("facultyId").value;
    const courseFees = document.getElementById("courseFees").value;
    const courseVacancy = document.getElementById("vacancy").value;

    const token = getTokenFromLocalStorage();

    fetch(`${config.API_URL}/courses`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        name,
        description,
        facultyId,
        courseFees,
        courseVacancy,
      }),
    })
      .then(async (response) => {
        if (response.ok) {
          return response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message);
        }
      })
      .then((data) => {
        showModal("Success", "Course added successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to add course: ${error.message}`, false);
      });
    addCourseForm.reset();
  });

  // Update Course Form Submission
  var updateCourseForm = document.getElementById("updateCourseForm");
  updateCourseForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const courseId = document.getElementById("courseId").value;
    const name = document.getElementById("updateCourseName").value;
    const description = document.getElementById(
      "updateCourseDescription"
    ).value;
    const facultyId = document.getElementById("updateFacultyId").value;
    const courseFees = document.getElementById("updateCourseFees").value;
    const courseVacancy = document.getElementById("updateVacancy").value;

    const token = getTokenFromLocalStorage();

    let api_url = `${config.API_URL}/courses/${courseId}`;
    fetch(api_url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        name,
        description,
        facultyId,
        courseFees,
        courseVacancy,
      }),
    })
      .then(async (response) => {
        if (response.ok) {
          return response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message);
        }
      })
      .then((data) => {
        showModal("Success", "Course updated successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to update course: ${error.message}`, false);
      });
    updateCourseForm.reset();
  });
});

$(document).ready(function () {
  if (!checkToken()) {
    return;
  }
  // $("#courseTable").DataTable().destroy();
  // $("#courseTable").DataTable({
  //   columns: [null, null, null, null, { searchable: false, orderable: false }],
  //   pagingType: "full_numbers",
  //   language: {
  //     paginate: {
  //       previous: '<span class="fa fa-chevron-left"></span>',
  //       next: '<span class="fa fa-chevron-right"></span>',
  //       first: '<span class="fa-solid fa-angles-left"></span>',
  //       last: '<span class="fa-solid fa-angles-right"></span>',
  //     },
  //     lengthMenu:
  //       'Display <select class="form-control input-sm">' +
  //       '<option value="3">3</option>' +
  //       '<option value="5">5</option>' +
  //       '<option value="10">10</option>' +
  //       '<option value="15">15</option>' +
  //       '<option value="20">20</option>' +
  //       '<option value="25">25</option>' +
  //       '<option value="-1">All</option>' +
  //       "</select> results",
  //   },
  // });
});
