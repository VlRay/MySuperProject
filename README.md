# MySuperProject

A simple microservice-based Web API application built with **.NET 8**, **Docker**, **PostgreSQL**, **MongoDB**, and **Flurl (FluentRest)**.  
This project was created as a test task to demonstrate knowledge of clean architecture, microservice communication, integration testing, and containerization.

---

## Architecture Overview

- **UserService** – handles user and subscription data (PostgreSQL).
- **ProjectService** – manages projects and chart indicators (MongoDB).
- **Communication** – ProjectService calls UserService via HTTP using **FluentRest**.

---

## Technologies Used

- **.NET 8**
- **PostgreSQL**
- **MongoDB**
- **Docker / docker-compose**
- **FluentRest** (via Flurl)
- **xUnit** + **Moq** for unit testing
- **Integration tests** using `WebApplicationFactory` and `Mongo2Go`

---

## Features Implemented

- Two microservices with CRUD operations  
- PostgreSQL (users & subscriptions)  
- MongoDB (projects & user settings)  
- Microservice-to-microservice communication via HTTP  
- Dockerized with `docker-compose`  
- Unit and integration tests  
- Swagger enabled for both services  
- Custom endpoint:
