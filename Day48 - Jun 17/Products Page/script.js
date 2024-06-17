document.addEventListener("DOMContentLoaded", function () {
  const productList = document.getElementById("product-list");

  // Function to create and append product card
  function appendProduct(product) {
    const productCard = document.createElement("div");
    productCard.className = "col-md-4 product-card";
    productCard.setAttribute("data-id", product.id);
    productCard.innerHTML = `
            <div class="card h-100 position-relative">
                <img src="${product.thumbnail}" class="card-img-top" alt="${product.title}">
                <div class="card-body">
                    <h5 class="card-title">${product.title}</h5>
                    <p class="card-text">${product.description}</p>
                    <p class="card-price">$${product.price}</p>
                </div>
                <button class="update-btn" onclick="openUpdateModal(${product.id}, '${product.thumbnail}', '${product.title}', '${product.description}', ${product.price})">Update</button>
                <button class="delete-btn" onclick="deleteProduct(${product.id}, this)">Delete</button>
            </div>
        `;
    productList.appendChild(productCard);
  }

  // Fetch and display products
  fetch("https://dummyjson.com/products")
    .then((response) => response.json())
    .then((data) => {
      const products = data.products;
      products.forEach((product) => {
        appendProduct(product);
      });
    })
    .catch((error) => console.error("Error fetching products:", error));

  // Add new product button click
  document
    .getElementById("add-product-btn")
    .addEventListener("click", function () {
      $("#addProductModal").modal("show");
    });

  // Save new product button click
  document
    .getElementById("save-product-btn")
    .addEventListener("click", function () {
      const productImage = document.getElementById("productImage").value;
      const productTitle = document.getElementById("productTitle").value;
      const productDescription =
        document.getElementById("productDescription").value;
      const productPrice = document.getElementById("productPrice").value;

      if (productImage && productTitle && productDescription && productPrice) {
        fetch("https://dummyjson.com/products/add", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            title: productTitle,
            description: productDescription,
            price: productPrice,
            thumbnail: productImage,
          }),
        })
          .then((res) => res.json())
          .then((newProduct) => {
            console.log("Product added:", newProduct);
            appendProduct(newProduct);
            $("#addProductModal").modal("hide");
            document.getElementById("add-product-form").reset();
          })
          .catch((error) => console.error("Error adding product:", error));
      }
    });
});

// Function to delete product
function deleteProduct(productId, button) {
  fetch(`https://dummyjson.com/products/${productId}`, {
    method: "DELETE",
  })
    .then((res) => res.json())
    .then(() => {
      console.log("Product deleted:", productId);
      const productCard = button.closest(".product-card");
      productCard.remove();
    })
    .catch((error) => console.error("Error deleting product:", error));
}

// Function to open update modal with current product details
function openUpdateModal(id, image, title, description, price) {
  document.getElementById("updateProductId").value = id;
  document.getElementById("updateProductImage").value = image;
  document.getElementById("updateProductTitle").value = title;
  document.getElementById("updateProductDescription").value = description;
  document.getElementById("updateProductPrice").value = price;

  $("#updateProductModal").modal("show");
}

// Update product button click
document
  .getElementById("update-product-btn")
  .addEventListener("click", function () {
    const productId = document.getElementById("updateProductId").value;
    const productImage = document.getElementById("updateProductImage").value;
    const productTitle = document.getElementById("updateProductTitle").value;
    const productDescription = document.getElementById(
      "updateProductDescription"
    ).value;
    const productPrice = document.getElementById("updateProductPrice").value;

    if (productImage && productTitle && productDescription && productPrice) {
      fetch(`https://dummyjson.com/products/${productId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title: productTitle,
          description: productDescription,
          price: productPrice,
          thumbnail: productImage,
        }),
      })
        .then((res) => res.json())
        .then((updatedProduct) => {
          console.log("Product updated:", updatedProduct);
          const productCard = document.querySelector(
            `.product-card[data-id='${productId}']`
          );
          productCard.querySelector(".card-img-top").src =
            updatedProduct.thumbnail;
          productCard.querySelector(".card-title").textContent =
            updatedProduct.title;
          productCard.querySelector(".card-text").textContent =
            updatedProduct.description;
          productCard.querySelector(
            ".card-price"
          ).textContent = `$${updatedProduct.price}`;
          $("#updateProductModal").modal("hide");
        })
        .catch((error) => console.error("Error updating product:", error));
    }
  });
