using MovieScoring.Domain.ScoreCalculator;
using MovieScoring.Model;
using MovieScoring.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MovieScoring
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReviewService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReviewService.svc or ReviewService.svc.cs at the Solution Explorer and start debugging.
    public class ReviewService : IReviewService
    {
        private ReviewRepository reviewRepository;
        private MovieRepository movieRepository;
        private ScoreCalculator scoreCalculator;

        public ReviewService()
        {
             reviewRepository = new ReviewRepository();
             movieRepository = new MovieRepository();
            scoreCalculator = new AvaregeScoreCalculator(reviewRepository, movieRepository);
        }
        public void deleteReview(string id)
        {
            int intId = Int32.Parse(id);
            reviewRepository.Delete(intId);
        }

        public List<ReviewDto> getAllReviews()
        {
            return reviewRepository.GetAll().Select(review => ReviewDto.CreateFrom(review)).ToList();
        }

        public ReviewDto getReview(string id)
        {
            int intId = Int32.Parse(id);
            return ReviewDto.CreateFrom(reviewRepository.Get(intId));
        }

        public List<ReviewDto> getReviewsForMovie(string id)
        {
            int intId = Int32.Parse(id);
            return reviewRepository
                .GetAll()
                .FindAll(review => review.Movie.Id == intId)
                .Select(review => ReviewDto.CreateFrom(review))
                .ToList();
        }

        public ReviewDto publishReview(ReviewDto review)
        {
            if (review.Score > 10 || review.Score < 0)
            {
                throw new Exception();
            }

            if (review.Movie.Id == 0)
            {
                throw new Exception("Movie not set");
            }

            Review reviewModel = review.toModel();
            reviewModel.Movie = movieRepository.Get(review.Movie.Id);
            reviewModel.fillDates();

            Review savedReview = reviewRepository.Save(reviewModel);
            scoreCalculator.calculate(review.Movie.Id);

            return ReviewDto.CreateFrom(savedReview);
        }

        public ReviewDto updateReview(string id, ReviewDto review)
        {
            int intId = Int32.Parse(id);

            Review reviewModel = review.toModel();
            reviewModel.UpdatedAt = DateTime.Now;

            Review updatedReview = reviewRepository.Update(intId, reviewModel);
            return ReviewDto.CreateFrom(updatedReview);
        }
    }
}
