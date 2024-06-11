function toggleSideBar() {
  var sidebar = document.getElementById("sidebar");
  var togglerIcon = document.getElementById("navbar-toggler-icon");

  if (
    sidebar.classList.contains("collapsing") &&
    togglerIcon.classList.contains("navbar-toggler-icon")
  ) {
    togglerIcon.classList.remove("navbar-toggler-icon");
    togglerIcon.classList.add("btn-close");
  } else {
    togglerIcon.classList.remove("btn-close");
    togglerIcon.classList.add("navbar-toggler-icon");
  }
}
function checkWindowSize() {
  var sidebar = document.getElementById("sidebar");
  var togglerIcon = document.getElementById("navbar-toggler-icon");

  if (window.innerWidth > 576) {
    sidebar.classList.remove("collapse");
    sidebar.classList.remove("show");
    togglerIcon.classList.remove("btn-close");
    togglerIcon.classList.add("navbar-toggler-icon");
  } else {
    sidebar.classList.add("collapse");
  }
}

window.addEventListener("resize", checkWindowSize);
window.addEventListener("load", checkWindowSize);
