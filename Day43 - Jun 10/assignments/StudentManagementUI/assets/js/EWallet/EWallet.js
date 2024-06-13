AOS.init({ duration: 1000 });

document
  .getElementById("rechargeForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();
    recharge();
  });

function recharge() {
  const amount = document.getElementById("amount").value;
  const messageDiv = document.getElementById("message");

  // Mock API call
  fetch("https://dummyjson.com/products", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    // body: JSON.stringify({ amount: amount }),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("data :>> ", data);
      if (data.products) {
        showMessage("Recharge successful!", "alert-success");
      } else {
        showMessage("Recharge failed!", "alert-danger");
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      showMessage("Recharge failed!", "alert-danger");
    });
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
