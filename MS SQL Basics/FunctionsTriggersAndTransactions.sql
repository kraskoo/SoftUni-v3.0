-- 01. Employees with Salary Above 35000
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS
BEGIN
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary > 35000
END
-- END

-- 02. Employees with Salary Above Number
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(
	@Salary DECIMAL(10, 2))
AS
BEGIN
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary >= @Salary
END
-- END

-- 03. Town Names Starting With
CREATE PROCEDURE usp_GetTownsStartingWith(
	@TownsStartWith NVARCHAR(50))
AS
BEGIN
	SELECT Name
	FROM Towns
	WHERE Name LIKE (@TownsStartWith + '%')
END
-- END

-- 04. Employees from Town
CREATE PROCEDURE usp_GetEmployeesFromTown(
	@TownName NVARCHAR(50))
AS
BEGIN
	SELECT e.FirstName, e.LastName
	FROM Employees e JOIN
	(SELECT a.AddressID
	FROM Towns t JOIN Addresses a ON
		t.TownID = a.TownID
	WHERE t.Name = @TownName) a ON e.AddressID = a.AddressID
END
-- END

-- 05. Salary Level Function
CREATE FUNCTION ufn_GetSalaryLevel(@salary MONEY)
	RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @SalaryLevel VARCHAR(7) =
	CASE
		WHEN @salary < 30000 THEN 'Low'
		WHEN @salary BETWEEN 30000 AND 50000 THEN 'Average'
		ELSE 'High'
	END

	RETURN @SalaryLevel
END
-- END

-- 07. Define Function
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX))
	RETURNS BIT
AS
BEGIN
	DECLARE @setAsVar NVARCHAR(MAX) = LOWER(@setOfLetters);
	DECLARE @wordAsVar NVARCHAR(MAX) = LOWER(@word);
	DECLARE @tableSet TABLE(Id INT PRIMARY KEY IDENTITY(1, 1),
		Letter NCHAR);
	DECLARE @tableWord TABLE(Id INT PRIMARY KEY IDENTITY(1, 1),
		Letter NCHAR);
	DECLARE @startIndex INT = 1;
	DECLARE @lenght INT = LEN(@setAsVar);
	WHILE @lenght >= @startIndex
	BEGIN
		INSERT INTO @tableSet VALUES(SUBSTRING(@setAsVar, @startIndex, 1));
		SET @startIndex = @startIndex + 1;
	END

	SET @startIndex = 1
	SET @lenght = LEN(@wordAsVar);
	WHILE @lenght >= @startIndex
	BEGIN
		INSERT INTO @tableWord VALUES(SUBSTRING(@wordAsVar, @startIndex, 1));
		SET @startIndex = @startIndex + 1;
	END

	DECLARE @sortedword TABLE(Id INT PRIMARY KEY IDENTITY(1, 1), Letter NCHAR);
	INSERT INTO @sortedword
	SELECT Letter FROM @tableWord ORDER BY Letter;

	DECLARE @sortedset TABLE(Id INT PRIMARY KEY IDENTITY(1, 1), Letter NCHAR);
	INSERT INTO @sortedset
	SELECT Letter FROM @tableSet ORDER BY Letter;
	DECLARE @wordStr NVARCHAR(300) =
		STUFF((SELECT
			LOWER(Letter) + ''
		FROM @sortedword
		FOR XML PATH('')), 1, 0, '');
	DECLARE @set NVARCHAR(300) =
		STUFF((SELECT
			LOWER(Letter) + ''
		FROM @sortedset
		FOR XML PATH('')), 1, 0, '');

	DECLARE @initialSetLenght INT = LEN(@set);
	DECLARE @wordStartIndex INT = 1;
	DECLARE @wordLength INT = LEN(@wordStr);
	DECLARE @result BIT = 1;
	WHILE @wordLength >= @wordStartIndex
	BEGIN
		IF CHARINDEX(SUBSTRING(@wordStr, @wordStartIndex, 1), @set) = 0
			SET @result = 0;
		SET @wordStartIndex = @wordStartIndex + 1;
	END
	RETURN @result;
END
-- END

-- 09. Find Full Name
CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT (FirstName + ' ' + LastName) AS [Full Name]
	FROM AccountHolders
END
-- END

-- 10. People with Balance Higher Than
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@sum MONEY)
AS
BEGIN 
	SELECT FirstName AS [First Name],
		LastName AS [Last Name]
	FROM (SELECT FirstName,
			LastName,
			SUM(a.Balance) AS TotalBalance
		FROM AccountHolders AS ah JOIN Accounts AS a
			ON a.AccountHolderId = ah.Id
		GROUP BY ah.FirstName, ah.LastName) AS tb
	WHERE tb.TotalBalance > @sum
