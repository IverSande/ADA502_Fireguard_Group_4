FROM python:3.12

# Set the working directory
WORKDIR /app

# Copy the requirements file into the container
COPY Api/requirements.txt .

# Install the dependencies
RUN pip install --no-cache-dir -r requirements.txt

RUN mkdir -p Api/app/generatedCode

RUN python -m grpc_tools.protoc -I Protos --python_out=Api/app/generatedCode --pyi_out=Api/app/generatedCode --grpc_python_out=Api/app/generatedCode Protos/TestService.proto

# Copy the application code into the container
COPY Api/app/ ./app


# Expose the port the application will run on
EXPOSE 8000

# Start the application using Uvicorn
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]