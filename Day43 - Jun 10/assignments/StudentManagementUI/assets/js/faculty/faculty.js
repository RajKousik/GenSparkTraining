AOS.init({ duration: 1000 });

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

let userEmail = null;

function hideModal(modalId) {
  const updateModalElement = document.getElementById(modalId);
  const updateModal = bootstrap.Modal.getInstance(updateModalElement);
  updateModal.hide();
}

function showModalById(modalId) {
  const modalElement = new bootstrap.Modal(document.getElementById(modalId));
  modalElement.show();
}

function getRole(roleId) {
  let data = "Unknown";
  switch (roleId) {
    case 0:
      data = "Admin";
      break;
    case 1:
      data = "Assistant_Professor";
      break;
    case 2:
      data = "Associate_Professor";
      break;
    case 3:
      data = "Professor";
      break;
    case 4:
      data = "Head Of Department";
      break;
    case 5:
      data = "Student";
      break;
    default:
      data = "Unknown";
      break;
  }
  return data;
}

async function rejectFaculty() {
  let api_url = `${config.API_URL}/faculty/delete/${userEmail}`;

  let response = await fetch(api_url, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });
  "response :>> ", response;
  if (response.ok) {
    hideModal("facultyViewModal");
    showModal("Success", "Rejected Successfully", true);
    setTimeout(() => {
      location.reload();
    }, 3000);
  } else {
    let error = await response.json();
    hideModal("facultyViewModal");
    showModal(
      "Rejection Failed!",
      `Something Went Wrong: ${error.message}`,
      false
    );
  }
}

async function approveFaculty() {
  let api_url = `${config.API_URL}/admin/activate/faculty?email=${userEmail}`;

  let response = await fetch(api_url, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  });

  // Simulated API call
  if (response.ok) {
    hideModal("facultyViewModal");
    showModal("Success", "Approved Successfully", true);

    setTimeout(() => {
      location.reload();
    }, 3000);
  } else {
    let error = await response.json();
    hideModal("facultyViewModal");
    showModal(
      "Approval Failed!",
      `Something Went Wrong: ${error.message}`,
      false
    );
  }
}

async function viewFacultyDetails(facultyId) {
  try {
    const response = await fetch(`${config.API_URL}/faculty/${facultyId}`);
    const data = await response.json();
    userEmail = data.email;
    const approveBtn = document.getElementById("activeBtn");
    const rejectBtn = document.getElementById("rejectBtn");
    const closeBtn = document.getElementById("closeBtn");
    if (data) {
      document.getElementById("facultyId").textContent = data.facultyId;
      document.getElementById("facultyName").textContent = data.name;
      document.getElementById("facultyGender").textContent = data.gender;
      document.getElementById("facultyEmail").textContent = data.email;
      document.getElementById("facultyDepartment").textContent =
        await getDepartmentName(data.departmentId);
      document.getElementById("facultyAddress").textContent = data.address;
      document.getElementById("facultyDob").textContent = formatDate(data.dob);
      document.getElementById("facultyStatus").textContent =
        data.status == 1 ? "Active" : "Inactive";
      document.getElementById("facultyRole").textContent = getRole(data.role);

      if (getUserRole().toLowerCase() !== "admin") {
        approveBtn.style.display = "none";
        rejectBtn.style.display = "none";
        closeBtn.style.display = "block";
      } else if ($("#facultyStatus").text().toLowerCase() === "active") {
        approveBtn.style.display = "none";
        rejectBtn.style.display = "none";
        closeBtn.style.display = "block";
      } else {
        approveBtn.style.display = "block";
        rejectBtn.style.display = "block";
        closeBtn.style.display = "none";
      }

      showModalById("facultyViewModal");
    } else {
      console.error(`Faculty with ID ${facultyId} not found.`);
    }
  } catch (error) {
    console.error("Error fetching faculty details:", error);
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

function showModal(title, message, isSuccess) {
  var modal = document.getElementById("responseModal");
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

async function getDepartmentName(departmentId) {
  const response = await fetch(`${config.API_URL}/departments/${departmentId}`);

  if (response.ok) {
    const data = await response.json();
    return data.name;
  } else {
    console.error("Something went wrong while fetching department name");
  }
}

document.addEventListener("DOMContentLoaded", async function () {
  if (!checkToken()) {
    return;
  }
  const approveBtn = document.getElementById("activeBtn");
  const rejectBtn = document.getElementById("rejectBtn");

  approveBtn.addEventListener("click", approveFaculty);
  rejectBtn.addEventListener("click", rejectFaculty);

  async function fetchFaculties() {
    const role = getUserRole();
    let url;

    if (role.toLowerCase() === "admin") {
      url = `${config.API_URL}/faculty`;
    } else {
      const facultyId = getUserId();
      const facultyResponse = await fetch(
        `${config.API_URL}/faculty/${facultyId}`
      );
      const facultyData = await facultyResponse.json();
      const departmentId = facultyData.departmentId;
      url = `${config.API_URL}/faculty/department/${departmentId}`;
    }
    const token = getTokenFromLocalStorage();
    const response = await fetch(url, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    if (response.ok) {
      const data = await response.json();
      populateTable(data);
    } else {
      console.error("Error while fetching the data", await response.json());
    }
  }

  async function populateTable(facultyData) {
    const tableBody = $("#facultyTable tbody");
    tableBody.empty();

    for (const faculty of facultyData) {
      const departmentName = await getDepartmentName(faculty.departmentId);
      const status = faculty.status == 1 ? "Active" : "Inactive";
      const row = `
        <tr>
          <th scope="row">${facultyData.indexOf(faculty) + 1}</th>
          <td>${faculty.facultyId}</td>
          <td>${faculty.name}</td>
          <td>${faculty.email}</td>
          <td>${departmentName}</td>
          <td id=${status.toLowerCase()}><p>${status}</p></td>
          <td>
            <button class="btn btn-primary"
            onclick="viewFacultyDetails(${faculty.facultyId})">
              View Details
            </button>
          </td>
        </tr>
      `;
      tableBody.append(row);
    }

    const table = $("#facultyTable").DataTable({
      columns: [
        null,
        null,
        null,
        null,
        null,
        { orderable: false },
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

    $("#filterActive, #filterInactive").on("change", function () {
      table.draw();
    });

    $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
      const filterActive = $("#filterActive").is(":checked");
      const filterInactive = $("#filterInactive").is(":checked");
      const status = data[5].toLowerCase();

      if (
        (filterActive && status == "active") ||
        (filterInactive && status.includes("inactive"))
      ) {
        return true;
      }
      return false;
    });

    table.draw(); // Initial filter
  }

  fetchFaculties();
});