END
-- END

-- 11. Future Value Function
CREATE FUNCTION ufn_CalculateFutureValue(@intialSum DECIMAL(20, 10),
	@earlyInterestRate DECIMAL(20, 10), @years DECIMAL(20, 10))
	RETURNS MONEY
AS
BEGIN
	DECLARE @FV MONEY;
	SET @FV = @intialSum*(Power((1 + @earlyInterestRate), @years));
	RETURN @FV;
END
-- END

-- 13. Deposit Money Procedure
CREATE PROCEDURE usp_DepositMoney
	@AccountId INT,
	@moneyAmount MONEY
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Accounts
		SET Balance = Balance + @moneyAmount
		WHERE Id = @AccountId		
	COMMIT TRANSACTION
END
-- END

-- 14. Withdraw Money Procedure
CREATE PROCEDURE usp_WithdrawMoney
	@AccountId INT,
	@moneyAmount MONEY
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE Accounts
		SET Balance = Balance - @moneyAmount
		WHERE Id = @AccountId		
	COMMIT TRANSACTION
END
-- END

-- 18. Cash in User Games Odd Rows
CREATE FUNCTION ufn_CashInUsersGames (@gameName NVARCHAR(50))
	RETURNS @userGameCash TABLE([SumCash] MONEY NOT NULL)
AS
BEGIN
	DECLARE @gameCash TABLE([SumCash] MONEY NOT NULL);
	INSERT INTO @gameCash
	SELECT SUM(rg.Cash) AS [SumCash]
	FROM (SELECT ug.Cash,
			ROW_NUMBER() OVER (ORDER BY ug.Cash DESC) AS RowNumber
		FROM UsersGames ug JOIN Games g
			ON ug.GameId = g.Id
		WHERE g.Name = @gameName) rg
	WHERE rg.RowNumber % 2 <> 0
	INSERT INTO @userGameCash
	SELECT *
	FROM @gameCash
	ORDER BY SumCash DESC
	RETURN
END
-- END

-- 20. Massive Shopping
BEGIN TRY
	DECLARE @GameId INT = (SELECT g.Id FROM Games g WHERE g.Name = 'Safflower');
	DECLARE @UserId INT = (SELECT u.Id FROM Users u WHERE u.Username = 'Stamat');
	BEGIN TRAN
		DECLARE @UserGameId int = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
		INSERT INTO UserGameItems (UserGameId, ItemId)
			SELECT @UserGameId, Id FROM Items WHERE MinLevel IN (11, 12)
		UPDATE UsersGames SET Cash = Cash -(SELECT SUM(Price) FROM Items WHERE MinLevel IN (11, 12))
		WHERE Id = @UserGameId
	COMMIT TRAN
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN
END CATCH

BEGIN TRY
	SET @GameId = (SELECT g.Id FROM Games g WHERE g.Name = 'Safflower');
	SET @UserId = (SELECT u.Id FROM Users u WHERE u.Username = 'Stamat');
	BEGIN TRAN
		SET @UserGameId = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
		INSERT INTO UserGameItems (UserGameId, ItemId)
			SELECT @UserGameId, Id FROM Items WHERE MinLevel IN (19, 20, 21)
		UPDATE UsersGames SET Cash = Cash -(SELECT SUM(Price) FROM Items WHERE MinLevel IN (19, 20, 21))
		WHERE Id = @UserGameId
	COMMIT TRAN
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN
END CATCH

SET @GameId = (SELECT g.Id FROM Games g WHERE g.Name = 'Safflower');
SET @UserId = (SELECT u.Id FROM Users u WHERE u.Username = 'Stamat');
SET @UserGameId = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)

SELECT Name [Item Name] FROM Items WHERE Id IN (SELECT ItemId FROM UserGameItems WHERE UserGameId = @UserGameId) ORDER BY [Item Name]
-- END

-- 21. Number of Users for Email Provider
SELECT e.EProviders AS [Email Provider], COUNT(u.Email) [Number Of Users]
FROM Users u JOIN (SELECT Id, SUBSTRING(Email,
		CHARINDEX('@', Email, 1) + 1,
		LEN(Email) - CHARINDEX('@', Email, 1)) AS EProviders
	FROM Users) e
	ON u.Id = e.Id
WHERE u.Email LIKE ('%' + e.EProviders)
GROUP BY e.EProviders
ORDER BY [Number Of Users] DESC, [Email Provider]
-- END

