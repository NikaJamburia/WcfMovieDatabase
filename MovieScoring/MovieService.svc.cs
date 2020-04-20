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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MovieService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MovieService.svc or MovieService.svc.cs at the Solution Explorer and start debugging.
    public class MovieService : IMovieService
    {
        private MovieRepository movieRepository;
        private ReviewRepository reviewRepository;
        private DirectorRepository directorRepository;
        private ActorRepository actorRepository;
        private ScoreCalculator scoreCalculator;

        public MovieService()
        {
        movieRepository = new MovieRepository();
        reviewRepository = new ReviewRepository();
        directorRepository = new DirectorRepository();
        actorRepository = new ActorRepository();
        scoreCalculator = new AvaregeScoreCalculator(reviewRepository, movieRepository);
    }
        public MovieDto addMovie(MovieDto movie)
        {
            if(movie.Director.Id == 0)
            {
                throw new Exception("Director not set");
            }

            Movie movieModel = movie.toModel();
            movieModel.fillDates();
            movieModel.Director = directorRepository.Get(movie.Director.Id);
            movieModel.Actors.Clear();

            if(movie.Actors.Count != 0)
            {
                movie.Actors
                    .ForEach(actor => movieModel.Actors.Add(actorRepository.Get(actor.Id)));
            }

            Movie savedMovie = movieRepository.Save(movieModel);
            return MovieDto.CreateFrom(savedMovie);
        }

        public int calculateScore(string id)
        {
            int intId = Int32.Parse(id);
            return scoreCalculator.calculate(intId);
        }

        public void deleteMovie(string id)
        {
            int intId = Int32.Parse(id);
            movieRepository.Delete(intId);
        }

        public List<MovieDto> getAllMovies()
        {
            return movieRepository
                .GetAll()
                .Select(movie => MovieDto.CreateFrom(movie))
                .ToList();
        }

        public MovieDto getMovie(string id)
        {
            int intId = Int32.Parse(id);
            return MovieDto.CreateFrom(movieRepository.Get(intId));
        }

        public MovieDto updateMovie(string id, MovieDto movie)
        {
            int intId = Int32.Parse(id);

            Movie movieModel = movie.toModel();
            movieModel.UpdatedAt = DateTime.Now;

            Movie updatedMovie = movieRepository.Update(intId, movie.toModel());
            return MovieDto.CreateFrom(updatedMovie);
        }
    }
}
