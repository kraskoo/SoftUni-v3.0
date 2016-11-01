-- 04. Insert Records in Both Tables
INSERT INTO [Towns] (Id, Name)
VALUES (1, 'Sofia');
INSERT INTO [Towns] (Id, Name)
VALUES (2, 'Plovdiv');
INSERT INTO [Towns] (Id, Name)
VALUES (3, 'Varna');
INSERT INTO [Minions] (Id, Name, Age, TownId)
VALUES (1, 'Kevin', 22, 1);
INSERT INTO [Minions] (Id, Name, Age, TownId)
VALUES (2, 'Bob', 15, 3);
INSERT INTO [Minions] (Id, Name, TownId)
VALUES (3, 'Steward', 2);
-- End

-- 07. Create Table People
CREATE TABLE People (
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name varchar(200) NOT NULL,
	Picture varbinary(MAX) CHECK(Picture <= 2097152),
	Height float(2),
	Width float(2),
	Gender char(1) NOT NULL CHECK(Gender IN ('m', 'f')),
	Birthdate date NOT NULL,
	Biography nvarchar(MAX)
);

INSERT INTO People (
	Name,
	Gender,
	Birthdate)
VALUES ('Ivan Borisov', 'm', '2-21-1989'),
	('Albena Karuzova', 'f', '6-17-1990'),
	('Nikolai Hristov', 'm', '3-5-1983'),
	('Stoianka Kostadinova', 'f', '11-26-1991'),
	('Pesho Familirianov', 'm', '3-13-1985');
-- End

-- 08. Create Table Users
CREATE TABLE Users (
	Id BIGINT PRIMARY KEY IDENTITY(1, 1),
	Name VARCHAR(30) DEFAULT NULL UNIQUE(Name) CHECK(Name IS NOT NULL),
	[Password] VARCHAR(26) DEFAULT NULL CHECK([Password] IS NOT NULL),
	ProfilePicture VARBINARY(MAX) CHECK(ProfilePicture <= 921600),
	LastLoginTime DATE,
	IsDelete BIT DEFAULT 0
);

INSERT INTO Users (
	Name,
	[Password]
) VAlUES ('Pesho', 'pesho444'),
	('Gosho', 'gosho222'),
	('Ivan', 'ivan999'),
	('Dragan', 'dragan111'),
	('Petkan', 'petkan777');
-- END

-- 13. Movies Database
CREATE TABLE Directors (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	DirectorName NVARCHAR(100) NOT NULL,
	Notes TEXT
);

CREATE TABLE Genres (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	GenreName NVARCHAR(100) NOT NULL,
	Notes TEXT
);

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	CategoryName NVARCHAR(100) NOT NULL,
	Notes TEXT
);

CREATE TABLE Movies (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Title NVARCHAR(100) NOT NULL,
	DirectorId INT,
	CopyrightYear INT NOT NULL,
	[Length] INT NOT NULL,
	GenreId INT,
	CategoryId INT,
	Rating INT CHECK (Rating >= 1 AND Rating <= 10),
	Notes TEXT,
	FOREIGN KEY (DirectorId) REFERENCES Directors(Id),
	FOREIGN KEY (GenreId) REFERENCES Genres(Id),
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

INSERT INTO Directors (DirectorName)
VALUES ('Misho'),
	('Gesha'),
	('Tosho'),
	('Sasho'),
	('Raho');

INSERT INTO Genres (GenreName)
VALUES ('Sci-fi'),
	('Documentary'),
	('Action'),
	('Horror'),
	('Fantasy');

INSERT INTO Categories (CategoryName)
VALUES ('Adventure film'),
	('Art film'),
	('Amature film'),
	('Propaganda film'),
	('Parody film');

INSERT INTO Movies (
	Title,
	DirectorId,
	CopyrightYear,
	[Length],
	GenreId,
	CategoryId,
	Rating
) VALUES ('Nocturnal Animals',
	2, 2008, 135, 4, 1, 6),
	('Rats',
	4, 2016, 105, 1, 3, 9),
	('Fifty Shades Darker',
	3, 2011, 81, 2, 4, 7),
	('The Promise',
	1, 1994, 76, 5, 2, 2),
	('Free Fire',
	5, 1998, 93, 3, 5, 8);
-- END

-- 14. Car Rental Database
CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Category NVARCHAR(30) NOT NULL,
	DailyRate DECIMAL(5, 2) DEFAULT 0,
	WeeklyRate DECIMAL(6, 2) DEFAULT 0,
	MontlyRate DECIMAL(7, 2) DEFAULT 0,
	WeekendRate DECIMAL(7, 2) DEFAULT 0
);

