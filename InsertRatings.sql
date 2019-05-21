BULK
INSERT Ratings
FROM 'C:\Users\Fiszerek\Desktop\praca magisterska\ml-latest-small\ratings.csv'

WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n',
FIRSTROW = 2
)
GO