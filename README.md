# ADA502_Fireguard_Group_4

# Project Structure
![image](https://github.com/user-attachments/assets/42c0afe2-7434-40c5-97c0-7dc7eaeeb512)<br>
This project is comprised of 4 primary microservices, 
# An api project
This project is an Asp Dot Net Core project running on Dotnet 9. <br>
The primary consern of this project is to receive api calls and route them to where they will get processed <br> 
The routing of data is through GRPC and this service will primarily send data and receive answers and not receive requests
# An auth project
TODO
# A business logic project
This is a python project running with fastApi and is responsible for the business logic of the project
# A Database project
This project is an Asp Dot Net Core project running oon Dotnet 9. <br>
The project is primarily to manage the postgres database and receive calls and return data.
The project also contains some logic to setup local testing with seeding of the database

The basic structure for the DB, will probably be extended in the future <br>
<img width="772" alt="image" src="https://github.com/user-attachments/assets/9e81b259-ca86-4a1d-853f-7f85bde95520" /><br>

Flow for creating a userevent
![image](https://github.com/user-attachments/assets/b508cd73-a565-4a31-b54d-7b6d8ba8005e) <br>

Flow for sending events on datapoll
![image](https://github.com/user-attachments/assets/bd051f39-c079-48da-a6de-ddca196d8f7a) <br>


# In the pipeline
Rabbit MQ will be used as a message broker to send messages from the api project that will be picked up directly from the database service without them <br>
needing to be coupled or know of eachother, just have a contract that they use to parse the data from the queue





# Quick start

To get started <br>

Generate the devcert using the devcert script, this is to utilize https locally (if you dont have any other localhost devcerts on your machine it needs to run a couple of times or you can change the array pointing in the script) <br>
Move the Cert into the folder specified in the docker compose <br>
Run the docker compose, this will spin up all services and a full end to end test can be done <br>

The database project seeds the database with some mockdata <br>

To test go to https://localhost:5272/api/firerisk/{id} (the seeder seeds 4 values so 1-4 should give data back) <br>
