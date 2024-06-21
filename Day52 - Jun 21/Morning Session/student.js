// Base class (Superclass)
class Person {
  constructor(name, age) {
    this._name = name; // Encapsulation using private properties
    this._age = age;
  }

  // Getter and Setter for name
  get name() {
    return this._name;
  }

  set name(value) {
    this._name = value;
  }

  // Getter and Setter for age
  get age() {
    return this._age;
  }

  set age(value) {
    if (value >= 0) {
      this._age = value;
    } else {
      console.log("Age cannot be negative");
    }
  }

  describe() {
    console.log(`I am ${this._name}, ${this._age} years old.`);
  }

  celebrateBirthday() {
    this._age += 1;
    console.log(`Happy birthday! I am now ${this._age} years old.`);
  }
}

// Derived class (Subclass)
class Student extends Person {
  constructor(name, age, studentId) {
    super(name, age); // Inheritance
    this._studentId = studentId;
  }

  // Getter and Setter for studentId
  get studentId() {
    return this._studentId;
  }

  set studentId(value) {
    this._studentId = value;
  }

  // Overriding the describe method (Polymorphism)
  describe() {
    console.log(
      `I am a student and my name is ${this._name}, ${this._age} years old, and my student ID is ${this._studentId}.`
    );
  }

  study() {
    console.log(`${this._name} is studying.`);
  }

  takeExam() {
    console.log(`${this._name} is taking an exam.`);
  }
}

//In Prototype Inheritance, an object uses the properties or methods of another object via the prototype linkage.

const teacherPrototype = {
  name: "Ram",
  teach() {
    console.log(`${this.name} is teaching.`);
  },
  gradePapers() {
    console.log(`${this.name} is grading papers.`);
  },
};

const teacher1 = Object.create(teacherPrototype);
teacher1.teach();
teacher1.gradePapers();

const teacher2 = Object.create(teacherPrototype);
teacher2.name = "Ms. Johnson";
teacher2.teach();
teacher2.gradePapers();

// Example usage
const person = new Person("Raj", 20);
person.describe();
person.celebrateBirthday();

const student1 = new Student("John Doe", 20, "S12345");
student1.describe();
student1.study();
student1.takeExam();
student1.celebrateBirthday();

const student2 = new Student("Jane Doe", 22, "S67890");
student2.describe();
student2.study();
student2.takeExam();
student2.celebrateBirthday();
