INSERT Into DefaultConnection.dbo.UserRating (Rating, MyMovieID, UserMovieLIstID) SELECT MovieRatings.dbo.Ratings.Rating, MyMovieID, UserMovieListID From MovieRatings.dbo.Ratings 
LEFT JOIN MovieRatings.dbo.Movies ON MovieRatings.dbo.Ratings.MovieID = MovieRatings.dbo.Movies.Id
INNER JOIN DefaultConnection.dbo.MyMovie ON DefaultConnection.dbo.MyMovie.Title = MovieRatings.dbo.Movies.Title
LEFT JOIN  DefaultConnection.dbo.UserMovieList ON DefaultConnection.dbo.UserMovieList.UserName = MovieRatings.dbo.Ratings.Id