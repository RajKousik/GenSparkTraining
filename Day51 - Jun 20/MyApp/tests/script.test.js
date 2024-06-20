const { JSDOM } = require("jsdom");
const fs = require("fs");
const path = require("path");
//import {JSDOM} from 'jsdom';

test("simple button click", () => {
  const html = fs.readFileSync(
    path.resolve(__dirname, "../index.html"),
    "utf8"
  );
  const script = fs.readFileSync(
    path.resolve(__dirname, "../script.js"),
    "utf8"
  );

  const dom = new JSDOM(html, {
    runScripts: "dangerously",
    resources: "usable",
  });
  const scriptElement = dom.window.document.createElement("script");
  scriptElement.textContent = script;
  dom.window.document.body.appendChild(scriptElement);

  //Raising the event
  dom.window.document.getElementById("btn").click();
  const actual = dom.window.document.getElementById("demo").innerHTML;
  expect(actual).toBe("Hello World");
});
