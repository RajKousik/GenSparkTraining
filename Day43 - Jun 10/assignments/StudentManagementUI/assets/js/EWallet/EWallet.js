AOS.init({ duration: 1000 });

if (window.top === window.self) {
  // If the page is not in an iframe, redirect to the main page or show an error
  window.location.href = "../../../src/pages/admin/index.html";
}

async function getWalletAmount() {
  var api_url = `${
    config.API_URL
  }/students/EWallet?studentRollNo=${getUserId()}`;

  var response = await fetch(api_url);
  let current_amount = 0;
  if (response.ok) {
    var data = await response.json();
    current_amount = data;
  } else {
    const error = await response.json();
    console.error("Error while fetching the data", error.message);
    current_amount = 0;
  }
  document.getElementById("current_amount").value = current_amount;
}

document.addEventListener("DOMContentLoaded", async function () {
  if (!checkToken()) {
    return;
  }
  getWalletAmount();
});

const rechargeBtn = document.getElementById("rechargeBtn");

document
  .getElementById("rechargeForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();
    //
    await recharge();
  });

async function recharge() {
  const amount = document.getElementById("amount").value;
  const password = document.getElementById("password").value;

  var api_url = `${config.API_URL}/students/recharge`;
  const requestBody = {
    studentId: getUserId(),
    RechargeAmount: amount,
    password: password,
  };
  var response = await fetch(api_url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(requestBody),
  });

  if (response.ok) {
    var data = await response.json();
    rechargeBtn.innerHTML = `Recharging...
    <div class="spinner-border spinner-border-sm" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>`;
    setTimeout(() => {
      getWalletAmount();
      showMessage("Recharge successful!", "alert-success");
      rechargeBtn.innerHTML = `Recharge`;
      document.getElementById("rechargeForm").reset();
    }, 3000);
  } else {
    const error = await response.json();
    console.error("Error while fetching the data", error.message);
    showMessage(`Recharge failed! ${error.message}`, "alert-danger");
    return 0;
  }
}

function showMessage(message, alertClass) {
  const messageDiv = document.getElementById("message");
  messageDiv.innerHTML = `<div class="alert ${alertClass} p-1 text-center">${message}</div>`;
  setTimeout(() => {
    messageDiv.firstChild.classList.add("fade-out");
    setTimeout(() => {
      messageDiv.innerHTML = "";
    }, 2000); // Matches the duration of the fade-out transition
  }, 3000); // Display the message for 3 seconds before starting fade out
}

document.querySelectorAll(".password-icon").forEach((icon) => {
  icon.addEventListener("click", togglePasswordVisibility);
});

function togglePasswordVisibility(event) {
  const icon = event.target;
  const input =
    this.previousElementSibling.tagName === "INPUT"
      ? this.previousElementSibling
      : this.previousElementSibling.querySelector("input");
  if (input.type === "password") {
    input.type = "text";
    icon.classList.remove("fa-eye-slash");
    icon.classList.add("fa-eye");
  } else {
    input.type = "password";
    icon.classList.remove("fa-eye");
    icon.classList.add("fa-eye-slash");
  }
}
