using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IFileService _fileService;
        private readonly IGenreService _genreService;
        public MovieController(IMovieService movieService, IFileService fileService, IGenreService genreService)
        {
            this._movieService = movieService;
            this._fileService = fileService;
            this._genreService = genreService;
        }
        public IActionResult Add()
        {
            var model = new Movie();
            model.GenreList = _genreService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString()});

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Movie model)
        {
            model.GenreList = _genreService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });

            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var fileResult = this._fileService.SaveImage(model.ImageFile);
            if (fileResult.Item1 == 0) 
            {
                TempData["msg"] = "File could not save";
            }
            var imageName = fileResult.Item2;
            model.MovieImage = imageName;
            var result = _movieService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";

                return RedirectToAction (nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
            //return View();
        }

        public IActionResult Edit(int id)
        {
            var data = _movieService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Movie model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _movieService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";

                return RedirectToAction(nameof(MovieList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
            return View();
        }

        public IActionResult MovieList ()
        {
            var data = this._movieService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id);
            return RedirectToAction(nameof(MovieList));
        }
    }
}
