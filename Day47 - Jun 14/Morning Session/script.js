function buttonClick() {
  var myDiv = document.getElementById("div1");
  myDiv.innerHTML = "You clicked the button";
  //myDiv.style.backgroundColor = "blue";
  //myDiv.style.color = "white";
  myDiv.classList.add("alert");
  myDiv.classList.add("alert-primary");
}
var validateName = () => {
  var txtName = document.getElementById("txtName");
  if (!txtName.value) {
    txtName.classList.add("error");
    txtName.classList.remove("correct");
  } else {
    txtName.classList.remove("error");
    txtName.classList.add("correct");
  }
};
var btnMouseHover = () => {
  document.getElementsByTagName("button")[0].style.fontSize = "2em";
};
var btnMouseHoverClear = () => {
  document.getElementsByTagName("button")[0].style.fontSize = "1em";
};
