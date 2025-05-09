# ADA502_Fireguard_Group_4

# Project Structure
![image](https://github.com/user-attachments/assets/42c0afe2-7434-40c5-97c0-7dc7eaeeb512)<br>
This project is comprised of 3 primary microservices, 
# An api project
This project is an Asp Dot Net Core project running on Dotnet 9. <br>
The primary consern of this project is to receive api calls and route them to where they will get processed <br> 
The routing of data is through GRPC and this service will primarily send data and receive answers and not receive requests
# A business logic project
This is a python project running with fastApi and is responsible for most of the business logic of the project.
In this project all the calls to outside api's are done and weatherdata from frost for instance is fetched and
used. <br>
TODO: The Grpc part of this project is not yet fully online and so at this time the api's can be tested
through a regular http fastapi directly to the python project, and will therefore not contain the implemented 
security or other features seen in the api project
# A Database project
This project is an Asp Dot Net Core project running on Dotnet 9. <br>
The project is primarily to manage the postgres database and receive calls and return data.
The project also contains some logic to setup local testing with seeding of the database

The structure of the DB<br>
![image](https://github.com/user-attachments/assets/147bc658-ea9e-4b76-84d8-c81ea098164e)<br>

Flow for creating a userevent
![image](https://github.com/user-attachments/assets/b508cd73-a565-4a31-b54d-7b6d8ba8005e) <br>




# Quick start

To get started <br>

Convert the env.txt file into a .env file <br>
Run the docker compose with "docker compose up", this will spin up all services and a full end to end test can be done <br>
Import the postman collection into your preferred api testing software for instance Postman. <br>
Create user creates a user with username and email and will return an ID, this is needed for further use <br>
Create Authentication token is used to generate a token that will last 30 minutes and allows for api calls <br>
Subscribe to event location takes a userID and a location and creates an instance in the database that will be used to notify when a certain firerisk threshold is reached at that location <br>

