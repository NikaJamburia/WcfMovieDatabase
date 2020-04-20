using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieScoring.Model;
using MovieScoring.Repository;

namespace MovieScoring.Domain.ScoreCalculator
{
    public class AvaregeScoreCalculator : ScoreCalculator
    {
        private CrudRepository<Review> ReviewRepository;
        private CrudRepository<Movie> MovieRepository;

        public AvaregeScoreCalculator(CrudRepository<Review> reviewRepository, CrudRepository<Movie> movieRepository)
        {
            ReviewRepository = reviewRepository;
            MovieRepository = movieRepository;
        }

        public int calculate(int movieId)
        {
            List<Review> movieReviews = ReviewRepository.GetAll().FindAll(review => review.Movie.Id == movieId);
            int reviewCount = movieReviews.Count();
            int scoreSum = movieReviews
                .Select(review => review.Score)
                .Aggregate(0, (a, b) => a+b);

            if (reviewCount <= 0)
            {
                return 0;
            }

            int newScore = scoreSum / reviewCount;

            Movie movieToUpdate = MovieRepository.Get(movieId);
            movieToUpdate.Score = newScore;
            MovieRepository.Update(movieId, movieToUpdate);

            return newScore;
        }
    }
}