using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Models.DTO;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Repositories.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext ctx;
        public MovieService(DatabaseContext _ctx)
        {
            this.ctx = _ctx;
        }

        public bool Add(Movie model)
        {
            try 
            {
                ctx.Movies.Add(model);
                ctx.SaveChanges();
                foreach (var genreId in model.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = model.Id,
                        GenreId = genreId
                    };
                    ctx.MovieGenres.Add(movieGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = ctx.Movies.Find(id);
                if (data == null) 
                {
                    return false;
                }
                ctx.Movies.Remove (data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public Movie GetById(int id)
        {
            return ctx.Movies.Find(id);
        }

        public MovieListVm List()
        {
            var list = ctx.Movies.AsQueryable();
            var data = new MovieListVm
            {
                MovieList= list,
            };
            return data;
        }

        public bool Update(Movie model)
        {
            try
            {
                ctx.Movies.Update(model);
                ctx.SaveChanges(); 
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
