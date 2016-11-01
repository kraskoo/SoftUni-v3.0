-- 01. One-To-One Relationship
CREATE TABLE Persons(
	PersonID INT IDENTITY(1, 1),
	FirstName NVARCHAR(50) NOT NULL,
	Salary MONEY,
	PassportID INT);

CREATE TABLE Passports(
	PassportID INT IDENTITY(101, 1),
	PassportNumber NVARCHAR(50) NOT NULL);

ALTER TABLE Persons
ADD CONSTRAINT PK_Persons PRIMARY KEY(PersonID);

ALTER TABLE Passports
ADD CONSTRAINT PK_Passports PRIMARY KEY(PassportID);

ALTER TABLE Persons
ADD CONSTRAINT FK_Persons_Passport FOREIGN KEY(PassportID)
	REFERENCES Passports(PassportID);

INSERT INTO Passports VALUES('N34FG21B'), ('K65LO4R7'), ('ZE657QP2');
INSERT INTO Persons VALUES('Roberto', 43300, 102),
	('Tom', 56100, 103),
	('Yana', 60200, 101);
-- END

-- 02. One-To-Many Relationship
CREATE TABLE Manufacturers(
	ManufacturerID INT PRIMARY KEY IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	EstablishedON DATE NOT NULL);

CREATE TABLE Models(
	ModelID INT PRIMARY KEY IDENTITY(101, 1),
	Name NVARCHAR(50) NOT NULL,
	ManufacturerID INT);

ALTER TABLE Models
ADD CONSTRAINT FK_Models_Manufacturers FOREIGN KEY(ManufacturerID)
	REFERENCES Manufacturers(ManufacturerID);

INSERT INTO Manufacturers VALUES('BMW', '03/07/1916'),
	('Tesla', '01/01/2003'),
	('Lada', '05/01/1966');

INSERT INTO Models VALUES('X1', 1),
	('i6', 1),
	('Model S', 2),
	('Model X', 2),
	('Model 3', 2),
	('Nova', 3);
-- END

-- 03. Many-To-Many Relationship
CREATE TABLE Students(
	StudentID INT IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL
	PRIMARY KEY(StudentID));

CREATE TABLE Exams(
	ExamID INT IDENTITY(101, 1),
	Name NVARCHAR(50) NOT NULL
	PRIMARY KEY(ExamID));

CREATE TABLE StudentsExams(
	StudentID INT NOT NULL,
	ExamID INT NOT NULL
	FOREIGN KEY(StudentID) REFERENCES
		Students(StudentID),
	FOREIGN KEY(ExamID) REFERENCES
		Exams(ExamID),
	PRIMARY KEY(StudentID, ExamID));

INSERT INTO Students VALUES('Mila'), ('Toni'), ('Ron');

INSERT INTO Exams VALUES('Spring MVC'), ('Neo4j'), ('Oracle 11g');
-- END

-- 04. Self-Referencing
CREATE TABLE Teachers(
	TeacherID INT IDENTITY(101, 1),
	Name NVARCHAR(50) NOT NULL,
	ManagerID INT
	PRIMARY KEY(TeacherID)
	FOREIGN KEY(ManagerID) REFERENCES
		Teachers(TeacherID));

INSERT INTO Teachers VALUES('John', NULL),
	('Maya', 106), ('Silvia', 106), ('Ted', 105), ('Mark', 101), ('Greta', 101);
-- END

-- 05. Online Store Database
CREATE TABLE Cities(
	CityID INT IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	PRIMARY KEY(CityID));

CREATE TABLE Customers(
	CustomerID INT IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	Birthday DATE,
	CityID INT,
	PRIMARY KEY (CustomerID),
	FOREIGN KEY(CityID) REFERENCES
		Cities(CityId));

CREATE TABLE Orders(
	OrderID INT IDENTITY(1, 1),
	CustomerID INT,
	PRIMARY KEY(OrderID),
	FOREIGN KEY(CustomerID) REFERENCES
		Customers(CustomerID));
			
CREATE TABLE ItemTypes(
	ItemTypeID INT IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL
	PRIMARY KEY(ItemTypeID));

CREATE TABLE Items(
	ItemID INT IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	ItemTypeID INT,
	PRIMARY KEY(ItemID),
	FOREIGN KEY(ItemTypeID) REFERENCES
		ItemTypes(ItemTypeID));
		
