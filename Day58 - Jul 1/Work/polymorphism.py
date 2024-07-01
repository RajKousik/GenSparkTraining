from math import pi

class Shape:
    """
    A base class to represent a generic shape.
    """

    def area(self):
        """
        Calculate the area of the shape.
        Should be overridden by derived classes.
        """
        raise NotImplementedError("This method should be overridden by subclasses.")

class Circle(Shape):
    """
    A class to represent a circle, inheriting from Shape.
    """

    def __init__(self, radius):
        """
        Initialize the circle with a radius.

        :param radius: Radius of the circle
        """
        self.radius = radius

    def area(self):
        """
        Calculate the area of the circle.
        """
        return pi * self.radius ** 2

class Square(Shape):
    """
    A class to represent a square, inheriting from Shape.
    """

    def __init__(self, side_length):
        """
        Initialize the square with a side length.

        :param side_length: Side length of the square
        """
        self.side_length = side_length

    def area(self):
        """
        Calculate the area of the square.
        """
        return self.side_length ** 2

class Rectangle(Shape):
    """
    A class to represent a rectangle, inheriting from Shape.
    """

    def __init__(self, width, height):
        """
        Initialize the rectangle with a width and height.

        :param width: Width of the rectangle
        :param height: Height of the rectangle
        """
        self.width = width
        self.height = height

    def area(self):
        """
        Calculate the area of the rectangle.
        """
        return self.width * self.height

def print_area(shape):
    """
    Print the area of a given shape.

    :param shape: An instance of a shape class
    """
    print(f"The area of the {shape.__class__.__name__.lower()} is {shape.area()}")


shapes = [
    Circle(5),
    Square(4),
    Rectangle(3, 6)
]

for shape in shapes:
    print_area(shape)
