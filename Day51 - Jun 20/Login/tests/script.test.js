const { JSDOM } = require("jsdom");
const fs = require("fs");
const path = require("path");

test("successful login", async () => {
  const html = fs.readFileSync(
    path.resolve(__dirname, "../src/index.html"),
    "utf8"
  );
  const script = fs.readFileSync(
    path.resolve(__dirname, "../src/script.js"),
    "utf8"
  );

  const dom = new JSDOM(html, {
    runScripts: "dangerously",
    resources: "usable",
  });

  const scriptElement = dom.window.document.createElement("script");
  scriptElement.textContent = script;
  dom.window.document.body.appendChild(scriptElement);

  await new Promise((resolve) => {
    dom.window.addEventListener("load", resolve);
  });

  dom.window.document.getElementById("username").value = "RajKousik";
  dom.window.document.getElementById("password").value = "Dragons";
  dom.window.document
    .getElementById("loginForm")
    .dispatchEvent(new dom.window.Event("submit"));

  const messageElement = dom.window.document.getElementById("message");
  expect(messageElement.textContent).toBe("Login successful!");
});

test("failed login", async () => {
  const html = fs.readFileSync(
    path.resolve(__dirname, "../src/index.html"),
    "utf8"
  );
  const script = fs.readFileSync(
    path.resolve(__dirname, "../src/script.js"),
    "utf8"
  );

  const dom = new JSDOM(html, {
    runScripts: "dangerously",
    resources: "usable",
  });

  const scriptElement = dom.window.document.createElement("script");
  scriptElement.textContent = script;
  dom.window.document.body.appendChild(scriptElement);

  await new Promise((resolve) => {
    dom.window.addEventListener("load", resolve);
  });

  dom.window.document.getElementById("username").value = "Emilia";
  dom.window.document.getElementById("password").value = "wrongPassword";
  dom.window.document
    .getElementById("loginForm")
    .dispatchEvent(new dom.window.Event("submit"));

  const messageElement = dom.window.document.getElementById("message");
  expect(messageElement.textContent).toBe("Invalid username or password.");
});
