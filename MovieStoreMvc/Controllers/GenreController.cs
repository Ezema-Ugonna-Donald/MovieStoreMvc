using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        public GenreController(IGenreService _genreService)
        {
            this.genreService = _genreService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            var result = genreService.Add(model);
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
            return View();
        }

        public IActionResult Edit(int id)
        {
            var data = genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";

                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
            return View();
        }

        public IActionResult GenreList ()
        {
            var data = this.genreService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }
    }
}
