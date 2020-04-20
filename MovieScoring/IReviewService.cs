using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MovieScoring
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReviewService" in both code and config file together.
    [ServiceContract]
    public interface IReviewService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "reviews")]
        List<ReviewDto> getAllReviews();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "reviews/{id}")]
        ReviewDto getReview(string id);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movies/{id}/reviews")]
        List<ReviewDto> getReviewsForMovie(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "reviews", BodyStyle = WebMessageBodyStyle.Bare)]
        ReviewDto publishReview(ReviewDto review);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "reviews/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        void deleteReview(string id);
        [OperationContract]
        [WebInvoke(Method = "PATCH", UriTemplate = "reviews/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        ReviewDto updateReview(string id, ReviewDto review);
    }

    [DataContract(Namespace = "")]
    public class ReviewDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public MovieDto Movie { get; set; }
        [DataMember]
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ReviewDto(int id, string text, string authorname, MovieDto movie, int score, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Text = text;
            AuthorName = authorname;
            Movie = movie;
            Score = score;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static ReviewDto CreateFrom(Review review)
        {
            return new ReviewDto(review.Id, review.Text, review.AuthorName, MovieDto.CreateFrom(review.Movie), review.Score, review.CreatedAt, review.UpdatedAt);
        }

        public Review toModel()
        {
            return new Review(Id, Text, AuthorName, Movie.toModel(), Score, CreatedAt, UpdatedAt);
        }
    }
}
