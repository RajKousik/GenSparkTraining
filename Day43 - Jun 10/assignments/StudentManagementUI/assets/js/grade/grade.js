AOS.init({ duration: 1000 });

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

// Function to show modal by ID
function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
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

async function getCourseIdByExamId(examId) {
  try {
    const response = await fetch(`${config.API_URL}/exams/${examId}`);
    const gradeData = await response.json();
    return gradeData.courseId;
  } catch (error) {
    console.error(`Error fetching course Id for examId ${examId}:`, error);
    return "Unknown Course";
  }
}

async function viewGradeDetails(examId) {
  try {
    const response = await fetch(`${config.API_URL}/grades/${examId}`);
    const data = await response.json();
    const result =
      data.studentGrade === "F" ||
      data.studentGrade === "UA" ||
      data.studentGrade === "RA"
        ? "Fail"
        : "Pass";
    const gradeDisplay = data.studentGrade.replace("_Plus", "+");
    if (data) {
      document.getElementById("studentId").textContent = data.studentId;
      document.getElementById("courseId").textContent =
        await getCourseIdByExamId(data.examId);
      document.getElementById("courseName").textContent =
        await getCourseNameById(
          document.getElementById("courseId").textContent
        );
      document.getElementById("examId").textContent = data.examId;
      document.getElementById("marksScored").textContent = data.marksScored;
      document.getElementById("evaluatedBy").textContent = data.evaluatedById;
      document.getElementById("percentage").textContent = data.percentage + "%";
      document.getElementById("studentGrade").textContent = gradeDisplay;
      document.getElementById("result").textContent = result;
      document.getElementById("comments").textContent = data.comments;
      showModalById("gradeViewModal");
    } else {
      console.error(`Grade with ID ${data.id} not found.`);
    }
  } catch (error) {
    console.error("Error fetching grade details:", error);
  }
}

document.addEventListener("DOMContentLoaded", function () {
  if (!checkToken()) {
    return;
  }
  // Function to populate the DataTable with fetched grade data
  async function populateGradeTable(gradeData) {
    const tableBody = $("#gradeTable tbody");
    tableBody.empty();

    for (const grade of gradeData) {
      const result =
        grade.studentGrade === "F" ||
        grade.studentGrade === "UA" ||
        grade.studentGrade === "RA"
          ? "Fail"
          : "Pass";
      const gradeDisplay = grade.studentGrade.replace("_Plus", "+");

      const courseId = await getCourseIdByExamId(grade.examId);

      const row = `
        <tr>
          <th scope="row">${gradeData.indexOf(grade) + 1}</th>
          <td>${grade.examId}</td>
          <td>${courseId}</td>
          <td>${gradeDisplay}</td>
          <td id="${result.toLowerCase()}"><p>${result}</p></td>
          <td>
            <button class="btn btn-primary" onclick="viewGradeDetails(${
              grade.id
            })">
              View Details
            </button>
          </td>
        </tr>
      `;
      tableBody.append(row);
    }

    // Initialize DataTable
    const gradeOrder = {
      O: 1,
      "A+": 2,
      A: 3,
      "B+": 4,
      B: 5,
      C: 6,
      F: 7,
      UA: 8,
      RA: 9,
    };

    jQuery.fn.dataTable.ext.type.order["grade-order-pre"] = function (d) {
      return gradeOrder[d] || 10; // default to a high number for unrecognized grades
    };

    // Initialize the DataTable
    const table = $("#gradeTable").DataTable({
      // Disable sorting on the last column
      columnDefs: [
        { orderable: false, targets: 5 },
        { type: "grade-order", targets: 3 }, // Apply custom sorting to the grade column
      ],
      columns: [null, null, null, null, null, { searchable: false }],
      pagingType: "full_numbers",
      // Set default page length
      pageLength: 10,
      language: {
        // Customize pagination prev and next buttons: use arrows instead of words
        paginate: {
          previous: '<span class="fa fa-chevron-left"></span>',
          next: '<span class="fa fa-chevron-right"></span>',
          first: '<span class="fa-solid fa-angles-left"></span>',
          last: '<span class="fa-solid fa-angles-right"></span>',
        },
        // Customize number of elements to be displayed
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

    table.draw();
  }

  // API URL to fetch grade data
  const apiUrl = `${config.API_URL}/grades/student/${getUserId()}`;

  // Fetch grade data and populate the DataTable
  fetch(apiUrl)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to fetch grade data.");
      }
      return response.json();
    })
    .then(async (data) => {
      await populateGradeTable(data);
    })
    .catch((error) => {
      console.error("Error fetching grade data:", error);
    });
});
