function formatTime(dateTime) {
  const date = new Date(dateTime);
  let hours = date.getHours();
  const minutes = String(date.getMinutes()).padStart(2, "0");
  const ampm = hours >= 12 ? "PM" : "AM";
  hours = hours % 12;
  hours = hours ? hours : 12; // the hour '0' should be '12'
  return `${hours}:${minutes} ${ampm}`;
}

function formatDate(dateString) {
  const date = new Date(dateString.split("T")[0]);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
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

function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

async function viewExamDetails(examId) {
  try {
    const response = await fetch(`${config.API_URL}/exams/${examId}`);
    const data = await response.json();

    if (data) {
      document.getElementById("examIdView").textContent = data.examId;
      document.getElementById("courseIdView").textContent = data.courseId;
      document.getElementById("examDateView").textContent = formatDate(
        data.examDate
      );
      document.getElementById("examTypeView").textContent = data.examType;
      document.getElementById("totalMarkView").textContent = data.totalMark;
      document.getElementById("courseNameView").textContent =
        await getCourseNameById(data.courseId);
      document.getElementById("startTimeView").textContent = formatTime(
        data.startTime
      );
      document.getElementById("endTimeView").textContent = formatTime(
        data.endTime
      );
      showModalById("examViewModal");
    } else {
      console.error(`Exam with ID ${examId} not found.`);
    }
  } catch (error) {
    console.error("Error fetching exam details:", error);
  }
}

const addExamNav = document.getElementById("add-exam-nav");
const updateExamNav = document.getElementById("update-exam-nav");
const deleteExamNav = document.getElementById("delete-exam-nav");
const viewAllExamsNav = document.getElementById("view-all-exams-nav");

const addExamView = document.getElementById("add-exam-form");
const updateExamView = document.getElementById("update-exam-form");
const deleteExamView = document.getElementById("delete-exam-form");
const viewAllExamsView = document.getElementById("view-all-exams");

document.getElementById("modalUpdateBtn").addEventListener("click", () => {
  addExamView.classList.add("d-none");
  updateExamView.classList.remove("d-none");
  deleteExamView.classList.add("d-none");
  viewAllExamsView.classList.add("d-none");

  addExamNav.classList.remove("active");
  updateExamNav.classList.add("active");
  deleteExamNav.classList.remove("active");
  viewAllExamsNav.classList.remove("active");

  const examId = document.getElementById("examIdView").innerText;

  populateUpdateForm(examId);
});

document.getElementById("modalDeleteBtn").addEventListener("click", () => {
  addExamView.classList.add("d-none");
  updateExamView.classList.add("d-none");
  deleteExamView.classList.remove("d-none");
  viewAllExamsView.classList.add("d-none");

  addExamNav.classList.remove("active");
  updateExamNav.classList.remove("active");
  deleteExamNav.classList.add("active");
  viewAllExamsNav.classList.remove("active");

  const examId = document.getElementById("examIdView").innerText;
  document.getElementById("deleteExamId").value = examId;
});
function convertTimeFormat(timeStr) {
  timeStr = timeStr.split("T")[1];

  // Extract the parts of the time string
  const [time, modifier] = timeStr.split(" ");

  // Split the time into hours and minutes
  let [hours, minutes] = time.split(":");

  // Convert hours to number
  hours = parseInt(hours, 10);

  // Adjust hours based on AM/PM modifier
  if (modifier === "PM" && hours < 12) {
    hours += 12;
  } else if (modifier === "AM" && hours === 12) {
    hours = 0;
  }

  // Ensure hours are two digits
  hours = hours < 10 ? "0" + hours : hours;

  // Return the formatted time string in "HH:mm" format
  return `${hours}:${minutes}`;
}

async function populateUpdateForm(examId) {
  let response = await fetch(`${config.API_URL}/exams/${examId}`);

  let data;
  if (response.ok) {
    data = await response.json();
  }

  document.getElementById("examId").value = data.examId;
  document.getElementById("updateCourseId").value = data.courseId;
  document.getElementById("updateTotalMark").value = data.totalMark;
  document.getElementById("updateExamDate").value = data.examDate.split("T")[0];
  document.getElementById("updateExamType").value = data.examType.toLowerCase();
  document.getElementById("updateStartTime").value = convertTimeFormat(
    data.startTime
  );
  document.getElementById("updateEndTime").value = convertTimeFormat(
    data.endTime
  );
}

document.addEventListener("DOMContentLoaded", function () {
  AOS.init({ duration: 1000 });

  if (window.top === window.self) {
    // If the page is not in an iframe, redirect to the main page or show an error
    window.location.href = "../../../src/pages/admin/index.html";
  }

  if (!checkToken()) {
    return;
  }

  const token = getTokenFromLocalStorage();

  populateCourseId("courseId");
  populateCourseId("updateCourseId");
  populateExamId("examId");
  populateExamId("deleteExamId");

  addExamNav.addEventListener("click", () => {
    addExamView.classList.remove("d-none");
    updateExamView.classList.add("d-none");
    deleteExamView.classList.add("d-none");
    viewAllExamsView.classList.add("d-none");

    populateCourseId("courseId");

    addExamNav.classList.add("active");
    updateExamNav.classList.remove("active");
    deleteExamNav.classList.remove("active");
    viewAllExamsNav.classList.remove("active");
  });

  updateExamNav.addEventListener("click", () => {
    addExamView.classList.add("d-none");
    updateExamView.classList.remove("d-none");
    deleteExamView.classList.add("d-none");
    viewAllExamsView.classList.add("d-none");

    populateCourseId("updateCourseId");
    populateExamId("examId");

    addExamNav.classList.remove("active");
    updateExamNav.classList.add("active");
    deleteExamNav.classList.remove("active");
    viewAllExamsNav.classList.remove("active");
  });

  deleteExamNav.addEventListener("click", () => {
    addExamView.classList.add("d-none");
    updateExamView.classList.add("d-none");
    deleteExamView.classList.remove("d-none");
    viewAllExamsView.classList.add("d-none");

    populateExamId("deleteExamId");

    addExamNav.classList.remove("active");
    updateExamNav.classList.remove("active");
    deleteExamNav.classList.add("active");
    viewAllExamsNav.classList.remove("active");
  });

  viewAllExamsNav.addEventListener("click", () => {
    addExamView.classList.add("d-none");
    updateExamView.classList.add("d-none");
    deleteExamView.classList.add("d-none");
    viewAllExamsView.classList.remove("d-none");

    populateExamTable();

    addExamNav.classList.remove("active");
    updateExamNav.classList.remove("active");
    deleteExamNav.classList.remove("active");
    viewAllExamsNav.classList.add("active");
  });

  // Function to show modal with dynamic content
  function showModal(title, message, isSuccess) {
    var modal = document.getElementById("examModal");
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
          option.textContent = course.courseId + " - " + course.name;
          courseSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching courses:", error);
      });
  }

  function populateExamId(elementId) {
    fetch(`${config.API_URL}/exams`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const examSelect = document.getElementById(elementId);
        examSelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select Exam Id";
        option.disabled = true;
        option.selected = true;
        examSelect.appendChild(option);

        data.forEach((exam) => {
          const option = document.createElement("option");
          option.value = exam.examId;
          option.textContent = exam.examId;
          examSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching courses:", error);
      });
  }

  // Add Exam Form Submission
  var addExamForm = document.getElementById("addExamForm");
  addExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const courseId = document.getElementById("courseId").value;
    const totalMark = document.getElementById("totalMark").value;
    const examDate = document.getElementById("examDate").value;
    const examType = document.getElementById("examType").value;
    let startTime = document.getElementById("startTime").value;
    let endTime = document.getElementById("endTime").value;

    const token = getTokenFromLocalStorage();

    // Convert time values to "HH:mm:ss" format
    startTime = `${startTime}:00`;
    endTime = `${endTime}:00`;

    fetch(`${config.API_URL}/exams`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        courseId,
        totalMark,
        examDate,
        examType,
        startTime,
        endTime,
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
        showModal("Success", "Exam added successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to add exam: ${error.message}`, false);
      });
    addExamForm.reset();
  });

  // Update Exam Form Submission
  var updateExamForm = document.getElementById("updateExamForm");
  updateExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const examId = document.getElementById("examId").value;
    const courseId = document.getElementById("updateCourseId").value;
    const totalMark = document.getElementById("updateTotalMark").value;
    const examDate = document.getElementById("updateExamDate").value;
    const examType = document.getElementById("updateExamType").value;
    let startTime = document.getElementById("updateStartTime").value;
    let endTime = document.getElementById("updateEndTime").value;

    const token = getTokenFromLocalStorage();

    // Convert time values to "HH:mm:ss" format
    startTime = `${startTime}:00`;
    endTime = `${endTime}:00`;

    let api_url = `${config.API_URL}/exams/${examId}`;
    fetch(api_url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        courseId,
        totalMark,
        examDate,
        examType,
        startTime,
        endTime,
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
        showModal("Success", "Exam updated successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to update exam: ${error.message}`, false);
      });
    updateExamForm.reset();
  });

  // Delete Exam Form Submission
  var deleteExamForm = document.getElementById("deleteExamForm");
  deleteExamForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const deleteExamId = document.getElementById("deleteExamId").value;

    let api_url = `${config.API_URL}/exams/${deleteExamId}`;
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
        showModal("Success", "Exam deleted successfully!", true);
        populateExamId("deleteExamId");
      })
      .catch((error) => {
        showModal("Error", `Failed to delete exam: ${error.message}`, false);
      });
    deleteExamForm.reset();
  });

  // function formatDate(dateString) {
  //   const date = new Date(dateString.split("T")[0]);
  //   const day = String(date.getDate()).padStart(2, "0");
  //   const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
  //   const year = date.getFullYear();
  //   return `${day}-${month}-${year}`;
  // }

  async function populateExamTable() {
    // Initialize DataTable for Exams
    const tableBody = document.querySelector("#examTable tbody");
    tableBody.innerHTML = "";

    let response = await fetch(`${config.API_URL}/exams`);

    let data = [];
    if (response.ok) {
      data = await response.json();
    }

    for (let index = 0; index < data.length; index++) {
      const exam = data[index];
      // const statusText = getStatusText(registration.approvalStatus);
      // const courseName = await getCourseNameById(registration.courseId);
      const examDate = formatDate(exam.examDate);
      const row = `
        <tr>
          <th scope="row">${index + 1}</th>
          <td>${exam.examId}</td>
          <td>${exam.courseId}</td>
          <td>${exam.totalMark}</td>
          <td>${examDate}</td>
          <td>${exam.examType}</td>
          <td>
            <button class="btn btn-primary" onclick="viewExamDetails(${
              exam.examId
            })">
              View Details
            </button>
          </td>
        </tr>
      `;
      tableBody.insertAdjacentHTML("beforeend", row);
    }

    $("#examTable").DataTable().destroy();
    const table = $("#examTable").DataTable({
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
          '<option value="5">5</option>' +
          '<option value="10">10</option>' +
          '<option value="15">15</option>' +
          '<option value="20">20</option>' +
          '<option value="25">25</option>' +
          '<option value="-1">All</option>' +
          "</select> results",
      },
    });

    // Event listeners for filter checkboxes
    $("#filterUpcomingExams, #filterOnline, #filterOffline").on(
      "change",
      function () {
        table.draw();
      }
    );

    // Custom filtering function for DataTable
    $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
      const filterUpcomingExams = $("#filterUpcomingExams").is(":checked");
      const filterOnline = $("#filterOnline").is(":checked");
      const filterOffline = $("#filterOffline").is(":checked");

      const mode = data[5].toLowerCase();
      const examDate = new Date(data[4].split("-").reverse().join("-"));
      const today = new Date();

      if (
        ((filterOnline && mode.includes("online")) ||
          (filterOffline && mode.includes("offline"))) &&
        (!filterUpcomingExams || examDate >= today)
      ) {
        return true;
      }
      return false;
    });
    table.draw();
  }
});
