# ADA502_Fireguard_Group_4

# Project Structure
![image](https://github.com/user-attachments/assets/42c0afe2-7434-40c5-97c0-7dc7eaeeb512)<br>
This project is comprised of 4 primary microservices, 
# An api project
This project runs pythons Fast Api to serve api requests
# An auth project
# A business logic project
# A Database project


# Quick start
run this command in root to create generated code for Api (need the grpc packages for python to run) <br>
python -m grpc_tools.protoc -I Protos --python_out=Api/app/generatedCode --pyi_out=Api/app/generatedCode --grpc_python_out=Api/app/generatedCode Protos/TestService.proto
