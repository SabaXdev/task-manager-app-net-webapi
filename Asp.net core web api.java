
Controller → Receives HTTP requests.
Service Interface → Provides a contract for business logic.
Service → Implements the business logic.
DbContext → Interacts with the database.
Database → Stores the data.


-------------> ASP.NET Structure >---------------
*Controller (Entry Point) -> Receives HTTP requests.
Calls methods in the service layer to process the request.


*Service Interface (Contract) -> Defines what the service will do without specifying how.
Used to decouple the implementation from the controller, allowing easier testing and maintenance.


*Service (Business Logic)-> Implements the actual logic of interacting with the database.
Calls methods in the DbContext to query or manipulate data.


*DbContext Class -> The heart of Entity Framework Core. Manages database operations.
Defines DbSet<T> properties that represent database tables.
Configured in Startup.cs to connect to the actual database.


*Database (Storage) -> The physical database (SQL Server, SQLite, etc.) stores your data.



Controller(use functions from service) -> 
					Service(Interface) ->
					Service(Main code) -> 
							   Context -> 
							   DbContext(base class for quering and save instances)



+-------------+    +----------------------+    +-------------------------+    +--------------------+
|             |    |                      |    |                         |    |                    |
| Controller  +--->|  Service Interface    +--->|  Service (Implementation)|--->|   DbContext         |
|             |    |  (IProductService)    |    |  (ProductService)        |    |  (AppDbContext)     |
+-------------+    +----------------------+    +-------------------------+    +--------------------+
                        ^                                                           |
                        |                                                           |
                  Dependency Injection                                        +---------------------+
                        |                                                    |                     |
                        +--------------------------------------------------->|     Database         |
                                                                             | (SQL Server/SQLite)  |
                                                                             +---------------------+






If u send request to backend using html form, be sure to enable 

dotnet add package Microsoft.AspNetCore.Cors


How Components and Services Work Together
Components handle the UI and user interactions.
Services handle the business logic, data management, and external API calls.
For example:

A TodoComponent displays a list of todos.
The TodoService fetches the todos from an API.
The TodoComponent uses the TodoService to get the todos and display them.




What is subscribe()?
subscribe() is a method provided by Observables in Angular, which are part of RxJS (Reactive Extensions for JavaScript). 
When you make an HTTP request in Angular (such as via HttpClient), the result of that request is an Observable. 
An Observable represents a stream of asynchronous data or events (like the result of an HTTP request).

The subscribe() method is used to "listen" to this stream of data and act on it when the data arrives 
(or when the HTTP request completes).

In this case:

The todoService.deleteTodo(id) method returns an Observable. This Observable doesn't return any data 
(it just confirms that the todo was successfully deleted).

You subscribe to the Observable so that you can react when the deletion operation is completed 
(successful or failed).


------------------> Layout <-------------------

Content
Padding -> space from the sides of the box  -> 10px (from all sides), 10px 15px, 10px,20px 10px, 20px
Border -> Border between padding and margin -> size, type, color -> 10px, solid, black
Margin -> Space toward other boxes













-------------> Building Asp.Net Project <-----------------
dotnet new webapi --use-controllers -o TaskManagerBackend
dotnet dev-certs https --trust

-> Create Models(make dir Models -> TaskManager.cs)

-> Install Packages
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0 
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package AutoMapper --version 13.0.1

-> Create child DbContext class and Register it in main cs file

-> Contracts for create and update TaskManager item(DTO)

-> Mapping(from DTO to TaskManager model) -> Register to main cs file (add automapper)

-> Service Interface
-> Service

Controller(Http method: POST, GET, PUT, DELETE)

Apply Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

Build
Run
-----------------------------------------------------------
-----------------> Angular <------------------------------- 



npm install angular cli

ng new my-app --no-standalone(Including module.ts)

src/
└── app/
    ├── core/                  # Core services (e.g., API service for HTTP requests)
    │   └── services/
    │       └── task.service.ts # TaskService for handling CRUD operations
    ├── components/             # All reusable components
    │   ├── task-list/
    │   │   ├── task-list.component.ts   # Lists all tasks
    │   │   └── task-list.component.html # Template for task list
    │   │   └── task-list.component.css  # Style for task list
    ├── models/
    │   ├── task-model.ts
    ├── app.component.ts           # Root component for the app
    └── app-routing.module.ts      # Routing configuration


-> Add Services and Component directories and Models(Manually)
ng generate service core/services/task
ng generate component components/task-list

-> Edit task-service file to handle http requests

-> Connect to backend
(// Connect to Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Your Angular app's URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});)


-> Import in componenet in order server to run correctly
import { FormsModule } from '@angular/forms';          (For ngModule)
import { CommonModule } from '@angular/common';


-> add provideHttpClient() in app.config.ts------------------------------------

export const appConfig: ApplicationConfig = {
  providers: 
  [
  	provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), 
    provideHttpClient()
]
};`
-> GO AHAED AND ADD BUSINESS LOGIC
--------------------------------------------------------------------------------