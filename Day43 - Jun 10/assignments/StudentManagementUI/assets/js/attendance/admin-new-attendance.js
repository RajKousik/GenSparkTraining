const addAttendanceNav = document.getElementById("add-attendance-nav");
const updateAttendanceNav = document.getElementById("update-attendance-nav");
const deleteAttendanceNav = document.getElementById("delete-attendance-nav");
const viewAllAttendanceNav = document.getElementById("view-all-attendance-nav");
const manageAttendanceNav = document.getElementById("manage-attendance-nav");

const addAttendanceView = document.getElementById("add-attendance-form");
const updateAttendanceView = document.getElementById("update-attendance-form");
const deleteAttendanceView = document.getElementById("delete-attendance-form");
const viewAllAttendanceView = document.getElementById("view-all-attendance");
const manageAttendanceView = document.getElementById("manage-attendance");

function showModalById(modalId) {
  const modalElement = document.getElementById(modalId);
  const modalInstance = new bootstrap.Modal(modalElement);
  modalInstance.show();
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

function formatDate(dateString) {
  const date = new Date(dateString.split("T")[0]);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
}

async function viewAttendanceDetails(attendanceId) {
  try {
    const response = await fetch(
      `${config.API_URL}/student-attendance/${attendanceId}`
    );
    const data = await response.json();

    if (data) {
      document.getElementById("studentIdModal").textContent =
        data.studentRollNo;
      document.getElementById("studentNameModal").textContent =
        await getStudentName(data.studentRollNo);
      document.getElementById("courseIdModal").textContent = data.courseId;
      document.getElementById("courseNameModal").textContent =
        await getCourseNameById(data.courseId);
      document.getElementById("attendanceDateModal").textContent = formatDate(
        data.date
      );
      document.getElementById("attendanceStatusModal").textContent =
        data.attendanceStatus;
      document.getElementById("AttendanceIdModal").textContent = data.id;

      showModalById("attendanceViewModal");
    } else {
      console.error(`Attendance with ID ${data.id} not found.`);
    }
  } catch (error) {
    console.error("Error fetching attendance details:", error);
  }
}

document.getElementById("modalUpdateBtn").addEventListener("click", () => {
  addAttendanceView.classList.add("d-none");
  updateAttendanceView.classList.remove("d-none");
  deleteAttendanceView.classList.add("d-none");
  viewAllAttendanceView.classList.add("d-none");
  manageAttendanceView.classList.add("d-none");

  addAttendanceNav.classList.remove("active");
  updateAttendanceNav.classList.add("active");
  deleteAttendanceNav.classList.remove("active");
  viewAllAttendanceNav.classList.remove("active");
  manageAttendanceNav.classList.remove("active");

  const attendanceId = document.getElementById("AttendanceIdModal").innerText;

  populateUpdateForm(attendanceId);
});

document.getElementById("modalDeleteBtn").addEventListener("click", () => {
  addAttendanceView.classList.add("d-none");
  updateAttendanceView.classList.add("d-none");
  deleteAttendanceView.classList.remove("d-none");
  viewAllAttendanceView.classList.add("d-none");
  manageAttendanceView.classList.add("d-none");

  addAttendanceNav.classList.remove("active");
  updateAttendanceNav.classList.remove("active");
  deleteAttendanceNav.classList.add("active");
  viewAllAttendanceNav.classList.remove("active");
  manageAttendanceNav.classList.remove("active");

  const attendanceId = document.getElementById("AttendanceIdModal").innerText;
  document.getElementById("deleteAttendanceId").value = attendanceId;
});

async function populateUpdateForm(attendanceId) {
  let response = await fetch(
    `${config.API_URL}/student-attendance/${attendanceId}`
  );

  let data;
  if (response.ok) {
    data = await response.json();
  }

  document.getElementById("attendanceId").value = data.id;
  document.getElementById("updateAttendanceStatus").value =
    data.attendanceStatus;
}

function newToast(classBackground, message) {
  const toastNotification = new bootstrap.Toast(
    document.getElementById("toastNotification")
  );
  var toast = document.getElementById("toastNotification");
  toast.className = "toast align-items-center text-white border-0";
  toast.classList.add(`${classBackground}`);
  var toastBody = document.querySelector(".toast-body");
  if (toastBody) {
    toastBody.innerHTML = `${message}`;
  }
  toastNotification.show();
}

document.addEventListener("DOMContentLoaded", function () {
  // AOS Initialization
  AOS.init({ duration: 1000 });

  // Token Validation
  if (!checkToken()) {
    return;
  }

  if (window.top === window.self) {
    // If the page is not in an iframe, redirect to the main page or show an error
    window.location.href = "../../../src/pages/admin/index.html";
  }

  const token = getTokenFromLocalStorage();

  // NEWLY ADDED START

  //   const API_URL = "your_api_url_here"; // Replace with actual API URL

  // NEWLY ADDED START
  var today = new Date();
  var day = today.getDate() > 9 ? today.getDate() : "0" + today.getDate(); // format should be "DD" not "D" e.g 09
  var month =
    today.getMonth() + 1 > 9
      ? today.getMonth() + 1
      : "0" + (today.getMonth() + 1);
  var year = today.getFullYear();

  $("#attendance-date").attr("max", `${year}-${month}-${day}`);

  // Set today's date as default
  document.getElementById("attendance-date").valueAsDate = new Date();

  // Fetch and populate courses
  fetch(`${config.API_URL}/courses`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
    .then((response) => response.json())
    .then((courses) => {
      const courseList = document.querySelector("#course-list ul");
      courseList.innerHTML = ""; // Clear previous courses

      courses.forEach((course) => {
        const li = document.createElement("li");
        li.className = "list-group-item course-item";
        li.innerHTML = `<button type="button" class="mark-attendance-btn btn w-100" data-course-id="${course.courseId}">
                        ${course.courseId} - ${course.name}
                      </button>`;
        courseList.appendChild(li);
      });

      // Add event listeners to the dynamically created buttons
      document.querySelectorAll(".mark-attendance-btn").forEach((button) => {
        button.addEventListener("click", function () {
          const courseId = this.getAttribute("data-course-id");
          document
            .querySelectorAll(".mark-attendance-btn")
            .forEach((other_button) => {
              other_button.classList.remove("active");
            });
          this.classList.add("active");
          // Fetch and populate students
          document.getElementById("attendance-date").valueAsDate = new Date();
          populateStudents(
            courseId,
            document.getElementById("attendance-date").value
          );
        });
      });
    })
    .catch((error) => console.error("Error fetching courses:", error));

  // Listen for change in attendance date
  document
    .getElementById("attendance-date")
    .addEventListener("change", function () {
      const selectedDate = this.value;
      const courseId = document
        .querySelector(".mark-attendance-btn.active")
        .getAttribute("data-course-id"); // Assumes a selected course is marked as active
      // Repopulate students based on the selected date and course
      populateStudents(courseId, selectedDate);
    });

  function populateStudents(courseId, attendanceDate) {
    fetch(
      `${config.API_URL}/course-registrations/approved-students?courseId=${courseId}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    )
      .then(async (response) => {
        if (response.ok) {
          return await response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
        }
      })
      .then((students) => {
        const studentList = document.querySelector("#student-list ul");
        studentList.innerHTML = ""; // Clear previous students

        // Fetch and display attendance status for each student
        students.forEach((student) => {
          const studentRollNo = student.studentRollNo;
          fetchAttendanceStatus(
            studentRollNo,
            courseId,
            attendanceDate,
            (attendanceStatus, attendanceId) => {
              const li = document.createElement("li");
              li.className =
                "list-group-item d-flex justify-content-between align-items-center";
              li.innerHTML = `${student.studentRollNo} - ${student.name}
            <div>
              <button type="button" class="present-btn btn btn-success attendanceBtn" data-course-id="${courseId}" data-student-id="${studentRollNo}" data-attendance-status="Present" data-attendance-id="${attendanceId}" ${
                attendanceStatus === "Present" ? "disabled" : ""
              }>Present</button>
              <button type="button" class="absent-btn btn btn-danger attendanceBtn" data-course-id="${courseId}" data-student-id="${studentRollNo}" data-attendance-status="Absent" data-attendance-id="${attendanceId}" ${
                attendanceStatus === "Absent" ? "disabled" : ""
              }>Absent</button>
              <button type="button" class="onduty-btn btn btn-warning attendanceBtn" data-course-id="${courseId}" data-student-id="${studentRollNo}" data-attendance-status="Od" data-attendance-id="${attendanceId}" ${
                attendanceStatus === "Od" ? "disabled" : ""
              }>On Duty</button>
            </div>`;

              // Set the background color based on fetched attendance status
              if (attendanceStatus === "Present") {
                li.style.backgroundColor = "#d4edda";
              } else if (attendanceStatus === "Absent") {
                li.style.backgroundColor = "#f8d7da";
              } else if (attendanceStatus === "Od") {
                li.style.backgroundColor = "#fff3cd";
              }

              // Add event listeners to attendance buttons
              const presentButton = li.querySelector(".present-btn");
              const absentButton = li.querySelector(".absent-btn");
              const ondutyButton = li.querySelector(".onduty-btn");

              presentButton.addEventListener("click", function () {
                markAttendance(
                  studentRollNo,
                  courseId,
                  attendanceDate,
                  "Present",
                  attendanceId
                );
              });

              absentButton.addEventListener("click", function () {
                markAttendance(
                  studentRollNo,
                  courseId,
                  attendanceDate,
                  "Absent",
                  attendanceId
                );
              });

              ondutyButton.addEventListener("click", function () {
                markAttendance(
                  studentRollNo,
                  courseId,
                  attendanceDate,
                  "Onduty",
                  attendanceId
                );
              });

              studentList.appendChild(li);
            }
          );
        });

        // Show the student list and hide the course list
        document.getElementById("course-list").style.display = "none";
        document.getElementById("student-list").style.display = "block";
      })
      .catch((error) => {
        newToast("bg-danger", "No students opted for the course!");
        console.error("Error fetching students:", error);
      });
  }

  function fetchAttendanceStatus(studentRollNo, courseId, date, callback) {
    fetch(`${config.API_URL}/student-attendance/student/${studentRollNo}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((attendanceRecords) => {
        // Check if there is an attendance record for the given courseId and date
        const attendanceRecord = attendanceRecords.find((record) => {
          return (
            record.courseId === parseInt(courseId) &&
            record.date.startsWith(date)
          );
        });

        callback(
          attendanceRecord ? attendanceRecord.attendanceStatus : null,
          attendanceRecord ? attendanceRecord.id : null
        );
      })
      .catch((error) => {
        console.error("Error fetching attendance status:", error);
        callback(null, null);
      });
  }

  function markAttendance(
    studentRollNo,
    courseId,
    date,
    attendanceStatus,
    attendanceId
  ) {
    let apiUrl = `${config.API_URL}/student-attendance`;

    // If attendanceId is available, it means attendance already exists and needs to be updated
    attendanceStatus =
      attendanceStatus.toLowerCase() == "onduty" ? "Od" : attendanceStatus;
    if (attendanceId) {
      apiUrl += `/${attendanceId}?attendanceStatus=${attendanceStatus}`;
    }

    const requestBody = {
      studentRollNo: studentRollNo,
      courseId: courseId,
      date: date,
      attendanceStatus: attendanceStatus,
    };

    fetch(apiUrl, {
      method: attendanceId ? "PUT" : "POST", // Use PUT if updating existing attendance, POST if creating new attendance
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(requestBody),
    })
      .then(async (response) => {
        if (!response.ok) {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
          // throw new Error("Failed to mark attendance");
        }
        return response.json();
      })
      .then((data) => {
        // Optionally update UI to reflect the change in attendance
        // For example, disable the button that was clicked
        const button = document.querySelector(
          `button[data-course-id="${courseId}"][data-student-id="${studentRollNo}"][data-attendance-status="${attendanceStatus}"]`
        );
        if (button) {
          document.querySelectorAll(".attendanceBtn").forEach((btn) => {
            btn.disabled = false;
          });
          button.disabled = true;
          const li = button.closest(".list-group-item");
          if (attendanceStatus === "Present") {
            li.style.backgroundColor = "#d4edda";
          } else if (attendanceStatus === "Absent") {
            li.style.backgroundColor = "#f8d7da";
          } else if (attendanceStatus === "Od") {
            li.style.backgroundColor = "#fff3cd";
          }
        }
      })
      .catch((error) => {
        console.error("Failed to mark attendance:", error);
        // Optionally show an error message
        newToast("bg-danger", error.message);
      });
  }

  document
    .getElementById("back-to-courses")
    .addEventListener("click", function () {
      // Hide the student list and show the course list
      document.getElementById("student-list").style.display = "none";
      document.getElementById("course-list").style.display = "block";
    });

  // Handle attendance status buttons
  //   document
  //     .getElementById("student-list")
  //     .addEventListener("click", function (event) {
  //       if (event.target.tagName === "BUTTON") {
  //         const button = event.target;
  //         const studentId = button.getAttribute("data-student-id");
  //         const courseId = button.getAttribute("data-course-id");
  //         const buttons = button.parentElement.querySelectorAll("button");

  //         // Save the attendance status in local storage
  //         if (button.classList.contains("present-btn")) {
  //           localStorage.setItem(
  //             `course-${courseId}-student-${studentId}`,
  //             "present"
  //           );
  //           event.target.parentElement.parentElement.style.backgroundColor =
  //             "#d4edda";
  //         } else if (button.classList.contains("absent-btn")) {
  //           localStorage.setItem(
  //             `course-${courseId}-student-${studentId}`,
  //             "absent"
  //           );
  //           event.target.parentElement.parentElement.style.backgroundColor =
  //             "#f8d7da";
  //         } else if (button.classList.contains("onduty-btn")) {
  //           localStorage.setItem(
  //             `course-${courseId}-student-${studentId}`,
  //             "onduty"
  //           );
  //           event.target.parentElement.parentElement.style.backgroundColor =
  //             "#fff3cd";
  //         }

  //         // Disable the clicked button and enable others
  //         buttons.forEach((btn) => {
  //           btn.disabled = false;
  //         });
  //         button.disabled = true;
  //       }
  //     });

  // NEWLY ADDED END

  populateStudentId("studentRollNo");
  populateCourseId("courseId");

  populateAttendanceId("attendanceId");

  populateAttendanceId("deleteAttendanceId");

  addAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.remove("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.add("d-none");
    manageAttendanceView.classList.add("d-none");

    populateStudentId("studentRollNo");
    populateCourseId("courseId");

    addAttendanceNav.classList.add("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.remove("active");
    manageAttendanceNav.classList.remove("active");
  });

  updateAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.remove("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.add("d-none");
    manageAttendanceView.classList.add("d-none");

    populateAttendanceId("attendanceId");

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.add("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.remove("active");
    manageAttendanceNav.classList.remove("active");
  });

  deleteAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.remove("d-none");
    viewAllAttendanceView.classList.add("d-none");
    manageAttendanceView.classList.add("d-none");

    populateAttendanceId("deleteAttendanceId");

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.add("active");
    viewAllAttendanceNav.classList.remove("active");
    manageAttendanceNav.classList.remove("active");
  });

  viewAllAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.remove("d-none");
    manageAttendanceView.classList.add("d-none");

    populateAttendanceTable();

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.add("active");
    manageAttendanceNav.classList.remove("active");
  });

  manageAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.add("d-none");
    manageAttendanceView.classList.remove("d-none");

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.remove("active");
    manageAttendanceNav.classList.add("active");
  });

  document
    .getElementById("studentRollNo")
    .addEventListener("change", function () {
      const studentRollNo = this.value;
      populateCourseId("courseId", studentRollNo);
    });

  function populateCourseId(elementId, studentRollNo = "") {
    const apiUrl = studentRollNo
      ? `${config.API_URL}/course-registrations/students?studentId=${studentRollNo}&status=1`
      : `${config.API_URL}/courses`;

    fetch(apiUrl, {
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

        data.forEach(async (course) => {
          let courseName = await getCourseNameById(course.courseId);
          const option = document.createElement("option");
          option.value = course.courseId;
          option.textContent = course.courseId + " - " + courseName;
          courseSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching courses:", error);
      });
  }

  function populateStudentId(elementId) {
    fetch(`${config.API_URL}/students/all`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const studentSelect = document.getElementById(elementId);
        studentSelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select student Id";
        option.disabled = true;
        option.selected = true;
        studentSelect.appendChild(option);

        data.forEach((student) => {
          const option = document.createElement("option");
          option.value = student.studentRollNo;
          option.textContent = student.studentRollNo + " - " + student.name;
          studentSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching students:", error);
      });
  }

  function populateAttendanceId(elementId) {
    fetch(`${config.API_URL}/student-attendance`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const attendanceSelect = document.getElementById(elementId);
        attendanceSelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select attendance Id";
        option.disabled = true;
        option.selected = true;
        attendanceSelect.appendChild(option);

        data.forEach((attendance) => {
          const option = document.createElement("option");
          option.value = attendance.id;
          option.textContent = attendance.id;
          attendanceSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching students:", error);
      });
  }

  // Function to show modal with dynamic content
  function showModal(title, message, isSuccess) {
    var modal = document.getElementById("attendanceModal");
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

  // Add Attendance Form Submission
  var addAttendanceForm = document.getElementById("addAttendanceForm");
  addAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const studentRollNo = document.getElementById("studentRollNo").value;
    const courseId = document.getElementById("courseId").value;
    const date = document.getElementById("attendanceDate").value;
    const attendanceStatus = document.getElementById("attendanceStatus").value;

    fetch(`${config.API_URL}/student-attendance`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        studentRollNo,
        courseId,
        date,
        attendanceStatus,
      }),
    })
      .then(async (response) => {
        if (response.ok) {
          return response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
        }
      })
      .then((data) => {
        showModal("Success", "Attendance added successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to add attendance: ${error.message}`, false);
      });
    addAttendanceForm.reset();
  });

  // Update Attendance Form Submission
  var updateAttendanceForm = document.getElementById("updateAttendanceForm");
  updateAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const attendanceId = document.getElementById("attendanceId").value;
    const status = document.getElementById("updateAttendanceStatus").value;

    fetch(
      `${config.API_URL}/student-attendance/${attendanceId}?attendanceStatus=${status}`,
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    )
      .then(async (response) => {
        if (response.ok) {
          return await response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
        }
      })
      .then((data) => {
        showModal("Success", "Attendance updated successfully!", true);
      })
      .catch((error) => {
        showModal(
          "Error",
          `Failed to update Attendance: ${error.message}`,
          false
        );
      });
    updateAttendanceForm.reset();
  });

  // Delete Attendance Form Submission
  var deleteAttendanceForm = document.getElementById("deleteAttendanceForm");
  deleteAttendanceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const deleteAttendanceId =
      document.getElementById("deleteAttendanceId").value;

    let api_url = `${config.API_URL}/student-attendance/${deleteAttendanceId}`;
    fetch(api_url, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then(async (response) => {
        if (response.ok) {
          return response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
        }
      })
      .then((data) => {
        showModal("Success", "Attendance deleted successfully!", true);
        populateAttendanceId("deleteAttendanceId");
      })
      .catch((error) => {
        showModal(
          "Error",
          `Failed to delete Attendance: ${error.message}`,
          false
        );
      });
    deleteAttendanceForm.reset();
  });

  async function populateAttendanceTable() {
    const tableBody = document.querySelector("#attendanceTable tbody");
    tableBody.innerHTML = "";

    let response = await fetch(`${config.API_URL}/student-attendance`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    let data = [];
    if (response.ok) {
      data = await response.json();
    }

    for (let index = 0; index < data.length; index++) {
      const attendance = data[index];

      const status =
        attendance.attendanceStatus == "Od"
          ? "On Duty"
          : attendance.attendanceStatus;

      const date = formatDate(attendance.date);

      const row = `
        <tr>
          <th scope="row">${index + 1}</th>
          <td>${attendance.id}</td>
          <td>${attendance.studentRollNo}</td>
          <td>${attendance.courseId}</td>
          <td>${date}</td>
          <td id="${attendance.attendanceStatus.toLowerCase()}"><p>${status}</p></td>
          <td><button class="btn btn-primary" onclick="viewAttendanceDetails(
            ${attendance.id})">View Details</button></td>
        </tr>
      `;
      tableBody.insertAdjacentHTML("beforeend", row);
    }

    $("#attendanceTable").DataTable().destroy();
    var groupColumn = 3;
    const table = $("#attendanceTable").DataTable({
      columnDefs: [
        { orderable: false, targets: 6 },
        { searchable: false, targets: 6 },
        { visible: false, targets: groupColumn },
      ],
      columns: [null, null, null, null, null, null, null],
      order: [
        [3, "asc"],
        [4, "desc"],
      ], // Sort by Course ID (4th column) and Date (5th column)
      rowGroup: {
        dataSrc: 3, // Group by the 4th column (Course ID)
      },
      pagingType: "full_numbers",
      drawCallback: function (settings) {
        var api = this.api();
        var rows = api.rows({ page: "current" }).nodes();
        var last = null;

        api
          .column(groupColumn, { page: "current" })
          .data()
          .each(function (group, i) {
            if (last !== group) {
              $(rows)
                .eq(i)
                .before(
                  '<tr class="group"><td colspan="6" style="background-color: #D2D2D2" class="fw-bold">' +
                    "Course " +
                    group +
                    "</td></tr>"
                );

              last = group;
            }
          });
      },
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

    // $("#attendanceTable tbody").on("click", "tr.group", function () {
    //   var currentOrder = table.order()[0];
    //   if (currentOrder[0] === groupColumn && currentOrder[1] === "asc") {
    //     table.order([groupColumn, "desc"]).draw();
    //   } else {
    //     table.order([groupColumn, "asc"]).draw();
    //   }
    // });

    $("#filterPresentStatus, #filterOnDutyStatus, #filterAbsentStatus").on(
      "change",
      function () {
        table.draw();
      }
    );

    $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
      const filterPresent = $("#filterPresentStatus").is(":checked");
      const filterOnDuty = $("#filterOnDutyStatus").is(":checked");
      const filterAbsent = $("#filterAbsentStatus").is(":checked");

      const status = data[5].toLowerCase();

      if (
        (filterPresent && status.toLowerCase().includes("present")) ||
        (filterAbsent && status.toLowerCase().includes("absent")) ||
        (filterOnDuty && status.toLowerCase().includes("on duty"))
      ) {
        return true;
      }
      return false;
    });

    table.draw();
  }
});

$(document).ready(function () {});