CREATE TABLE Cars(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	PlateNumber NVARCHAR(10) NOT NULL,
	Make INT,
	Model NVARCHAR(30) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT NOT NULL,
	Door INT DEFAULT 4,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(20) DEFAULT 'Good',
	Available BIT DEFAULT 0,
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Tile NVARCHAR(40),
	Notes TEXT
);

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	DrivenLicenseNumber NVARCHAR(10) NOT NULL,
	FullName NVARCHAR(60) NOT NULL,
	Address NVARCHAR(100),
	ZipCode NVARCHAR(10),
	Notes TEXT
);

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	EmployeeId INT,
	CustomerId INT NOT NULL,
	CarId INT NOT NULL,
	CarCondition NVARCHAR(20),
	TankLevel FLOAT(2) NOT NULL,
	KilometrageStart DECIMAL(15, 2) NOT NULL,
	KilometrageEnd DECIMAL(15, 2),
	TotalKilometrage DECIMAL(15, 2),
	StartDate DATE NOT NULL,
	EndDate DATE,
	TotalDays INT,
	RateApplied NVARCHAR(20)
		CHECK(RateApplied IN ('DailyRate', 'WeeklyRate', 'MontlyRate', 'WeekendRate')),
	TaxRate DECIMAL(20, 2),
	OrderStatus NVARCHAR(40),
	Notes TEXT,
	FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
	FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
	FOREIGN KEY (CarId) REFERENCES Cars(Id)
);

INSERT INTO Categories(
	Category, DailyRate) VALUES ('light', 783.23);
INSERT INTO Categories(
	Category, MontlyRate) VALUES ('cabrio', 546.23);
INSERT INTO Categories(
	Category, WeekendRate) VALUES ('heavy', 8795.23);
INSERT INTO Cars(PlateNumber, Model, CarYear, CategoryId) VALUES (
	'CA4211TB', 'Porsche Carrera 911 GT3', 2004, 1);
INSERT INTO Cars(PlateNumber, Model, CarYear, CategoryId) VALUES (
	'CA1756HE', 'BMW Z4 GT3', 2010, 2);
INSERT INTO Cars(PlateNumber, Model, CarYear, CategoryId) VALUES (
	'CA2955CT', 'Mercedes-AMG GT3', 2013, 3);
INSERT INTO Employees (FirstName, LastName) VALUES('Pesho', 'Petrov');
INSERT INTO Employees (FirstName, LastName) VALUES('Dragan', 'Cankov');
INSERT INTO Employees (FirstName, LastName) VALUES('Ivan', 'Ivanov');
INSERT INTO Customers (DrivenLicenseNumber, FullName) VALUES(
	'MORGA60SM9', 'Pesho Petrov');
INSERT INTO Customers (DrivenLicenseNumber, FullName) VALUES(
	'KERPV72IU8', 'Dragan Cankov');
INSERT INTO Customers (DrivenLicenseNumber, FullName) VALUES(
	'UITHA23OP5', 'Ivan Ivanov');
