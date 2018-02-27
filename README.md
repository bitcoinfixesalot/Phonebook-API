# Phonebook-API

#Requirements

.NET Core 2.0 – download from http://dot.net/

# Installation

Change the connectionstring in config.json file to use your sql server. 
"PhonebookConnectionString": "server=(localdb)\\ProjectsV13;Database=PhonebookDb;Integrated Security=true;MultipleActiveResultSets=true;

>dotnet run

#Accessing the API

You can access the API using postman - https://www.getpostman.com/

POST - Login
Necessary to authenticate user and return jwt token:

"http://localhost:49893/api/Login"
curl -X POST -H "Content-Type: application/json" -d '{ "username": "admin", "password": "P@ssw0rd!" }' 

POST - Create Entry
Given the JWT token returned at login.


curl -X POST -H "Authorization: jwt token goes here" -H "Content-Type: application/json" -d 
'{
    "name": "Colin Farrell",
    "phoneNumber": "12345153125",
    "address": "20 Ellerbeck Rd, Darwen BB3 3EX, UK"
}' "http://localhost:49893/api/phonebook/"

GET - Get Entry
curl -X GET -H "Authorization: jwt token goes here" "http://localhost:49893/api/phonebook/{id}"

PUT - Update Entry
To update an entry, the id is needed:
curl -X PUT -H "Authorization: jwt goes here" -H "Content-Type: application/json" -d '
{
    "id": "1",
    "name": "Colin Farrell",
    "phoneNumber": "+44 12345153125",
}' "http://localhost:49893/api/phonebook/"

DELETE - Delete Entry
curl -X DELETE -H "Authorization: jwt token " -H "Content-Type: application/json" -d '' "http://localhost:49893/api/phonebook/{id}"


