# ToDoApp
Simple To Do App.


<p float="left">
<img src="register.PNG" width="300" >
<img src="login.PNG" width="300" >
<img src="home.PNG" width="300" >
</p>


Technology Stack:
1. .NET Core
2. Dependency Injection
3. Couchbase
4. Swagger
5. Docker
6. NUnit and Mockito
7. Angular frontend


To start this project follow these steps:


// using docker
1. clone this project to any folder in youre PC.
2. open the project file and start ToDo.sln file in visual studio 2019
3. set docker-compose as startup project
4. press ctrl+F5 to start the project api
5. check if the Api is working by going to https://localhost:44363/swagger/index.html
6. to start ToDo.UI project simply open project file open CMD and write ng serve
7. go to http://localhost:8081/ and add new user=> username: Administrator password: password
8. open new bucket with name: ToDoBucket
9. go to query tab and execute : CREATE PRIMARY INDEX ON ToDoBucket
10. go to http://localhost:4200/mauth for UI
11. enjoy :)

<br>

// using SSL
1. clone this project to any folder in youre PC.
2. open the project file and start ToDo.sln file in visual studio 2019
3. set ToDo.Api as startup project
4. press ctrl+F5 to start the project api
5. go to ToDo.UI project  -> src -> environments -> environment.ts and change apiBaseURl to youre api port 
6. to start ToDo.UI project simply open project file open CMD and write ng serve
7. go to http://localhost:8081/ and add new user=> username: Administrator password: password
8. open new bucket with name: ToDoBucket
9. go to query tab and execute : CREATE PRIMARY INDEX ON ToDoBucket
10. go to http://localhost:4200/mauth for UI
11. enjoy :)

 