CREATE TABLE OrderItems(
	OrderID INT NOT NULL,
	ItemID INT NOT NULL
	FOREIGN KEY(OrderID) REFERENCES
		Orders(OrderID),
	FOREIGN KEY(ItemID) REFERENCES
		Items(ItemID),
	PRIMARY KEY(OrderID, ItemID));
-- END

-- 06. University Database
CREATE TABLE Majors(
	MajorID INT PRIMARY KEY IDENTITY(1, 1),
	Name VARCHAR(50) NOT NULL);

CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY(1, 1),
	StudentNumber VARCHAR(12),
	StudentName VARCHAR(50) NOT NULL,
	MajorID INT NOT NULL
	FOREIGN KEY(MajorID) REFERENCES Majors(MajorID));

CREATE TABLE Payments(
	PaymentID INT PRIMARY KEY IDENTITY(1, 1),
	PaymentDate DATE NOT NULL,
	PaymentAmount DECIMAL(8, 2) NOT NULL,
	StudentID INT UNIQUE NOT NULL,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID));

CREATE TABLE Subjects(
	 SubjectID INT PRIMARY KEY IDENTITY(1, 1),
	 SubjectName VARCHAR(50) NOT NULL);

CREATE TABLE Agenda(
	StudentID INT NOT NULL,
	SubjectID INT NOT NULL,
	FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
	PRIMARY KEY(StudentID, SubjectID));
-- END

-- 09. Employee Address
SELECT TOP 5 e.EmployeeID, e.JobTitle, a.AddressID, a.AddressText
FROM Employees e JOIN Addresses a
	ON e.AddressID = a.AddressID
ORDER BY a.AddressID
-- END

-- 10. Employee Departments
SELECT TOP 5 e.EmployeeID, e.FirstName, e.Salary, d.Name [DepartmentName]
FROM Employees e JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
-- END

-- 11. Employees Without Projects
SELECT TOP 3 e.EmployeeID, e.FirstName
FROM Employees e LEFT JOIN EmployeesProjects ep
	ON e.EmployeeID = ep.EmployeeID
WHERE ep.ProjectID IS NULL
ORDER BY e.EmployeeID
-- END

-- 12. Employees With Project
SELECT TOP 5 e.EmployeeID,
	e.FirstName,
	p.Name
FROM Employees e JOIN EmployeesProjects ep
	ON e.EmployeeID = ep.EmployeeID JOIN
	Projects p ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '08.13.2002' AND p.EndDate IS NULL
ORDER BY e.EmployeeID
-- END

-- 13. Employee 24
SELECT e.EmployeeID,
	e.FirstName,
	(CASE
		WHEN YEAR(p.StartDate) >= 2005 THEN NULL
		ELSE p.Name
	END) as ProjectName
FROM Projects p JOIN EmployeesProjects ep
	ON p.ProjectID = ep.ProjectID JOIN Employees e
	ON ep.EmployeeID = e.EmployeeID
WHERE e.EmployeeID = 24
-- END

-- 14. Employee Manager
SELECT EmployeeID,
	FirstName,
	(CASE
		WHEN ManagerID = 3 THEN 3
		ELSE 7
	END) AS ManagerID,
	(CASE
		WHEN ManagerID = 3 THEN (SELECT e.FirstName FROM Employees e WHERE e.EmployeeID = 3)
		ELSE (SELECT e.FirstName FROM Employees e WHERE e.EmployeeID = 7)
	END) AS ManagerName
FROM Employees
WHERE ManagerID IN (3, 7)
ORDER BY EmployeeID
-- END

-- 15. Highest Peaks in Bulgaria
SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries c JOIN MountainsCountries mc
	ON c.CountryCode = mc.CountryCode JOIN Mountains m
	ON mc.MountainId = m.Id JOIN Peaks p
	ON m.Id = p.MountainId
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC
-- END

-- 16. Count Mountain Ranges
SELECT mc.CountryCode, COUNT(m.MountainRange) AS MountainRanges
FROM Mountains m JOIN MountainsCountries mc
	ON m.Id = mc.MountainId
GROUP BY mc.CountryCode
HAVING mc.CountryCode IN ('BG', 'US', 'RU')
-- END

-- 17. Countries With or Without Rivers
SELECT c.CountryName, r.RiverName
FROM (SELECT TOP 5 c.CountryCode, c.CountryName
FROM Countries c JOIN Continents ct
	ON c.ContinentCode = ct.ContinentCode
WHERE ct.ContinentName = 'Africa'
ORDER BY c.CountryName) c LEFT JOIN CountriesRivers cr
	ON c.CountryCode = cr.CountryCode LEFT JOIN Rivers r
	ON cr.RiverId = r.Id
