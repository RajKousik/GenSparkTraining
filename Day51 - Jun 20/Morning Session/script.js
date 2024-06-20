function testMethod() {
  // Get the content of the h1 element
  const h1Content = document.querySelector("h1").innerHTML;

  // Check if the content is "Hello World"
  if (h1Content === "Hello, World!") {
    console.log(`The content of the h1 element is ${h1Content}`);
  } else {
    console.log(`The content of the h1 element is not ${h1Content}`);
  }
}
