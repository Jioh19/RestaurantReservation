# Exercise : Restaurant Reservation API

![[Pasted image 20250321115316.png]]
Restaurant Reservation Entity-Relationship diagram.
### 1. Create Web API Project

- Create a new [ASP.NET](http://asp.net/) Core Web API project named `RestaurantReservation.Api`, `RestaurantReservation.Api.Contracts`, `RestaurantReservation.Domain` and `RestaurantReservation.Infrastructure`.

### 2. Setup Dependency

- Add a reference to the `RestaurantReservation.Infrastructure` project from the `RestaurantReservation.Api` project where the `Program.cs` file should be.

### 3. Implement CRUD Operations

- Create API controllers for each entity and implement CRUD operations.

### 4. Implement Reservation Endpoints

- Include CRUD and other specific endpoints for reservations.

### 5. Implement Additional Methods as API Endpoints

- Create endpoints for additional methods, including those related to reservations.
    - **GET** `/api/employees/managers` - List all managers.
    - **GET** `/api/reservations/customer/{customerId}` - Retrieve reservations by customer ID.
    - **GET** `/api/reservations/{reservationId}/orders` - List orders and menu items for a reservation.
    - **GET** `/api/reservations/{reservationId}/menu-items` - List ordered menu items for a reservation.
    - **GET** `/api/employees/{employeeId}/average-order-amount` - Calculate average order amount for an employee.

### 7. Implement Authorization

- Secure the APIs using JWT or another authorization mechanism.

### 8. Validation and Error Handling

- Implement input validation with Fluent Validation and provide user-friendly error messages creating your own middleware to catch the proper exceptions.

### 9. API Documentation with Swagger

- Integrate Swagger to auto-generate API documentation. Ensure that the documentation is comprehensive, including parameters, expected responses, and possible error codes.

### 10. Testing

- Manually test all endpoints using tools like Postman or the Swagger UI to ensure they're working as expected.
- Create unit tests for the Controllers and the Domain services
- Create integration tests for the Infrastructure repositories.

### 11. Bonus: 

#### 11.1 Postman Test Suite

- Write a comprehensive Postman test suite to automatically test all the API endpoints.
- Include tests for successful operations and error handling.
- Share the Postman collection file.

#### 11.2 GitHub actions CI / CD pipeline
* Write a build, test and deploy pipeline with GitHub actions that automatically build, runs unit tests and merges the code or rollbacks if there is an issue.
