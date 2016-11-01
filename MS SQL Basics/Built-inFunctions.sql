-- 01. Find Names of All Employees by First Name
SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'SA%'
-- END

-- 02. Find Names of All Employees by Last Name
SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%ei%'
-- END

-- 03. Find First Names of All Employess
SELECT FirstName
FROM Employees
WHERE (DepartmentID = 3 OR DepartmentID = 10) AND (Year(HireDate) >= 1995 AND Year(HireDate) <= 2005)
-- END

-- 04. Find All Employees Except Engineers
SELECT FirstName, LastName
FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'
-- END

-- 05. Find Towns with Name
Select Name
From Towns
Where LEN(Towns.Name) = 5 OR LEN(Towns.Name) = 6
ORDER BY Towns.Name
-- END

-- 06. Find Towns Starting With
SELECT TownID, Name
FROM Towns
WHERE Towns.Name LIKE 'M%' OR Towns.Name LIKE 'K%' OR Towns.Name LIKE 'B%' OR Towns.Name LIKE 'E%'
ORDER BY Towns.Name
-- END

-- 07. Find Towns Not Starting With
SELECT TownID, Name
FROM Towns
WHERE Name NOT LIKE 'R%' AND Name NOT LIKE 'B%'  AND Name NOT LIKE 'D%'
ORDER BY Name
-- END

-- 08. Create View Employees Hired After
CREATE VIEW [V_EmployeesHiredAfter2000] AS
SELECT FirstName, LastName
FROM Employees
WHERE YEAR(HireDate) > 2000
-- END

-- 09. Length of Last Name
SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5
-- END

-- 10. Countries Holding 'A'
SELECT CountryName, IsoCode as [ISO Code]
FROM Countries
WHERE (LEN(CountryName) - LEN(REPLACE(CountryName, 'a', ''))) >= 3
ORDER BY IsoCode
-- END

-- 11. Mix of Peak and River Names
SELECT PeakName, RiverName, (LOWER(SUBSTRING(PeakName, 1, LEN(PeakName) - 1) + RiverName)) as Mix
FROM Peaks, Rivers
WHERE SUBSTRING(PeakName, LEN(PeakName), 1) = SUBSTRING(RiverName, 1, 1)
ORDER BY Mix
-- END

-- 12. Games From 2011 and 2012 Year
SELECT TOP 50 Name as [Game],
	(FORMAT(Start, 'yyyy-MM-dd', 'en-US')) as [Start]
FROM Games
WHERE YEAR(Games.Start) IN (2011, 2012)
ORDER BY Games.Start, Games.Name
-- END

-- 13. User Email Providers
SELECT Username, (SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email) - CHARINDEX('@', Email) + 1)) as [Email Provider]
FROM Users
ORDER BY [Email Provider], Username
-- END

-- 14. Get Users with IPAddress Like Pattern
SELECT Username, IpAddress as [IP Address]
FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username
-- END

-- 15. Show All Games with Duration
SELECT Name, [Part of the Day] = (
	CASE
		WHEN DATEPART(HOUR, Start) >= 0 AND DATEPART(HOUR, Start) < 12 THEN 'Morning'
		WHEN DATEPART(HOUR, Start) >= 12 AND DATEPART(HOUR, Start) < 18 THEN 'Afternoon'
		WHEN DATEPART(HOUR, Start) >= 18 AND DATEPART(HOUR, Start) < 24 THEN 'Evening'
	END
), Duration = (
	CASE
		WHEN Duration <= 3 THEN 'Extra Short'
		WHEN Duration >= 4 AND Duration <= 6 THEN 'Short'
		WHEN Duration > 6 THEN 'Long'
		ELSE 'Extra Long'
	END
)
FROM Games
ORDER BY Name, [Duration], [Part of the Day]
-- END

-- 16. Orders Table
SELECT ProductName, OrderDate, (DATEADD(DAY, 3, OrderDate)) as [Pay Due], (DATEADD(MONTH, 1, OrderDate)) as [Deliver Due]
FROM ORDERS
-- END