-- 22. All Users in Games
SELECT g.Name, gt.Name, u.Username, ug.Level, ug.Cash, c.Name
FROM Users u JOIN UsersGames ug
	ON u.Id = ug.UserId JOIN Games g
	ON ug.GameId = g.Id JOIN GameTypes gt
	ON g.GameTypeId = gt.Id JOIN Characters c
	ON ug.CharacterId = c.Id
ORDER BY ug.Level DESC, u.Username, g.Name
-- END

-- 23. Users in Games with Their Items
SELECT u.Username, g.Name AS Game, COUNT(i.Id) AS [Items Count], SUM(i.Price) AS [Items Price]
FROM UserGameItems ugi JOIN UsersGames ug
	ON ugi.UserGameId = ug.Id JOIN Games g
	ON ug.GameId = g.Id JOIN Items i
	ON ugi.ItemId = i.Id JOIN Users u
	ON ug.UserId = u.Id
GROUP BY u.Username, g.Name
HAVING COUNT(i.Id) >= 10
ORDER BY [Items Count] DESC, [Items Price] DESC, u.Username
-- END

-- 24. * User in Games with Their Statistics
SELECT Username, g.Name Game, MAX(c.Name) Character, 
	SUM(its.Strength) + MAX(chs.Strength) + MAX(gts.Strength) [Strength], 
	SUM(its.Defence) + MAX(chs.Defence) + MAX(gts.Defence) [Defence], 
	SUM(its.Speed) + MAX(chs.Speed) + MAX(gts.Speed) [Speed], 
	SUM(its.Mind) + MAX(chs.Mind) + MAX(gts.Mind) [Mind], 
	SUM(its.Luck) + MAX(chs.Luck) + MAX(gts.Luck) [Luck]
FROM Users u
	LEFT JOIN UsersGames ug ON ug.UserId = u.Id 
	LEFT JOIN Games g ON g.Id = ug.GameId
	LEFT JOIN GameTypes gt ON g.GameTypeId = gt.Id
	LEFT JOIN UserGameItems ugi ON ugi.UserGameId = ug.Id
	LEFT JOIN Items i ON i.Id = ugi.ItemId
	LEFT JOIN Characters c ON c.Id = ug.CharacterId
	LEFT JOIN [Statistics] chs ON chs.Id = c.StatisticId
	LEFT JOIN [Statistics] gts ON gts.Id = gt.BonusStatsId
	LEFT JOIN [Statistics] its ON its.Id = i.StatisticId
	GROUP BY Username, g.Name
ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC
-- END

-- 25. All Items with Greater than Average Statistics
DECLARE @AvgMind MONEY = (SELECT AVG(st.Mind) FROM Items i JOIN [Statistics] st ON i.StatisticId = st.Id);
DECLARE @AvgLuck MONEY = (SELECT AVG(st.Luck) FROM Items i JOIN [Statistics] st ON i.StatisticId = st.Id);
DECLARE @AvgSpeed MONEY = (SELECT AVG(st.Speed) FROM Items i JOIN [Statistics] st ON i.StatisticId = st.Id);
SELECT i.Name, i.Price,
	i.MinLevel, st.Strength, st.Defence,
	st.Speed, st.Luck, st.Mind
FROM Items i JOIN [Statistics] st
	ON i.StatisticId = st.Id
WHERE st.Mind > @AvgMind AND st.Luck > @AvgLuck AND st.Speed > @AvgSpeed
ORDER BY i.Name
-- END

-- 26. Display All Items about Forbidden Game Type
SELECT i.Name AS Item,
	i.Price,
	i.MinLevel,
	gt.Name AS [Forbidden Game Type]
FROM GameTypeForbiddenItems gtfi FULL JOIN Items i
	ON gtfi.ItemId = i.Id FULL JOIN GameTypes gt
	ON gtfi.GameTypeId = gt.Id
ORDER BY gt.Name DESC, i.Name
-- END

-- 27. Buy Items for User in Game
DECLARE @GameId int = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
DECLARE @UserId int = (SELECT Id FROM Users WHERE Username = 'Alex')
DECLARE @UserGameId int = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)

INSERT INTO UserGameItems (UserGameId, ItemId)
SELECT @UserGameId, Id FROM Items WHERE Name IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet')

UPDATE UsersGames SET Cash = Cash -(SELECT SUM(Price) FROM Items
WHERE Name IN ('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet'))
WHERE Id = @UserGameId

