using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MovieScoring
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMovieService" in both code and config file together.
    [ServiceContract]
    public interface IMovieService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movies")]
        List<MovieDto> getAllMovies();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movies/{id}")]
        MovieDto getMovie(string id);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "movies/{id}/calculate-score")]
        int calculateScore(string id);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "movies", BodyStyle = WebMessageBodyStyle.Bare)]
        MovieDto addMovie(MovieDto movie);
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "movies/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        void deleteMovie(string id);
        [OperationContract]
        [WebInvoke(Method = "PATCH", UriTemplate = "movies/{id}", BodyStyle = WebMessageBodyStyle.Bare)]
        MovieDto updateMovie(string id, MovieDto movie);
    }

    [DataContract(Namespace = "")]
    [KnownType(typeof(ActorDto))]
    [KnownType(typeof(DirectorDto))]
    public class MovieDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember]
        public DateTime AirDate { get; set; }
        [DataMember]
        public DirectorDto Director { get; set; }
        [DataMember]
        public virtual List<ActorDto> Actors { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MovieDto() { }

        public MovieDto(int id, string name, int score, DateTime airDate, DirectorDto director, List<ActorDto> actors, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Score = score;
            AirDate = airDate;
            Director = director;
            Actors = actors;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static MovieDto CreateFrom(Movie movie)
        {
            List<ActorDto> actorDtoList;
            if (movie.Actors == null)
            {
                actorDtoList = new List<ActorDto>();
            }
            else
            {
                actorDtoList = movie.Actors.Select(actor => ActorDto.CreateFrom(actor)).ToList();
            }
            return new MovieDto(movie.Id, movie.Name, movie.Score, movie.AirDate, DirectorDto.CreateFrom(movie.Director), actorDtoList, movie.CreatedAt, movie.UpdatedAt);
        }

        public Movie toModel()
        {
            List<Actor> actors = new List<Actor>();
            if(Actors != null)
            {
                actors = Actors.Select(actor => actor.toModel()).ToList();
            }
            if(Director == null)
            {
                Director = new DirectorDto();
            }

            return new Movie(Id, Name, Score, AirDate, Director.toModel(), actors, CreatedAt, UpdatedAt);
        }
    }

    [DataContract(Namespace = "")]
    public class ActorDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ActorDto() { }
        public ActorDto(int id, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

        }

        public static ActorDto CreateFrom(Actor actor)
        {
            return new ActorDto(actor.Id, actor.Name, actor.CreatedAt, actor.UpdatedAt);
        }

        public Actor toModel()
        {
            return new Actor(Id, Name, CreatedAt, UpdatedAt);
        }
    }

    [DataContract(Namespace = "")]
    public class DirectorDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DirectorDto() { }
        public DirectorDto(int id, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

        }

        public static DirectorDto CreateFrom(Director director)
        {
            return new DirectorDto(director.Id, director.Name, director.CreatedAt, director.UpdatedAt);
        }

        public Director toModel()
        {
            return new Director(Id, Name, CreatedAt, UpdatedAt);
        }

    }
}
