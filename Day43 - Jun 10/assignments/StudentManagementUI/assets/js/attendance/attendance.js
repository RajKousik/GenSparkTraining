AOS.init({ duration: 1000 });

const overallAttendanceNav = document.getElementById("overall-attendance-nav");
const attendanceReportNav = document.getElementById("attendance-report-nav");

const attendanceReportView = document.getElementById("attendanceReportView");
const overallAttendanceView = document.getElementById("view-all-attendance");

function formatDate(dateString) {
  const date = new Date(dateString.split("T")[0]);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
}

document.addEventListener("DOMContentLoaded", async function () {
  if (!checkToken()) {
    return;
  }

  if (window.top === window.self) {
    // If the page is not in an iframe, redirect to the main page or show an error
    window.location.href = "../../../src/pages/admin/index.html";
  }

  async function populateAttendanceTable() {
    const tableBody = document.querySelector("#attendanceTable tbody");
    tableBody.innerHTML = "";

    let response = await fetch(
      `${config.API_URL}/student-attendance/student/${getUserId()}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );
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
          <td>${attendance.courseId}</td>
          <td>${date}</td>
          <td id="${attendance.attendanceStatus.toLowerCase()}"><p>${status}</p></td>
        </tr>
      `;
      tableBody.insertAdjacentHTML("beforeend", row);
    }

    $("#attendanceTable").DataTable().destroy();
    var groupColumn = 2;
    const table = $("#attendanceTable").DataTable({
      // columnDefs: [{ orderable: false, targets: 5 }],
      columnDefs: [{ visible: false, targets: groupColumn }],
      // order: [[groupColumn, "asc"]],
      columns: [null, null, null, null, null],
      order: [
        [groupColumn, "asc"],
        [3, "desc"],
      ],
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
                  '<tr class="group"><td colspan="4" style="background-color: #D2D2D2" class="fw-bold">' +
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

    $("#attendanceTable tbody").on("click", "tr.group", function () {
      var currentOrder = table.order()[0];
      if (currentOrder[0] === groupColumn && currentOrder[1] === "asc") {
        table.order([groupColumn, "asc"]).draw();
      } else {
        table.order([groupColumn, "desc"]).draw();
      }
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

      const status = data[4].toLowerCase();

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

  overallAttendanceNav.addEventListener("click", () => {
    overallAttendanceView.classList.remove("d-none");
    attendanceReportView.classList.add("d-none");
    populateAttendanceTable();
    overallAttendanceNav.classList.add("active");
    attendanceReportNav.classList.remove("active");
  });

  attendanceReportNav.addEventListener("click", () => {
    overallAttendanceView.classList.add("d-none");
    attendanceReportView.classList.remove("d-none");
    updateCharts();
    overallAttendanceNav.classList.remove("active");
    attendanceReportNav.classList.add("active");
  });

  // Function to fetch data from the API
  async function fetchData() {
    const response = await fetch(
      `${
        config.API_URL
      }/student-attendance/attendance-percentage/${getUserId()}`
    );
    // return response;
    let data;
    if (response.ok) {
      data = await response.json();
    } else {
      const error = await response.json();
      console.error("Error while fetching the data", error.message);
      return [];
    }
    return data;
  }

  // Function to create a chart for a given course
  function createChart(courseId, attendancePercentage) {
    const chartContainer = document.getElementById("chartContainer");
    const colDiv = document.createElement("div");
    colDiv.setAttribute("data-aos", "fade-up");
    colDiv.className = "col-lg-6 col-md-12 d-flex align-items-stretch";

    const cardDiv = document.createElement("div");
    cardDiv.className = "card chart-container p-2 w-100"; // Ensure the chart container takes full width

    const canvas = document.createElement("canvas");
    canvas.id = `chart-${courseId}`;
    cardDiv.appendChild(canvas);
    colDiv.appendChild(cardDiv);
    chartContainer.appendChild(colDiv);

    const ctx = canvas.getContext("2d");
    new Chart(ctx, {
      type: "doughnut",
      data: {
        labels: ["Present", "Absent"],
        datasets: [
          {
            label: "Attendance",
            data: [attendancePercentage, 100 - attendancePercentage],
            backgroundColor: ["#2ECC40", "#FF4136"],
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false, // Allow chart to adapt to container size
        plugins: {
          title: {
            display: true,
            text: `Course ID: ${courseId}`,
            font: {
              size: 24, // Increase the font size of the title
            },
          },
          legend: {
            position: "top",
            labels: {
              font: {
                size: 18, // Increase the font size of the legend labels
              },
            },
          },
        },
      },
    });
  }

  // Function to update the charts with fetched data
  async function updateCharts() {
    const data = await fetchData();
    document.getElementById("chartContainer").innerHTML = "";
    if (data.length > 0) {
      document.getElementById("message").innerText = null;
      data.forEach((course) => {
        createChart(course.courseId, course.attendancePercentage);
      });
    } else {
      document.getElementById("message").innerText =
        "No Attendance Records Found As of Now!";
    }
  }

  // Call the function to update the charts when the page loads
  await populateAttendanceTable();
  // await updateCharts();
});
