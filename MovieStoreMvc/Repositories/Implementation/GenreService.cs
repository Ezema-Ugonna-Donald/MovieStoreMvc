using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext ctx;
        public GenreService(DatabaseContext _ctx)
        {
            this.ctx = _ctx;
        }

        public bool Add(Genre model)
        {
            try 
            {
                ctx.Genres.Add(model);
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
                var data = ctx.Genres.Find(id);
                if (data == null) 
                {
                    return false;
                }
                ctx.Genres.Remove (data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            return ctx.Genres.Find(id);
        }

        public IQueryable<Genre> List()
        {
            var data = ctx.Genres.AsQueryable();
            return data;
        }

        public bool Update(Genre model)
        {
            try
            {
                ctx.Genres.Update(model);
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
