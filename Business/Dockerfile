FROM python:3.12

# Set the working directory
WORKDIR /app

# Copy the requirements file into the container
COPY Business/requirements.txt .

# Install the dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the application code into the container
COPY Business/app/ ./app

# Start the application using Uvicorn
CMD ["python", "app/main.py"]