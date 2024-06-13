// let currentId = 1;

// const form = document.getElementById("productForm");

// form.addEventListener("submit", function (event) {
//   event.preventDefault();

//   const name = document.getElementById("name").value.trim();
//   const category = document.getElementById("category").value.trim();
//   const price = parseFloat(document.getElementById("price").value);
//   const quantity = parseInt(document.getElementById("quantity").value, 10);
//   const messageDiv = document.getElementById("message");

//   messageDiv.textContent = "";
//   messageDiv.classList.remove("success", "error");

//   if (!name) {
//     messageDiv.textContent = "Product name is required.";
//     messageDiv.classList.add("error");
//     return;
//   }

//   if (!category) {
//     messageDiv.textContent = "Category is required.";
//     messageDiv.classList.add("error");
//     return;
//   }

//   if (isNaN(price) || price <= 0) {
//     messageDiv.textContent = "Price must be a number greater than 0.";
//     messageDiv.classList.add("error");
//     return;
//   }

//   if (isNaN(quantity) || quantity <= 0) {
//     messageDiv.textContent = "Quantity must be a number greater than 0.";
//     messageDiv.classList.add("error");
//     return;
//   }

//   const table = document
//     .getElementById("productTable")
//     .getElementsByTagName("tbody")[0];
//   const newRow = table.insertRow();
//   const idCell = newRow.insertCell(0);
//   const nameCell = newRow.insertCell(1);
//   const categoryCell = newRow.insertCell(2);
//   const priceCell = newRow.insertCell(3);
//   const quantityCell = newRow.insertCell(4);

//   idCell.textContent = currentId++;
//   nameCell.textContent = name;
//   categoryCell.textContent = category;
//   priceCell.textContent = price.toFixed(2);
//   quantityCell.textContent = quantity;

//   messageDiv.textContent = "Product added successfully!";
//   messageDiv.classList.add("success");

//   form.reset();
// });

let currentId = 1;

document
  .getElementById("productForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    // Get form values
    const formData = new FormData(event.target);
    const productData = {
      name: formData.get("name").trim(),
      category: formData.get("category").trim(),
      price: parseFloat(formData.get("price")),
      quantity: parseInt(formData.get("quantity"), 10),
      image: formData.get("image"),
    };
    const messageDiv = document.getElementById("message");

    // Clear any previous messages
    messageDiv.innerHTML = "";
    messageDiv.classList.remove("success", "error");

    // Destructure the productData object
    const { name, category, price, quantity, image } = productData;

    // Validate the form using rest operator for conditions
    const validations = [
      {
        valid: !!name,
        message: "Product name is required.",
        element: document.getElementById("name"),
      },
      {
        valid: !!category,
        message: "Category is required.",
        element: document.getElementById("category"),
      },
      {
        valid: !isNaN(price) && price > 0,
        message: "Price must be a number greater than 0.",
        element: document.getElementById("price"),
      },
      {
        valid: !isNaN(quantity) && quantity > 0,
        message: "Quantity must be a number greater than 0.",
        element: document.getElementById("quantity"),
      },
    ];

    // Reset all input borders and collect error messages
    let allValid = true;
    validations.forEach((validation) => {
      validation.element.classList.remove("is-invalid");
      if (!validation.valid) {
        allValid = false;
        validation.element.classList.add("is-invalid");
        const errorMessage = document.createElement("div");
        errorMessage.textContent = validation.message;
        errorMessage.classList.add("error");
        messageDiv.appendChild(errorMessage);
      }
    });

    if (!allValid) {
      messageDiv.classList.add("error");
      return;
    }

    // Create a new row and cells
    const table = document
      .getElementById("productTable")
      .getElementsByTagName("tbody")[0];
    const newRow = table.insertRow();
    const idCell = newRow.insertCell(0);
    const nameCell = newRow.insertCell(1);
    const categoryCell = newRow.insertCell(2);
    const priceCell = newRow.insertCell(3);
    const quantityCell = newRow.insertCell(4);
    const imageCell = newRow.insertCell(5);

    // Set cell values using the spread operator
    let imageContent = "Not Uploaded";
    console.log("image :>> ", image);
    if (image.size != 0) {
      imageContent = `<img src="${URL.createObjectURL(
        formData.get("image")
      )}" alt="${name}" style="max-width: 100px; height: auto;">`;
    }
    const cells = [
      currentId++,
      name,
      category,
      price.toFixed(2),
      quantity,
      imageContent,
    ];
    [
      idCell.textContent,
      nameCell.textContent,
      categoryCell.textContent,
      priceCell.textContent,
      quantityCell.textContent,
      imageCell.innerHTML,
    ] = cells;

    // Display success message
    const successMessage = document.createElement("div");
    successMessage.textContent = "Product added successfully!";
    successMessage.classList.add("success");
    messageDiv.appendChild(successMessage);

    // Clear the form values
    document.getElementById("productForm").reset();
  });
