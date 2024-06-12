AOS.init({ duration: 1000 });

function filterExams() {
  // Call searchExams to apply both search and filter
  searchExams();
}

function searchExams() {
  const upcomingCheckbox = document.getElementById("filterUpcomingExams");
  const onlineCheckbox = document.getElementById("filterOnline");
  const offlineCheckbox = document.getElementById("filterOffline");

  const isUpcomingChecked = upcomingCheckbox.checked;
  const isOnlineChecked = onlineCheckbox.checked;
  const isOfflineChecked = offlineCheckbox.checked;

  const input = document.getElementById("searchInput");
  const filter = input.value.toLowerCase();
  const table = document.getElementById("examTable");
  const rows = table.getElementsByTagName("tr");

  const currentDate = new Date();

  for (let i = 1; i < rows.length; i++) {
    const cells = rows[i].getElementsByTagName("td");
    const examIdElement = cells[0];
    const courseIdElement = cells[1];
    const examDateElement = cells[2];
    const modeElement = cells[3];

    const examId = examIdElement.innerText.toLowerCase();
    const courseId = courseIdElement.innerText.toLowerCase();
    const examDate = new Date(examDateElement.innerText);
    const modeText = modeElement ? modeElement.innerText.toLowerCase() : "";

    const matchesSearch = examId.includes(filter) || courseId.includes(filter);

    let matchesFilter = false;

    // Check if no filter is applied
    if (!isUpcomingChecked && !isOnlineChecked && !isOfflineChecked) {
      matchesFilter = true;
    } else if (isUpcomingChecked && isOnlineChecked && isOfflineChecked) {
      if (
        examDate > currentDate &&
        (modeText.includes("online") || modeText.includes("offline"))
      ) {
        matchesFilter = true;
      }
    } else if (isUpcomingChecked && isOnlineChecked) {
      if (examDate > currentDate && modeText.includes("online")) {
        matchesFilter = true;
      }
    } else if (isUpcomingChecked && isOfflineChecked) {
      if (examDate > currentDate && modeText.includes("offline")) {
        matchesFilter = true;
      }
    }
    // Check if the exam mode matches the selected checkboxes
    else if (
      (isUpcomingChecked && examDate > currentDate) ||
      (isOnlineChecked && !isUpcomingChecked && modeText.includes("online")) ||
      (isOfflineChecked && !isUpcomingChecked && modeText.includes("offline"))
    ) {
      matchesFilter = true;
    }

    const shouldDisplayRow = matchesSearch && matchesFilter;

    rows[i].style.display = shouldDisplayRow ? "" : "none";
  }
}
