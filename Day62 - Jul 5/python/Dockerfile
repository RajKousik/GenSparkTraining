# Use an official Python runtime as a parent image
FROM python:3.9-slim

# Copy the current directory contents into the container at /app
COPY . /app

# Set the working directory in the container
WORKDIR /app

# Run hello.py when the container launches
CMD ["python", "index.py"]
