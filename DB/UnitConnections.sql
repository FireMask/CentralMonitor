CREATE DATABASE UnitMonitor
GO

DROP TABLE IF EXISTS UnitConnections
GO

CREATE TABLE UnitConnections (
	Id INT PRIMARY KEY IDENTITY,
    UnitNo NVARCHAR(10),
	CONSTRAINT AK_UnitNo UNIQUE(UnitNo),
    DbServer NVARCHAR(255),
    DbName NVARCHAR(255),
    LastUpdate DATETIME,
    IsActive BIT,
    District NVARCHAR(100)
)
GO

--Example
INSERT INTO UnitConnections (UnitNo, DbServer, DbName, LastUpdate, IsActive, District)
VALUES (001, 'LIRAPC\SQLEXPRESS', 'Unit001', GETDATE(), 1, 'North')

INSERT INTO UnitConnections (UnitNo, DbServer, DbName, LastUpdate, IsActive, District)
VALUES (002, 'LIRAPC\SQLEXPRESS', 'Unit002', GETDATE(), 1, 'North')

INSERT INTO UnitConnections (UnitNo, DbServer, DbName, LastUpdate, IsActive, District)
VALUES (003, 'LIRAPC\SQLEXPRESS', 'Unit003', GETDATE(), 0, 'South')

SELECT * FROM UnitConnections