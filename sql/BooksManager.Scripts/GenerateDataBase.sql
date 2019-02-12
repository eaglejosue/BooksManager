USE MASTER
GO

CREATE DATABASE BooksManager
GO

CREATE TABLE Books (
    Id bigint NOT NULL IDENTITY,
    Title varchar(100) NOT NULL,
    Description varchar(500) NOT NULL,
    Price decimal(18,2) NOT NULL,
    Author varchar(100) NOT NULL,
    Year int NOT NULL,
    Publisher varchar(100) NULL,
    Edition int NOT NULL,
    Tag varchar(50) NULL,
    Summary varchar(50) NULL,
    CONSTRAINT PK_Books PRIMARY KEY (Id)
);
GO

CREATE TABLE Customers (
    Id bigint NOT NULL IDENTITY,
    Name varchar(100) NOT NULL,
    Email varchar(100) NOT NULL,
    Telephone varchar(12) NOT NULL,
    BirthDate datetime2 NOT NULL,
    CONSTRAINT PK_Customers PRIMARY KEY (Id)
);
GO

CREATE TABLE Bookings (
    Id bigint NOT NULL IDENTITY,
    StartTime datetime2 NOT NULL,
    EndTime datetime2 NOT NULL,
    Price decimal(18,2) NOT NULL,
    BookId bigint NOT NULL,
    CustomerId bigint NULL,
    CONSTRAINT PK_Bookings PRIMARY KEY (Id),
    CONSTRAINT FK_Bookings_Books_BookId FOREIGN KEY (BookId) REFERENCES Books (Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Bookings_Customers_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers (Id) ON DELETE NO ACTION
);
GO

CREATE INDEX IX_Bookings_BookId ON Bookings (BookId);
GO

CREATE INDEX IX_Bookings_CustomerId ON Bookings (CustomerId);
GO