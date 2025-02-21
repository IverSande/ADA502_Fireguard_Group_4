# ADA502_Fireguard_Group_4

# Project Structure
![image](https://github.com/user-attachments/assets/42c0afe2-7434-40c5-97c0-7dc7eaeeb512)<br>
This project is comprised of 4 primary microservices, 
# An api project
This project runs pythons Fast Api to serve api requests
# An auth project
# A business logic project
# A Database project

The basic structure for the DB, will probably be extended in the future <br>
<img width="772" alt="image" src="https://github.com/user-attachments/assets/9e81b259-ca86-4a1d-853f-7f85bde95520" /><br>

Flow for creating a userevent
![image](https://github.com/user-attachments/assets/b508cd73-a565-4a31-b54d-7b6d8ba8005e)




# Quick start
run this command in root to create generated code for Api (need the grpc packages for python to run) <br>
python -m grpc_tools.protoc -I Protos --python_out=Api/app/generatedCode --pyi_out=Api/app/generatedCode --grpc_python_out=Api/app/generatedCode Protos/TestService.proto
