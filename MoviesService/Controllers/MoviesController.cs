using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesLibrary;

namespace MoviesService.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        [HttpGet]
        public ActionResult Index(string sortField)
        {
            var searchFieldValues =
                typeof (MovieData)
                    .GetProperties()
                    .Select(property => property.Name.ToLower())
                    .Where(propertyName => !string.IsNullOrWhiteSpace(Request.QueryString.Get(propertyName)))
                    .ToDictionary(
                        propertyName => propertyName,
                        propertyName => Request.QueryString.Get(propertyName));

            return Json(moviesRepository.GetAll(sortField, searchFieldValues), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(MovieData model)
        {
            moviesRepository.Create(model);

            return Json(new { successful = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(MovieData model)
        {
            moviesRepository.Update(model);

            return Json(new { successful = true }, JsonRequestBehavior.AllowGet);
        }
    }
}