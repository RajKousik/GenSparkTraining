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

function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
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

async function viewGradeDetails(gradeId) {
  try {
    const response = await fetch(`${config.API_URL}/grades/${gradeId}`);
    const data = await response.json();

    const result =
      data.studentGrade === "F" ||
      data.studentGrade === "UA" ||
      data.studentGrade === "RA"
        ? "Fail"
        : "Pass";
    const gradeDisplay = data.studentGrade.replace("_Plus", "+");
    if (data) {
      document.getElementById("studentIdModal").textContent = data.studentId;
      document.getElementById("courseIdModal").textContent =
        await getCourseIdByExamId(data.examId);
      document.getElementById("courseNameModal").textContent =
        await getCourseNameById(
          document.getElementById("courseIdModal").textContent
        );
      document.getElementById("examIdModal").textContent = data.examId;
      document.getElementById("marksScoredModal").textContent =
        data.marksScored;
      document.getElementById("evaluatedByModal").textContent =
        data.evaluatedById;
      document.getElementById("percentageModal").textContent =
        data.percentage + "%";
      document.getElementById("studentGradeModal").textContent = gradeDisplay;
      document.getElementById("resultModal").textContent = result;
      document.getElementById("commentsModal").textContent = data.comments;
      showModalById("gradeViewModal");
    } else {
      console.error(`Grade with ID ${data.id} not found.`);
    }
  } catch (error) {
    console.error("Error fetching grade details:", error);
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

  populateStudentId("studentId");
  populateFaculty("evaluatedBy");
  populateExamId("examId");

  populateFaculty("updateEvaluatedBy");
  populateGrade("updateGradeId");

  populateGrade("deleteGradeId");

  const addGradeNav = document.getElementById("add-grade-nav");
  const updateGradeNav = document.getElementById("update-grade-nav");
  const deleteGradeNav = document.getElementById("delete-grade-nav");
  const viewAllGradesNav = document.getElementById("view-all-grades-nav");

  const addGradeView = document.getElementById("add-grade-form");
  const updateGradeView = document.getElementById("update-grade-form");
  const deleteGradeView = document.getElementById("delete-grade-form");
  const viewAllGradesView = document.getElementById("view-all-grades");

  addGradeNav.addEventListener("click", () => {
    addGradeView.classList.remove("d-none");
    updateGradeView.classList.add("d-none");
    deleteGradeView.classList.add("d-none");
    viewAllGradesView.classList.add("d-none");

    populateStudentId("studentId");
    populateFaculty("evaluatedBy");
    populateExamId("examId");

    addGradeNav.classList.add("active");
    updateGradeNav.classList.remove("active");
    deleteGradeNav.classList.remove("active");
    viewAllGradesNav.classList.remove("active");
  });

  updateGradeNav.addEventListener("click", () => {
    addGradeView.classList.add("d-none");
    updateGradeView.classList.remove("d-none");
    deleteGradeView.classList.add("d-none");
    viewAllGradesView.classList.add("d-none");

    populateFaculty("updateEvaluatedBy");
    populateGrade("updateGradeId");

    addGradeNav.classList.remove("active");
    updateGradeNav.classList.add("active");
    deleteGradeNav.classList.remove("active");
    viewAllGradesNav.classList.remove("active");
  });

  deleteGradeNav.addEventListener("click", () => {
    addGradeView.classList.add("d-none");
    updateGradeView.classList.add("d-none");
    deleteGradeView.classList.remove("d-none");
    viewAllGradesView.classList.add("d-none");

    populateGrade("deleteGradeId");

    addGradeNav.classList.remove("active");
    updateGradeNav.classList.remove("active");
    deleteGradeNav.classList.add("active");
    viewAllGradesNav.classList.remove("active");
  });

  viewAllGradesNav.addEventListener("click", () => {
    addGradeView.classList.add("d-none");
    updateGradeView.classList.add("d-none");
    deleteGradeView.classList.add("d-none");
    viewAllGradesView.classList.remove("d-none");

    populateGradeTable();

    addGradeNav.classList.remove("active");
    updateGradeNav.classList.remove("active");
    deleteGradeNav.classList.remove("active");
    viewAllGradesNav.classList.add("active");
  });

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

  function populateGrade(elementId) {
    fetch(`${config.API_URL}/grades`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        const gradeSelect = document.getElementById(elementId);
        gradeSelect.innerHTML = "";

        const option = document.createElement("option");
        option.value = "";
        option.textContent = "Select Grade Id";
        option.disabled = true;
        option.selected = true;
        gradeSelect.appendChild(option);

        data.forEach((grade) => {
          const option = document.createElement("option");
          option.value = grade.id;
          option.textContent = grade.id;
          gradeSelect.appendChild(option);
        });
      })
      .catch((error) => {
        console.error("Error fetching grades:", error);
      });
  }

  function populateStudentId(elementId, examId = "") {
    const apiUrl = examId
      ? `${config.API_URL}/exams/examId?examId=${examId}`
      : `${config.API_URL}/students/all`;

    fetch(apiUrl, {
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

  // // Event listener for student roll number selection
  // document.getElementById("studentId").addEventListener("change", function () {
  //   const studentRollNo = this.value;
  //   populateExamId("examId", studentRollNo);
  // });

  // Event listener for examId selection
  document.getElementById("examId").addEventListener("change", function () {
    const examId = this.value;
    populateStudentId("studentId", examId);
  });

  function populateExamId(elementId, studentRollNo = "") {
    const apiUrl = studentRollNo
      ? `${config.API_URL}/exams/studentRollNo?studentRollNo=${studentRollNo}`
      : `${config.API_URL}/exams`;
    fetch(apiUrl, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.ok) return response.json();
        else return [];
      })
      .then((data) => {
        if (data) {
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
        }
      })
      .catch((error) => {
        console.error("Error fetching courses:", error);
      });
  }

  // Function to show modal with dynamic content
  function showModal(title, message, isSuccess) {
    var modal = document.getElementById("gradeModal");
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

  // Add Grade Form Submission
  var addGradeForm = document.getElementById("addGradeForm");
  addGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const studentId = document.getElementById("studentId").value;
    const examId = document.getElementById("examId").value;
    const evaluatedById = document.getElementById("evaluatedBy").value;
    const marksScored = document.getElementById("marksScored").value;
    const comments = document.getElementById("comments").value;

    fetch(`${config.API_URL}/grades`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        studentId,
        examId,
        evaluatedById,
        marksScored,
        comments,
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
        showModal("Success", "Grade added successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to add grade: ${error.message}`, false);
      });
    addGradeForm.reset();
  });

  // Update Grade Form Submission
  var updateGradeForm = document.getElementById("updateGradeForm");
  updateGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const gradeId = document.getElementById("updateGradeId").value;
    const evaluatedById = document.getElementById("updateEvaluatedBy").value;
    const marksScored = document.getElementById("updateMarksScored").value;
    const comments = document.getElementById("updateComments").value;

    fetch(`${config.API_URL}/grades/${gradeId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        evaluatedById,
        marksScored,
        comments,
      }),
    })
      .then(async (response) => {
        if (response.ok) {
          return await response.json();
        } else {
          let data = await response.json();
          throw new Error(data.message || Object.values(data.errors)[0]);
        }
      })
      .then((data) => {
        showModal("Success", "Grade updated successfully!", true);
      })
      .catch((error) => {
        showModal("Error", `Failed to update grade: ${error.message}`, false);
      });
    updateGradeForm.reset();
  });

  // Delete Grade Form Submission
  var deleteGradeForm = document.getElementById("deleteGradeForm");
  deleteGradeForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const deleteGradeId = document.getElementById("deleteGradeId").value;

    let api_url = `${config.API_URL}/grades/${deleteGradeId}`;
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
        showModal("Success", "Grade deleted successfully!", true);
        populateGrade("deleteGradeId");
      })
      .catch((error) => {
        showModal("Error", `Failed to delete Grade: ${error.message}`, false);
      });
    deleteGradeForm.reset();
  });

  async function populateGradeTable() {
    const tableBody = document.querySelector("#gradeTable tbody");
    tableBody.innerHTML = "";

    let response = await fetch(`${config.API_URL}/grades`, {
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
      const grade = data[index];

      const result =
        grade.studentGrade === "F" ||
        grade.studentGrade === "UA" ||
        grade.studentGrade === "RA"
          ? "Fail"
          : "Pass";
      const gradeDisplay = grade.studentGrade.replace("_Plus", "+");

      const row = `
        <tr>
          <th scope="row">${index + 1}</th>
          <td>${grade.id}</td>
          <td>${grade.examId}</td>
          <td>${grade.studentId}</td>
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
      tableBody.insertAdjacentHTML("beforeend", row);
    }

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

    $("#gradeTable").DataTable().destroy();
    // Initialize the DataTable
    const table = $("#gradeTable").DataTable({
      // Disable sorting on the last column
      columnDefs: [
        { orderable: false, targets: 6 },
        { type: "grade-order", targets: 4 }, // Apply custom sorting to the grade column
      ],
      columns: [null, null, null, null, null, null, { searchable: false }],
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
});