INSERT INTO RentalOrders (CustomerId, CarId, TankLevel,
	KilometrageStart, StartDate) VALUES(
		2, 1, 220.2, 275000, '6-23-2014');
	UPDATE ro
	SET ro.CarCondition = c.Condition
	FROM RentalOrders as ro, Cars as c
	WHERE ro.CarId = 1;
	UPDATE ro
	SET ro.RateApplied = (
		CASE
			WHEN c.DailyRate >= 0 THEN 'DailyRate'
			WHEN c.MontlyRate >= 0 THEN 'MontlyRate'
			WHEN c.WeekendRate >= 0 THEN 'WeekendRate'
			WHEN c.WeeklyRate >= 0 THEN 'WeeklyRate'
		END)
	FROM RentalOrders as ro, Categories as c
	WHERE ro.CarId = 1 AND c.Id = (
		SELECT car.CategoryId
		FROM Cars as car
		WHERE car.Id = 1);
INSERT INTO RentalOrders (CustomerId, CarId, TankLevel,
	KilometrageStart, StartDate) VALUES(
		1, 3, 342.5, 115000, '2-5-2014');
	UPDATE ro
	SET ro.CarCondition = c.Condition
	FROM RentalOrders as ro, Cars as c
	WHERE ro.CarId = 3;
	UPDATE ro
	SET ro.RateApplied = (
		CASE
			WHEN c.DailyRate >= 0 THEN 'DailyRate'
			WHEN c.MontlyRate >= 0 THEN 'MontlyRate'
			WHEN c.WeekendRate >= 0 THEN 'WeekendRate'
			WHEN c.WeeklyRate >= 0 THEN 'WeeklyRate'
		END)
	FROM RentalOrders as ro, Categories as c
	WHERE ro.CarId = 3 AND c.Id = (
		SELECT car.CategoryId
		FROM Cars as car
		WHERE car.Id = 3);
INSERT INTO RentalOrders (CustomerId, CarId, TankLevel,
	KilometrageStart, StartDate) VALUES(
		3, 2, 271.9, 85000, '9-9-2014');
	UPDATE ro
	SET ro.CarCondition = c.Condition
	FROM RentalOrders as ro, Cars as c
	WHERE ro.CarId = 2;
	UPDATE ro
	SET ro.RateApplied = (
		CASE
			WHEN c.DailyRate >= 0 THEN 'DailyRate'
			WHEN c.MontlyRate >= 0 THEN 'MontlyRate'
			WHEN c.WeekendRate >= 0 THEN 'WeekendRate'
			WHEN c.WeeklyRate >= 0 THEN 'WeeklyRate'
		END)
	FROM RentalOrders as ro, Categories as c
	WHERE ro.CarId = 2 AND c.Id = (
		SELECT car.CategoryId
		FROM Cars as car
		WHERE car.Id = 2);
-- END

-- 15. Hotel Database
CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Tile NVARCHAR(40),
	Notes TEXT
);

CREATE TABLE Customers(
	AccountNumber INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	EmergencyName NVARCHAR(60) NOT NULL,
	EmergencyNumber INT NOT NULL,
	Notes TEXT,
	CONSTRAINT UK_AccountNumber UNIQUE(AccountNumber)
);

CREATE TABLE RoomStatus(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	RoomStatus NVARCHAR(20) NOT NULL,
	Note TEXT
);

CREATE TABLE RoomTypes(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	RoomType NVARCHAR(20) NOT NULL,
	Note TEXT
);

CREATE TABLE BedTypes(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	BedType NVARCHAR(20) NOT NULL,
	Note TEXT
);

CREATE TABLE Rooms(
	RoomNumber INT PRIMARY KEY NOT NULL,
	RoomType NVARCHAR(20) NOT NULL,
	BedType NVARCHAR(20) NOT NULL,
	Rate INT,
	RoomStatus NVARCHAR(20) NOT NULL,
	Note TEXT,
	CONSTRAINT UK_RoomNumber UNIQUE(RoomNumber),
	CONSTRAINT CK_Rate
		CHECK(Rate >= 1 AND Rate <= 10)
);

CREATE TABLE Payments(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	EmployeeId INT NOT NULL,
	PaymentDate DATE NOT NULL,
	AccountNumber INT NOT NULL,
	FirstDateOccupied DATE,
	LastDateOccupied DATE,
	TotalDays INT,
	AmountCharged INT,
	TaxRate DECIMAL(6, 2),
	TaxAmount DECIMAL(7, 2),
	PaymentTotal DECIMAL(9, 2),
	Notes TEXT
);

