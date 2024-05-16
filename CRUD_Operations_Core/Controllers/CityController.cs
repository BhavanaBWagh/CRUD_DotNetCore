using CRUD_Operations_Core.Data;
using CRUD_Operations_Core.Models.DB_Operations;
using CRUD_Operations_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CRUD_Operations_Core.Controllers
{
    public class CityController : Controller
    {

        public readonly CityRepository _cityRepository = null;
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
            _cityRepository = new CityRepository(_context);
        }




        public IActionResult Index(string SortColumn = "Id", string Iconclass = "fa-sort-asc", int PageNo = 1)
        {
            if (SortColumn == null) { SortColumn = "Id"; }
            ViewBag.SortColumn = SortColumn;
            ViewBag.Iconclass = Iconclass;
            List<City> city = _cityRepository.GetCities(SortColumn, Iconclass, PageNo);
            ViewBag.countries=_context.Countries;
            return View(city);
        }

        public IActionResult Search(string search = "", int PageNo = 1, string SortColumn = "Id")
        {

            if (search == null) { search = ""; }
            ViewBag.value = search;
            List<City> city = _cityRepository.SearchCity(search);
            return View("Index", city);
        }

        [HttpGet]
        public IActionResult Create()
        {
          City city = new City();
            ViewBag.country = _cityRepository.GetCountries();
            return View(city);
        }

        [HttpPost]
        public IActionResult Create(City city)
        {
            bool result = false;

            if (ModelState.IsValid)
            {
                result = _cityRepository.CreateCity(city);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.country = _cityRepository.GetCountries();
            return View("Create", city);
        }

        public IActionResult Details(int id)
        {
            City city;
            city = _cityRepository.GetCityDetailsObj(id);
            ViewBag.country = _context.Countries.ToList();
            return View(city);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<City> city;
            city = _cityRepository.GetCityDetails(id);
            return View(city);
        }

        [HttpPost]
        public IActionResult Delete(City c)
        {
            List<City> city = null;
            try
            {
                city = _cityRepository.GetCityDetails(c.Id);
                bool result = _cityRepository.DeleteCity(c.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
                return View(city);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            City city;
            city = _cityRepository.GetCityDetailsObj(id);
            ViewBag.country = _context.Countries.ToList();
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(int id, City _c)
        {
            if (ModelState.IsValid && _c != null)
            {
                _cityRepository.EditCity(_c);
                return RedirectToAction("Index");
            }
            ViewBag.country =_context.Countries.ToList();
            return RedirectToAction("Edit");
        }


        [HttpGet]
        public IActionResult CreateModalForm(int countryid)
        {
            City city = new City();
            city.CountryId = countryid;
            city.CountryName = GetCountryName(countryid);
            return PartialView("_CreateModalForm", city);
        }

        [HttpPost]
        public IActionResult CreateModalForm(City city)
        {
            _context.Add(city);
            _context.SaveChanges();
            return NoContent();
        }

        private string GetCountryName(int countryId)
        {
            if (countryId==0)
            {
                return "";
            }

            String strCountryName = _context.Countries
                .Where(ct => ct.Id == countryId)
                .Select(ct => ct.Name).Single().ToString();

            return strCountryName;
        }

    }
}
    
