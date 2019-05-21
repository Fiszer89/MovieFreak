BULK
INSERT Movies
FROM 'C:\Users\Fiszerek\Desktop\praca magisterska\ml-latest-small\movies.csv'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n',
FIRSTROW = 1
)
GO
--Check the content of the table.
SELECT *
FROM Movie
GO
--Drop the table to clean up database.
DROP TABLE Movie
GO