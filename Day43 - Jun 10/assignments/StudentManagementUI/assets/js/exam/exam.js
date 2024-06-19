// Initialize AOS library
AOS.init({ duration: 1000 });

// Function to display exam details in the modal
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

// Function to format date as dd-mm-yyyy
function formatDate(dateString) {
  const date = new Date(dateString);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
}

// Function to get course name by ID
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

// Function to format time as 12-hour format with AM/PM
function formatTime(dateTime) {
  const date = new Date(dateTime);
  let hours = date.getHours();
  const minutes = String(date.getMinutes()).padStart(2, "0");
  const ampm = hours >= 12 ? "PM" : "AM";
  hours = hours % 12;
  hours = hours ? hours : 12; // the hour '0' should be '12'
  return `${hours}:${minutes} ${ampm}`;
}

// Function to show modal by ID
function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

document.addEventListener("DOMContentLoaded", function () {
  // Function to populate the DataTable with fetched exam data
  function populateExamTable(examData) {
    const tableBody = $("#examTable tbody");
    tableBody.empty();

    examData.forEach((exam, index) => {
      const row = `
        <tr>
          <td>${index + 1}</td>
          <td>${exam.examId}</td>
          <td>${exam.courseId}</td>
          <td>${formatDate(exam.examDate)}</td>
          <td>${exam.examType}</td>
          <td>
            <button class="btn btn-primary" onclick="viewExamDetails(${
              exam.examId
            })"
              >
              View Exam
            </button>
          </td>
        </tr>
      `;
      tableBody.append(row);
    });

    // Initialize DataTable
    const table = $("#examTable").DataTable({
      columnDefs: [{ orderable: false, targets: 5 }],
      columns: [
        null,
        null,
        null,
        { type: "date-dd-mmm-yyyy" },
        null,
        { searchable: false },
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

      const mode = data[4].toLowerCase();
      const examDate = new Date(data[3].split("-").reverse().join("-"));
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

  // API URL to fetch exam data
  const apiUrl = `${
    config.API_URL
  }/exams/studentRollNo?studentRollNo=${getStudentRollNo()}`;

  // Fetch exam data and populate the DataTable
  fetch(apiUrl)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to fetch exam data.");
      }
      return response.json();
    })
    .then((data) => {
      populateExamTable(data);
    })
    .catch((error) => {
      console.error("Error fetching exam data:", error);
    });
});
