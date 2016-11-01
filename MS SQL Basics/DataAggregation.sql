-- 01. Records’ Count
SELECT COUNT(*) as [Count]
FROM WizzardDeposits
-- END

-- 02. Longest Magic Wand
SELECT MAX(MagicWandSize) as [LongestMagicWand]
FROM WizzardDeposits
-- END

-- 03. Longest Magic Wand per Deposit Groups
SELECT DepositGroup, MAX(MagicWandSize) as [LongestMagicWand]
FROM WizzardDeposits
GROUP BY DepositGroup
-- END

-- 04. Smallest Deposit Group per Magic Wand Size
DECLARE @MinAvg INT;
SET @MinAvg = CAST(
(SELECT TOP 1 AVG(MagicWandSize) average
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY average) AS INT);

SELECT DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
HAVING AVG(MagicWandSize) = @MinAvg
-- END

-- 05. Deposits Sum
SELECT DepositGroup, SUM(DepositAmount) as TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup
-- END

-- 06. Deposits Sum for Ollivander Family
SELECT DepositGroup, SUM(DepositAmount) as TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
-- END

-- 07. Deposits Filter
SELECT DepositGroup, SUM(DepositAmount) as TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC
-- END

-- 08. Deposit Charge
SELECT w.DepositGroup, w.MagicWandCreator, MIN(d.DepositCharge) [MinDepositCharge]
FROM WizzardDeposits d JOIN (SELECT DISTINCT mwc.MagicWandCreator, dg.DepositGroup
FROM WizzardDeposits mwc JOIN(
	SELECT DISTINCT DepositGroup
	FROM WizzardDeposits) dg ON mwc.DepositGroup = dg.DepositGroup) w
		ON d.DepositGroup = w.DepositGroup AND d.MagicWandCreator = w.MagicWandCreator
GROUP BY w.DepositGroup, w.MagicWandCreator
ORDER BY w.MagicWandCreator, w.DepositGroup
-- END

-- 09. Age Groups
SELECT y.AgeGroup, COUNT(y.AgeGroup) [WizzardCount]
FROM(SELECT [AgeGroup] = (CASE
	WHEN years.Age > 0 AND years.Age <= 10 THEN '[0-10]'
	WHEN years.Age > 10 AND years.Age <= 20 THEN '[11-20]'
	WHEN years.Age > 20 AND years.Age <= 30 THEN '[21-30]'
	WHEN years.Age > 30 AND years.Age <= 40 THEN '[31-40]'
	WHEN years.Age > 40 AND years.Age <= 50 THEN '[41-50]'
	WHEN years.Age > 50 AND years.Age <= 60 THEN '[51-60]'
	ELSE '[61+]'
END)
FROM WizzardDeposits years) y
GROUP BY y.AgeGroup
-- END

-- 10. First Letter
SELECT SUBSTRING(FirstName, 1, 1) [first_letter]
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY SUBSTRING(FirstName, 1, 1)
-- END

-- 11. Average Interest
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest)
FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC
-- END

-- 12. Rich Wizard, Poor Wizard
DECLARE @HostWizzard INT;
DECLARE @GuestWizzard INT;
DECLARE @SumDifference DECIMAL(10, 2);
DECLARE @Length INT;
SET @HostWizzard = 1;
SET @GuestWizzard = 2;
SET @SumDifference = 0;
SET @Length = (SELECT COUNT(*) FROM WizzardDeposits);

WHILE (@GuestWizzard <= @Length)
BEGIN
	SET @SumDifference = @SumDifference + ((SELECT DepositAmount
	FROM WizzardDeposits WHERE Id = @HostWizzard) - (SELECT DepositAmount
	FROM WizzardDeposits WHERE Id = @GuestWizzard))
	SET @HostWizzard = @HostWizzard + 1;
	SET @GuestWizzard = @GuestWizzard + 1;
END

SELECT @SumDifference as [SumDifference]
-- END

-- 13. Employees Minimum Salaries
SELECT e.DepartmentID, MIN(e.Salary)
FROM Employees e
WHERE e.HireDate > '01/01/2000' AND e.DepartmentID IN (2, 5, 7)
GROUP BY e.DepartmentID
-- END

-- 14. Employees Average Salaries
DECLARE @HighSalaries TABLE(
	DepartmentID INT,
	ManagerID INT,	
	Salary MONEY);
INSERT INTO @HighSalaries
SELECT DepartmentID,
	ManagerID,
	Salary
FROM Employees
WHERE Salary > 30000

DELETE FROM @HighSalaries
WHERE ManagerID = 42

UPDATE @HighSalaries
SET Salary = Salary + 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) [AverageSalary]
FROM @HighSalaries
GROUP BY DepartmentID
-- END

-- 15. Employees Maximum Salaries
SELECT m.DepartmentID, m.MaxSalary
FROM (SELECT DepartmentID, MAX(Salary) [MaxSalary]
FROM Employees
GROUP BY DepartmentID) m
WHERE m.MaxSalary NOT BETWEEN 30000 AND 70000
-- END

-- 16. Employees Count Salaries
SELECT COUNT(Salary) [Count]
FROM Employees
WHERE ManagerID IS NULL
-- END

-- 17. 3rd Highest Salary
SELECT DepartmentID, 
	(SELECT DISTINCT Salary FROM Employees WHERE DepartmentID = e.DepartmentID ORDER BY Salary DESC OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY)AS ThirdHighestSalary
FROM Employees e
WHERE (SELECT DISTINCT Salary FROM Employees WHERE DepartmentID = e.DepartmentID ORDER BY Salary DESC OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY) IS NOT NULL
GROUP BY DepartmentID
-- END

-- 18. Salary Challenge
SELECT TOP 10 e.FirstName, e.LastName, e.DepartmentID
FROM (
SELECT DepartmentID,
AVG(Salary) AverageSalary
FROM Employees
GROUP BY DepartmentID) a JOIN Employees e
	ON a.DepartmentID = e.DepartmentID
GROUP BY e.DepartmentID, e.FirstName, e.LastName, e.Salary, a.AverageSalary
HAVING e.Salary > a.AverageSalary
-- END