-- END

-- 18. Continents and Currencies
DECLARE @MaxCurrencyUsageByContinentCode TABLE(
	ContinentCode CHAR(2) PRIMARY KEY NOT NULL,
	MaxCurrencyUsage INT);
DECLARE @CurrencyUsageByContinentCode TABLE(
	ContinentCode CHAR(2),
	CurrencyCode CHAR(3),
	CurrencyUsage INT);

INSERT INTO @MaxCurrencyUsageByContinentCode
SELECT cu.ContinentCode, MAX(cu.CurrencyUsage) AS MaxCurrencyUsage
FROM (SELECT ContinentCode, CurrencyCode, COUNT(CurrencyCode) AS CurrencyUsage
FROM Countries
GROUP BY CurrencyCode, ContinentCode
HAVING COUNT(CurrencyCode) > 1) cu JOIN Countries c
	ON cu.ContinentCode = c.ContinentCode
GROUP BY cu.ContinentCode

INSERT INTO @CurrencyUsageByContinentCode
SELECT ContinentCode, CurrencyCode, COUNT(CurrencyCode) AS CurrencyUsage
FROM Countries
GROUP BY CurrencyCode, ContinentCode
HAVING COUNT(CurrencyCode) > 1

SELECT mcu.ContinentCode, cu.CurrencyCode, mcu.MaxCurrencyUsage AS CurrencyUsage
FROM @MaxCurrencyUsageByContinentCode mcu JOIN @CurrencyUsageByContinentCode cu
	ON mcu.MaxCurrencyUsage = cu.CurrencyUsage
WHERE mcu.MaxCurrencyUsage = cu.CurrencyUsage AND
	mcu.ContinentCode = cu.ContinentCode
-- END

--  19. Countries Without any Mountains
SELECT COUNT(*) AS CountryCode
FROM Countries c FULL JOIN MountainsCountries mc
	ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL
-- END

-- 20. Highest Peak and Longest River by Country
SELECT TOP 5 *
FROM (SELECT c.CountryName, p.HighestPeakElevation, r.LongestRiverLength
FROM Countries c
	JOIN
	(SELECT c.CountryName, MAX(r.Length) AS LongestRiverLength
	FROM Countries c LEFT JOIN CountriesRivers cr
		ON c.CountryCode = cr.CountryCode LEFT JOIN Rivers r
		ON cr.RiverId = r.Id
		GROUP BY c.CountryName) r ON c.CountryName = r.CountryName
	JOIN
		(SELECT c.CountryName,
			MAX(p.Elevation) AS HighestPeakElevation
		FROM Countries c LEFT JOIN MountainsCountries mc
			ON c.CountryCode = mc.CountryCode LEFT JOIN Peaks p
			ON mc.MountainId = p.MountainId
			GROUP BY c.CountryName) p ON c.CountryName = p.CountryName
GROUP BY c.CountryName, p.HighestPeakElevation, r.LongestRiverLength) country
ORDER BY country.HighestPeakElevation DESC,
	country.LongestRiverLength DESC, country.CountryName
-- END

-- 21. Highest Peak Name and Elevation by Country
SELECT TOP 5 c.CountryName,
	(CASE
		WHEN mc.CountryCode IS NULL THEN '(no highest peak)'
		ELSE p.HighestPeakName
	END) AS HighestPeakName,
	(CASE
		WHEN mc.CountryCode IS NULL THEN 0
		ELSE p.HighestPeakElevation
	END) AS HighestPeakElevation,
	(CASE
		WHEN mc.CountryCode IS NULL THEN '(no mountain)'
		ELSE p.Mountain
	END) AS Mountain
FROM Countries c LEFT JOIN MountainsCountries mc
	ON c.CountryCode = mc.CountryCode LEFT JOIN
	(SELECT m.Id, p.PeakName AS HighestPeakName,
		MAX(p.Elevation) AS HighestPeakElevation,
		m.MountainRange AS Mountain
	FROM Mountains m JOIN Peaks p
		ON m.Id = p.MountainId
	GROUP BY m.Id, m.MountainRange, p.PeakName) p
	ON mc.MountainId = p.Id
GROUP BY c.CountryName,
	mc.CountryCode,
	p.Id,
	p.HighestPeakName,
	p.HighestPeakElevation,
	p.Mountain
ORDER BY c.CountryName, p.HighestPeakName
-- END