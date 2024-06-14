document.addEventListener("DOMContentLoaded", function () {
  const professionList = ["Doctor", "Engineer", "Teacher"];
  const professionInput = document.getElementById("profession");
  const professionDataList = document.getElementById("professionList");

  const personalInfoCollapse = document.getElementById("personalInfo");
  const professionalInfoCollapse = document.getElementById("professionalInfo");

  function showCollapse(collapseElement) {
    $(collapseElement).collapse("show");
  }

  function hideCollapse(collapseElement) {
    $(collapseElement).collapse("hide");
  }

  function updateAge() {
    const dob = document.getElementById("dob").value;
    const ageInput = document.getElementById("age");
    if (dob) {
      const dobDate = new Date(dob);
      const diff = Date.now() - dobDate.getTime();
      const ageDate = new Date(diff);
      const age = ageDate.getUTCFullYear() - 1970;
      //   console.log("diff, ageDate, age :>> ", diff, ageDate, age);
      ageInput.value = age;
    } else {
      ageInput.value = "";
    }
  }

  function validateField(field, validationFn, errorMessage) {
    const value = field.value.trim();
    const errorField = document.getElementById(field.id + "Error");
    if (!validationFn(value)) {
      field.classList.add("error");
      errorField.innerText = errorMessage;
      return false;
    } else {
      field.classList.remove("error");
      errorField.innerText = "";
      return true;
    }
  }

  function validateForm() {
    let isValid = true;
    let errors = "";

    // Name validation
    const nameValid = validateField(
      document.getElementById("name"),
      (value) => value !== "",
      "Name is required"
    );
    isValid = isValid && nameValid;

    // Phone validation
    const phoneValid = validateField(
      document.getElementById("phone"),
      (value) => /^\d{10}$/.test(value),
      "Phone number must be 10 digits"
    );
    isValid = isValid && phoneValid;

    // DOB validation
    const dobValid = validateField(
      document.getElementById("dob"),
      (value) => {
        if (value === "") return false; // Required check
        const today = new Date();
        const dobDate = new Date(value);
        return dobDate <= today; // Check if DOB is not in future
      },
      "Valid Date of Birth is required"
    );
    isValid = isValid && dobValid;

    // Gender validation
    const gender = document.querySelector('input[name="gender"]:checked');
    const genderError = document.getElementById("genderError");
    if (!gender) {
      genderError.innerText = "Gender is required";
      document.querySelector('input[name="gender"]').classList.add("error");
      isValid = false;
    } else {
      genderError.innerText = "";
      document
        .querySelectorAll('input[name="gender"]')
        .forEach((el) => el.classList.remove("error"));
    }

    // Qualification validation
    const qualificationChecked = document.querySelectorAll(
      'input[type="checkbox"]:checked'
    );
    const qualificationError = document.getElementById("qualificationError");
    if (qualificationChecked.length === 0) {
      qualificationError.innerText = "At least one qualification is required";
      document.querySelector('input[type="checkbox"]').classList.add("error");
      isValid = false;
    } else {
      qualificationError.innerText = "";
      document
        .querySelectorAll('input[type="checkbox"]')
        .forEach((el) => el.classList.remove("error"));
    }

    // Profession validation
    const professionValid = validateField(
      document.getElementById("profession"),
      (value) => value !== "",
      "Profession is required"
    );
    if (professionValid) {
      const profession = document.getElementById("profession").value.trim();
      if (!professionList.includes(profession)) {
        professionList.push(profession);
        const option = document.createElement("option");
        option.value = profession;
        professionDataList.appendChild(option);
      }
    }
    isValid = isValid && professionValid;

    if (!isValid) {
      //   console.log("isValid :>> ", isValid);
      if (
        (!nameValid ||
          !phoneValid ||
          !dobValid ||
          !gender ||
          qualificationChecked.length === 0) &&
        !professionValid
      ) {
        showCollapse(personalInfoCollapse);
        showCollapse(professionalInfoCollapse);
      } else if (!professionValid) {
        // console.log("professionValid :>> ", professionValid);
        showCollapse(professionalInfoCollapse);
      } else {
        showCollapse(personalInfoCollapse);
      }
      errors = "Please correct the highlighted errors";
    }

    document.getElementById("formErrors").innerText = errors;
    return isValid;
  }

  document.getElementById("dob").addEventListener("blur", updateAge);
  document
    .querySelectorAll(".form-control, .form-check-input")
    .forEach((input) => {
      input.addEventListener("blur", validateForm);
      input.addEventListener("mouseleave", validateForm);
    });

  const registrationForm = document.getElementById("registrationForm");
  registrationForm.addEventListener("submit", function (e) {
    e.preventDefault();
    if (validateForm()) {
      alert("Form submitted successfully!");
      registrationForm.reset();
    }
  });
});
