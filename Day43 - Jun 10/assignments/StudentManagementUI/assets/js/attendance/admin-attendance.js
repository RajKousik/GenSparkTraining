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
  populateStudentId("studentRollNo");
  populateCourseId("courseId");

  populateAttendanceId("attendanceId");

  populateAttendanceId("deleteAttendanceId");

  const addAttendanceNav = document.getElementById("add-attendance-nav");
  const updateAttendanceNav = document.getElementById("update-attendance-nav");
  const deleteAttendanceNav = document.getElementById("delete-attendance-nav");
  const viewAllAttendanceNav = document.getElementById(
    "view-all-attendance-nav"
  );

  const addAttendanceView = document.getElementById("add-attendance-form");
  const updateAttendanceView = document.getElementById(
    "update-attendance-form"
  );
  const deleteAttendanceView = document.getElementById(
    "delete-attendance-form"
  );
  const viewAllAttendanceView = document.getElementById("view-all-attendance");

  addAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.remove("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.add("d-none");

    populateStudentId("studentRollNo");
    populateCourseId("courseId");

    addAttendanceNav.classList.add("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.remove("active");
  });

  updateAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.remove("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.add("d-none");

    populateAttendanceId("attendanceId");

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.add("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.remove("active");
  });

  deleteAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.remove("d-none");
    viewAllAttendanceView.classList.add("d-none");

    populateAttendanceId("deleteAttendanceId");

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.add("active");
    viewAllAttendanceNav.classList.remove("active");
  });

  viewAllAttendanceNav.addEventListener("click", () => {
    addAttendanceView.classList.add("d-none");
    updateAttendanceView.classList.add("d-none");
    deleteAttendanceView.classList.add("d-none");
    viewAllAttendanceView.classList.remove("d-none");

    populateAttendanceTable();

    addAttendanceNav.classList.remove("active");
    updateAttendanceNav.classList.remove("active");
    deleteAttendanceNav.classList.remove("active");
    viewAllAttendanceNav.classList.add("active");
  });

  document
    .getElementById("studentRollNo")
    .addEventListener("change", function () {
      const studentRollNo = this.value;
      populateCourseId("courseId", studentRollNo);
    });

  function formatDate(dateString) {
    const date = new Date(dateString.split("T")[0]);
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
  }

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
        </tr>
      `;
      tableBody.insertAdjacentHTML("beforeend", row);
    }

    $("#attendanceTable").DataTable().destroy();

    const table = $("#attendanceTable").DataTable({
      // columnDefs: [{ orderable: false, targets: 5 }],
      columns: [null, null, null, null, null, null],
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
