# persons-minimal-api

Simple CRUD API built with ASP.NET Core Minimal API and SQL Server, using Dapper and stored procedures for data access.
Dapper is used as a lightweight ORM to keep queries fast and explicit, avoiding the overhead of heavier abstractions.

---

## Stack

* ASP.NET Core
* Dapper
* SQL Server

---

## 📁 Project Structure

```plaintext
MinimalAPI_CRUD/
│
├── Entities/
│   └── Person.cs
│
├── Repositories/
│   ├── IPersonRepository.cs
│   └── PersonRepository.cs
│
├── Program.cs
└── database.sql
```

---

## Endpoints

### Get all persons

GET /persons

**Responses:**

* 200 OK

---

### Get person by ID

GET /persons/{id}

**Responses:**

* 200 OK
* 404 Not Found

---

### Create person

POST /persons

**Body:**

```json
{
  "name": "Fernando",
  "last_name": "Flores",
  "curp": "FOS12HCLLNSSA2"
}
```

**Responses:**

* 201 Created
* 400 Bad Request (duplicate CURP)
* 500 Internal Server Error

---

### Update person

PUT /persons/{id}

**Body:**

```json
{
  "idPerson": 1,
  "name": "Fernando",
  "last_name": "Flores",
  "curp": "FOS12HCLLNSSA2"
}
```

**Responses:**

* 204 No Content
* 400 Bad Request
* 404 Not Found

---

### Delete person

DELETE /persons/{id}

**Responses:**

* 204 No Content
* 404 Not Found

---

## Notes

* Uses repository pattern for data access
* Database access handled via stored procedures
* Handles SQL Server unique constraint (CURP) gracefully
* Focused on simplicity and clean structure