CREATE TABLE Occupancies(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	EmployeeId INT,
	PaymentDate DATE NOT NULL,
	AccountNumber INT NOT NULL,
	RoomNumber INT NOT NULL,
	RateApplied INT,
	PhoneCharge INT,
	Notes TEXT
);

INSERT INTO Employees(FirstName, LastName) VALUES('Asen', 'Radev');
INSERT INTO Employees(FirstName, LastName) VALUES('Ivailo', 'Ivanov');
INSERT INTO Employees(FirstName, LastName) VALUES('Martin', 'Zlatev');
INSERT INTO Customers(AccountNumber, FirstName, LastName, EmergencyName, EmergencyNumber)
	VALUES(1, 'Stoian', 'Petrov', '', '');
INSERT INTO Customers(AccountNumber, FirstName, LastName, EmergencyName, EmergencyNumber)
	VALUES(2, 'Nikolai', 'Nikolov', '', '');
INSERT INTO Customers(AccountNumber, FirstName, LastName, EmergencyName, EmergencyNumber)
	VALUES(3, 'Damian', 'Damianov', '', '');
INSERT INTO RoomStatus(RoomStatus) VALUES('Free');
INSERT INTO RoomStatus(RoomStatus) VALUES('Occupied');
INSERT INTO RoomStatus(RoomStatus) VALUES('Renovation');
INSERT INTO RoomTypes(RoomType) VALUES('One-Room');
INSERT INTO RoomTypes(RoomType) VALUES('Two-Room');
INSERT INTO RoomTypes(RoomType) VALUES('Three-Room');
INSERT INTO BedTypes(BedType) VALUES('Single-Person');
INSERT INTO BedTypes(BedType) VALUES('Couple');
INSERT INTO BedTypes(BedType) VALUES('Bigger');
INSERT INTO Rooms(RoomNumber, RoomType, BedType, RoomStatus) VALUES(
	23, 'Two-Room', 'Couple', 'Free');
INSERT INTO Rooms(RoomNumber, RoomType, BedType, RoomStatus) VALUES(
	17, 'One-Room', 'Bigger', 'Free');
INSERT INTO Rooms(RoomNumber, RoomType, BedType, RoomStatus) VALUES(
	62, 'Three-Room', 'Single-Person', 'Free');
INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber) VALUES(2, '5-23-2013', 1);
INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber) VALUES(1, '7-11-2014', 3);
INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber) VALUES(3, '8-19-2015', 2);
INSERT INTO Occupancies(EmployeeId, PaymentDate, AccountNumber, RoomNumber)
	VALUES(1, '7-23-2013', 2, 23);
INSERT INTO Occupancies(EmployeeId, PaymentDate, AccountNumber, RoomNumber)
	VALUES(3, '1-6-2014', 3, 17);
INSERT INTO Occupancies(EmployeeId, PaymentDate, AccountNumber, RoomNumber)
	VALUES(2, '12-15-2015', 1, 62);
-- END

-- 19. Basic Select All Fields
SELECT * FROM Towns;
SELECT * FROM Departments;
SELECT * FROM Employees;
-- END

-- 20. Basic Select All Fields and Order Them
SELECT * FROM Towns ORDER BY Name;
SELECT * FROM Departments ORDER BY Name;
SELECT * FROM Employees ORDER BY Salary DESC;
-- END

-- 21. Basic Select Some Fields
SELECT Name FROM Towns ORDER BY Name;
SELECT Name FROM Departments ORDER BY Name;
SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC;
-- END

-- 22. Increase Employees
UPDATE Employees
SET Salary = Salary * 1.10;

SELECT Salary FROM Employees;
-- END

-- 23. Decrease Tax Rate
UPDATE Payments
SET TaxRate = TaxRate - (TaxRate * 0.03);

SELECT TaxRate FROM Payments;
-- END

-- 24. Delete All Records
TRUNCATE TABLE Occupancies;
-- END