SELECT Username, g.Name, Cash, i.Name [Item Name]
FROM Users u LEFT JOIN UsersGames ug
	ON u.Id = ug.UserId LEFT JOIN Games g
	ON g.Id = ug.GameId LEFT JOIN Characters c
	ON c.Id = ug.CharacterId LEFT JOIN UserGameItems ugi
	ON ugi.UserGameId = ug.Id LEFT JOIN Items i
	ON i.Id = ugi.ItemId
WHERE g.Name = 'Edinburgh'
GROUP BY Username, g.Name, i.Name, Cash, u.Id, g.Id
ORDER BY [Item Name]
-- END

-- 28. Peaks and Mountains
SELECT p.PeakName, m.MountainRange AS Mountain, p.Elevation
FROM Peaks p JOIN Mountains m
	ON p.MountainId = m.Id
ORDER BY p.Elevation DESC
-- END

-- 29. Peaks with Mountain, Country and Continent
SELECT p.PeakName, m.MountainRange AS Mountain, c.CountryName, c.ContinentName
FROM MountainsCountries mc JOIN (SELECT con.ContinentName,
	cou.CountryName, cou.CountryCode
	FROM Continents con JOIN Countries cou
		ON con.ContinentCode = cou.ContinentCode) c
	ON mc.CountryCode = c.CountryCode JOIN Mountains m
	ON mc.MountainId = m.Id JOIN Peaks p
	ON m.Id = p.MountainId
ORDER BY p.PeakName, c.CountryName
-- END

-- 30. Rivers by Country
SELECT cou.CountryName,
	con.ContinentName,
	rc.RiversCount,
	CASE
		WHEN rc.RiversCount = 0 THEN 0
		ELSE rc.TotalLength
	END AS TotalLength
FROM Continents con JOIN Countries cou
	ON con.ContinentCode = cou.ContinentCode JOIN (SELECT c.CountryCode,
		COUNT(r.Id) AS RiversCount, SUM(r.Length) AS TotalLength
	FROM CountriesRivers cr FULL JOIN Countries c
		ON cr.CountryCode = c.CountryCode FULL JOIN Rivers r
		ON cr.RiverId = r.Id
	GROUP BY c.CountryCode) rc
	ON cou.CountryCode = rc.CountryCode
ORDER BY rc.RiversCount DESC, rc.TotalLength DESC, cou.CountryName
-- END

-- 31. Count of Countries by Currency
SELECT cur.CurrencyCode,
	cur.Description AS Currency,
	COUNT(c.CurrencyCode) AS NumberOfCountries
FROM Currencies cur LEFT JOIN Countries c
	ON cur.CurrencyCode = c.CurrencyCode
GROUP BY cur.CurrencyCode, cur.Description
ORDER BY NumberOfCountries DESC, Currency
-- END

-- 32. Population and Area by Continent
SELECT con.ContinentName,
	SUM(cou.AreaInSqKm) CountriesArea,
	SUM(CONVERT(DECIMAL, cou.Population)) AS CountriesPopulation
FROM Continents con JOIN Countries cou
	ON con.ContinentCode = cou.ContinentCode
GROUP BY con.ContinentName
ORDER BY CountriesPopulation DESC
-- END

-- 33. Monasteries by Country
CREATE TABLE Monasteries(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(MAX),
	CountryCode CHAR(2),
	CONSTRAINT FK_Monasteries_Countries FOREIGN KEY(CountryCode)
		REFERENCES Countries(CountryCode));

INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sumela Monastery', 'TR');

UPDATE Countries
SET IsDeleted = 1
WHERE CountryCode IN (SELECT CountryCode
FROM CountriesRivers
GROUP BY CountryCode
HAVING COUNT(RiverId) > 3);

SELECT m.Name, c.CountryName
FROM Monasteries m JOIN Countries c
	ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted <> 1
ORDER BY m.Name
-- END

-- 34. Monasteries by Continents and Countries
UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

INSERT INTO Monasteries VALUES(
	'Hanga Abbey', (SELECT TOP 1 CountryCode
FROM Countries
WHERE CountryName = 'Tanzania'));

INSERT INTO Monasteries VALUES(
	'Myin-Tin-Daik', (SELECT TOP 1 CountryCode
FROM Countries
WHERE CountryName = 'Myanmar'));

SELECT con.ContinentName,
	cou.CountryName,
	COUNT(m.Id) AS MonasteriesCount
FROM Continents con JOIN Countries cou
	ON con.ContinentCode = cou.ContinentCode LEFT JOIN Monasteries m
	ON cou.CountryCode = m.CountryCode
WHERE cou.IsDeleted <> 1
GROUP BY con.ContinentName, cou.CountryName
ORDER BY MonasteriesCount DESC, cou.CountryName
-- END