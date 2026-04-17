CREATE DATABASE MinimalAPI_DB

USE MinimalAPI_DB

CREATE TABLE Persons (
    IDPerson INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Last_name NVARCHAR(100) NOT NULL,
    CURP NVARCHAR(18) NOT NULL
)


ALTER TABLE Persons
ADD CONSTRAINT UQ_Persons_CURP UNIQUE (CURP)




CREATE PROCEDURE SP_GetAllPersons
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * 
    FROM Persons;
END


CREATE PROCEDURE SP_InsertPerson
    @Name NVARCHAR(100),
    @Last_name NVARCHAR(100),
    @CURP NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Persons (Name, Last_name, CURP)
    VALUES (@Name, @Last_name, @CURP);

    SELECT SCOPE_IDENTITY();
END



CREATE PROCEDURE SP_GetPersonById
    @IDPerson INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IDPerson,
        Name,
        Last_name,
        CURP
    FROM Persons
    WHERE IDPerson = @IDPerson;
END
