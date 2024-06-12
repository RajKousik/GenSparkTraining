// function checkingEvenNumbers(num) {
//   return num % 2 == 0; //boolean
// }

// function filteringNumbers(numbers, callbackFunc) {
//   let numberArray = [];
//   for (let value of numbers) {
//     if (callbackFunc(value)) numberArray.push(value);
//   }
//   return numberArray;
// }

// let arrayOfNumbers = [22, 45, 99, 3, 8, 44];
// console.log(filteringNumbers(arrayOfNumbers, checkingEvenNumbers));
// console.log(filteringNumbers(arrayOfNumbers, (num) => num % 2 == 1));
// console.log(filteringNumbers(arrayOfNumbers, (num) => num < 10));

// const area = function (radius) {
//   return Math.PI * radius * radius;
// };
// const diameter = function (radius) {
//   return 2 * radius;
// };
// const calculate = function (radius, logic) {
//   const output = [];
//   for (let i = 0; i < radius.length; i++) {
//     output.push(logic(radius[i]));
//   }
//   return output;
// };
// radius = [7, 8, 9];
// console.log(calculate(radius, area));
// console.log(calculate(radius, diameter));

// function checkingEvenNumbers(num) {
//   return num % 2 == 0; //boolean
// }

// function filteringEvenNumbers(numbers, callbackFunc) {
//   let numberArray = [];
//   for (let value of numbers) {
//     if (callbackFunc(value)) numberArray.push(value);
//   }
//   return () => console.log(numberArray);
// }

// let arrayOfNumbers = [22, 45, 99, 3, 8, 44];
// console.log(filteringEvenNumbers(arrayOfNumbers,checkingEvenNumbers))
// let result = filteringEvenNumbers(arrayOfNumbers, checkingEvenNumbers);
// result();

//reduce
// let arrayOfNumbers=[1,2,3,4,5]
// let sumOfArrayElements=arrayOfNumbers.reduce((sum,value)=>{
// return sum+value
// })
// console.log(sumOfArrayElements)

//foreach
// let arrayOfNumbers=[22,45,99,3,8,44]
// arrayOfNumbers.forEach(num=>{console.log(num)})

//sort
// let arrayOfNumbers = [22, 45, 99, 3, 8, 44];
// arrayOfNumbers.sort((numOne, numTwo) => numOne - numTwo);
// console.log(arrayOfNumbers);

// arrayOfNumbers.sort();
// console.log("arrayOfNumbers :>> ", arrayOfNumbers);

// let sumOfArrayElements = arrayOfNumbers.reduce((sum, value) => {
//   return sum + value;
// }, 50);
// console.log(sumOfArrayElements);

const dateDisplay = () => {
  setInterval(() => {
    document.getElementById("demo").innerHTML = Date();
  }, 500);
};

function accessingParaElement() {
  dateDisplay();
}
