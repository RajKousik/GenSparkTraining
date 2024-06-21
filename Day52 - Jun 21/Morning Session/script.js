// function employeeDetails(employeeName, employeeSalary) {
//   this.employeeName = employeeName;
//   this.employeeSalary = employeeSalary;
//   this.displayDetails = () => {
//     return (
//       "Employee name is " +
//       this.employeeName +
//       " and salary is " +
//       this.employeeSalary
//     );
//   };
// }
// const firstEmployee = new employeeDetails("John", 20000);
// const secondEmployee = new employeeDetails("Mary", 30000);

// // console.log(firstEmployee.displayDetails())
// // console.log(secondEmployee.displayDetails())
// console.log(firstEmployee.displayDetails());
// console.log(secondEmployee.employeeName);

// class Vehicle {
//   constructor(vehicleType, modelName) {
//     this.vehicleType = vehicleType;
//     this.modelName = modelName;
//   }
//   displayDetails() {
//     console.log(
//       `The type of Vehicle is ${this.vehicleType} and the model is ${this.modelName}`
//     );
//   }
// }
// let vehicleOne = new Vehicle("Car", "Audi");
// vehicleOne.displayDetails();

// class Employee {
//   constructor(name, designation, salary) {
//     this.name = name;
//     this.designation = designation;
//     this.salary = salary;
//   }
//   greet() {
//     console.log("Hello welcome i am communicating through parent class");
//   }
// }

// class Manager extends Employee {
//   constructor(name, salary, designation, empId) {
//     super(name, designation, salary);
//     this.empId = empId;
//   }
//   displayInformation() {
//     console.log(`The name of employee is ${this.name} salary is ${this.salary}
//               Designation is ${this.designation} and Employee ID is ${this.empId}`);
//   }
// }

// let manager = new Manager("John", "HR Manager", 50000, 100);
// manager.greet();
// manager.displayInformation();

// class Animal {
//   walking() {
//     console.log("Animal is walking");
//   }
// }

// class Tiger extends Animal {
//   walking() {
//     console.log("Tiger is walking with four legs");
//   }
// }

// class Bear extends Animal {
//   walking() {
//     console.log("Bear is walking with two legs");
//   }
// }

// const animals = [new Tiger(), new Bear()];
// animals.forEach((animal) => animal.walking());

// class Shape {
//   area(valueOne, valueTwo) {
//     console.log(valueOne * valueTwo);
//   }
// }

// class Rectangle extends Shape {
//   area(valueOne, valueTwo) {
//     super.area(valueOne, valueTwo);
//   }
// }

// let rectangle = new Rectangle();
// rectangle.area(6, 8);

class Person {
  startWalking() {
    console.log("Person starts walking from his home");
  }
  reachedGroceryShop() {
    this.startWalking();
    console.log("Reached the grocery shop");
  }
}

let ram = new Person();
ram.reachedGroceryShop();
