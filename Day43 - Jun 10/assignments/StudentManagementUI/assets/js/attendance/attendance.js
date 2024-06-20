AOS.init({ duration: 1000 });

document.addEventListener("DOMContentLoaded", async function () {
  // Function to fetch data from the API
  async function fetchData() {
    const response = await fetch(
      `${config.API_URL}/student-attendance/attendance-percentage/12`
    );
    console.log("response :>> ", response);
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
  await updateCharts();
});
