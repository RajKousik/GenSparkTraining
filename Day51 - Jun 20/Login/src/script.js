const users = [
  { username: "Emilia", password: "Dracarys" },
  { username: "Aegon", password: "BurnThemAll" },
  { username: "RajKousik", password: "Dragons" },
];

document
  .getElementById("loginForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const messageElement = document.getElementById("message");

    const user = users.find(
      (user) => user.username === username && user.password === password
    );

    if (user) {
      messageElement.style.color = "green";
      messageElement.textContent = "Login successful!";
    } else {
      messageElement.style.color = "red";
      messageElement.textContent = "Invalid username or password.";
    }
  });
