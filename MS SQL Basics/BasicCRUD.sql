-- 02. Find All Information About Departments
SELECT * FROM Departments
-- END

-- 03. Find all Department Names
SELECT Name FROM Departments
-- END

-- 04. Find Salary of Each Employee
SELECT FirstName, LastName, Salary FROM Employees
-- END

-- 05. Find Full Name of Each Employee
SELECT FirstName, MiddleName, LastName FROM Employees
-- END

-- 06. Find Email Address of Each Employee
DECLARE @Domain CHAR(10);
SET @Domain = 'softuni.bg';

SELECT CONCAT(e.FirstName, '.', e.LastName, '@', @Domain) as [Full Email Address] FROM Employees e;
-- END

-- 07. Find All Different Employee’s Salaries
SELECT DISTINCT SALARY FROM Employees
-- END

-- 08. Find all Information About Employees
SELECT EmployeeID as [ID],
	e.FirstName as [First Name],
	e.LastName as [Last Name],
	e.MiddleName as [Middle Name],
	e.JobTitle as [Job Title],
	e.DepartmentID as [Dept ID],
	e.ManagerID as [Mngr ID],
	e.HireDate as [Hire Date],
	e.Salary,
	e.AddressID
FROM Employees e
WHERE e.JobTitle = 'Sales Representative'
--END

-- 09. Find Names of All Employees by Salary in Range
SELECT FirstName, LastName, JobTitle
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000
-- END

-- 10. Find Names of All Employees
SELECT (e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName) [Full Name]
FROM Employees e
WHERE e.Salary IN (14000, 12500, 23600, 25000)
-- END

-- 11. Find All Employees Without Manager
SELECT e.FirstName, e.LastName
FROM Employees e
WHERE e.ManagerID IS NULL
-- END

-- 12. Find All Employees with Salary More Than
SELECT e.FirstName, e.LastName, e.Salary
FROM Employees e
WHERE e.Salary > 50000
ORDER BY e.Salary DESC
-- END

-- 13. Find 5 Best Paid Employees
SELECT TOP 5 e.FirstName, e.LastName
FROM Employees e
ORDER BY e.Salary DESC
-- END

-- 14. Find All Employees Except Marketing
SELECT e.FirstName, e.LastName
FROM Employees e
WHERE e.DepartmentID != 4
-- END

-- 15. Sort Employees Table
SELECT e.EmployeeID as [ID],
	e.FirstName as [First Name],
	e.LastName as [Last Name],
	e.MiddleName as [Middle Name],
	e.JobTitle as [Job Title],
	e.DepartmentID as [Dept ID],
	e.ManagerID as [Mngr ID],
	e.HireDate as [Hire Date],
	e.Salary,
	AddressID
FROM Employees e
ORDER BY e.Salary DESC,
	e.FirstName ASC,
	e.LastName DESC,
	e.MiddleName ASC
-- END

-- 16. Create View Employees with Salaries
CREATE VIEW V_EmployeesSalaries AS
SELECT FirstName, LastName, Salary FROM Employees;
-- END

-- 17. Create View Employees with Job Titles
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT [Full Name] = (CASE
			WHEN e.MiddleName IS NULL THEN CONCAT(e.FirstName, '  ', e.LastName)
			ELSE CONCAT(e.FirstName, ' ', e.MiddleName, ' ', e.LastName)
		END),
		e.JobTitle as [Job Title]
FROM Employees e;
--END

-- 18. Distinct Job Titles
SELECT DISTINCT JobTitle FROM Employees
-- END

-- 19. Find First 10 Started Projects
SELECT TOP 10 p.ProjectID as [ID],
	p.Name,
	p.Description,
	p.StartDate,
	p.EndDate
FROM Projects p
ORDER BY StartDate, Name
-- END

-- 20. Last 7 Hired Employees
SELECT TOP 7 FirstName, LastName, HireDate
FROM Employees
ORDER BY HireDate DESC
-- END

-- 21. Increase Salaries
UPDATE Employees
SET Salary = Salary * 1.12
FROM Employees e
JOIN Departments d ON d.DepartmentID = e.DepartmentID
WHERE d.Name IN ('Engineering', 'Tool Design', 'Marketing', 'Information Services');

SELECT Salary
FROM Employees
-- END

-- 22. All Mountain Peaks
SELECT p.PeakName
FROM Peaks p
ORDER BY p.PeakName
-- END

-- 23. Biggest Countries by Population
SELECT TOP 30 c.CountryName, c.Population
FROM Countries c
WHERE c.ContinentCode = 'EU'
ORDER BY c.Population DESC
-- END

-- 24. Countries and Currency (Euro / Not Euro)
SELECT c.CountryName,
	c.CountryCode,
	Currency = (CASE
			WHEN c.CurrencyCode = 'EUR' THEN 'Euro'
			ELSE 'Not Euro'
		END)
FROM Countries c
ORDER BY c.CountryName
-- END

-- 25. All Diablo Characters
SELECT Name 
FROM Characters
ORDER BY Name, StatisticId
-- END