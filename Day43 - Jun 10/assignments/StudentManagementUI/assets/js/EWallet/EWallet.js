AOS.init({ duration: 1000 });

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

document
  .getElementById("rechargeForm")
  .addEventListener("submit", async function (event) {
    event.preventDefault();
    await recharge();
  });

async function recharge() {
  const amount = document.getElementById("amount").value;

  var api_url = `${config.API_URL}/students/recharge`;
  const requestBody = {
    studentId: getUserId(),
    RechargeAmount: amount,
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
    getWalletAmount();
    showMessage("Recharge successful!", "alert-success");
  } else {
    const error = await response.json();
    console.error("Error while fetching the data", error.message);
    showMessage(`Recharge failed! ${error.message}`, "alert-danger");
    return 0;
  }
}

async function updateWallet(amount) {}

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
