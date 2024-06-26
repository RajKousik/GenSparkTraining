// Fetch all the forms we want to apply custom Bootstrap validation styles to
var forms = document.querySelectorAll(".needs-validation");

// Loop over them and prevent submission
Array.prototype.slice.call(forms).forEach(function (form) {
  form.addEventListener(
    "submit",
    function (event) {
      if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
      }
      form.classList.add("was-validated");
    },
    false
  );
});

function removeValidations(form) {
  form.querySelectorAll(".form-control").forEach(function (input) {
    input.classList.remove("is-invalid", "is-valid");
  });
}

// Real-time validation
document
  .querySelectorAll(".needs-validation .form-control")
  .forEach(function (input) {
    input.addEventListener("input", function () {
      if (input.type === "password") {
        return; // Skip validation for password fields
      }
      if (input.checkValidity()) {
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
      } else {
        input.classList.remove("is-valid");
        input.classList.add("is-invalid");
      }
    });
  });

// window.addEventListener("pagehide", function () {
//   document
//     .querySelectorAll(".needs-validation .form-control")
//     .forEach(function (input) {
//       input.classList.remove("is-valid", "is-invalid");
//     });
// });
