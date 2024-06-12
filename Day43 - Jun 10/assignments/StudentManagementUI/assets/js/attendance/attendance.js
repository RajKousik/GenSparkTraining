AOS.init({ duration: 1000 });

document.addEventListener("DOMContentLoaded", function () {
  // Function to fetch data from the API
  async function fetchData() {
    return [
      {
        courseId: 5,
        attendancePercentage: 66.66666666666666,
      },
      {
        courseId: 10,
        attendancePercentage: 91.88888888888888,
      },
      {
        courseId: 12,
        attendancePercentage: 61.88888888888888,
      },
      {
        courseId: 13,
        attendancePercentage: 70.88888888888888,
      },
      {
        courseId: 14,
        attendancePercentage: 80.88888888888888,
      },
    ];
  }

  // Function to create a chart for a given course
  function createChart(courseId, attendancePercentage) {
    const chartContainer = document.getElementById("chartContainer");
    const colDiv = document.createElement("div");
    colDiv.className = "col-lg-6 col-md-12 d-flex align-items-stretch"; // Flexbox for equal height

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
    if (data.length > 0) {
      data.forEach((course) => {
        createChart(course.courseId, course.attendancePercentage);
      });
    }
  }

  // Call the function to update the charts when the page loads
  updateCharts();
});
