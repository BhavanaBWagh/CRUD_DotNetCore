using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Core.Models;
using CRUD_Operations_Core.Models.DB_Operations;
using CRUD_Operations_Core.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_Operations_Core.Controllers
{
    public class CountryController : Controller
    {
        public readonly CountryRepository _countryRepository = null;
        private readonly AppDbContext _context;

        public CountryController(AppDbContext context)
        {
            _context = context;
            _countryRepository = new CountryRepository(_context);
        }

        //public IActionResult Index()
        //{
        //    List<Country> country = _countryRepository.GetCountries();
        //    return View("Index",country);
        //}


        public IActionResult Index(string SortColumn = "Id", string Iconclass = "fa-sort-asc", int PageNo = 1)
        {
            if(SortColumn == null) { SortColumn = "Id";}
            ViewBag.SortColumn = SortColumn;
            ViewBag.Iconclass = Iconclass;
            List<Country> country =  _countryRepository.GetCountries(SortColumn,Iconclass,PageNo);

            //#region Paging Logic
            //int NoOfRecords = 4;
            //int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(country.Count) / Convert.ToDouble(NoOfRecords)));
            //int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecords;

            //ViewBag.NoOfRecords = NoOfRecords;
            //ViewBag.NoOfPage = NoOfPage;
            //ViewBag.PageNo = PageNo;

            //country = country.Skip(NoOfRecordsToSkip).Take(NoOfRecords).ToList();
            //#endregion
            return View(country);
        }

        public IActionResult Search(string search = "", int PageNo = 1, string SortColumn = "Id")
        {
            
            if (search == null) { search = ""; }
            ViewBag.value = search;
            List<Country> country= _countryRepository.SearchCountry(search);

            //#region Paging Logic
            //int NoOfRecords = 4;
            //int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(country.Count) / Convert.ToDouble(NoOfRecords)));
            //int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecords;

            //ViewBag.NoOfRecords = NoOfRecords;
            //ViewBag.NoOfPage = NoOfPage;
            //ViewBag.PageNo = PageNo;

            //country = country.Skip(NoOfRecordsToSkip).Take(NoOfRecords).ToList();
            //#endregion
            return View("Index", country);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Country country = new Country();
          //  List<Country> country = _countryRepository.GetCountries();
            return View(country);
        }

        [HttpPost]
        public IActionResult Create(Country country)
        {
            bool result = false;
            
            if (ModelState.IsValid)
            {
                result = _countryRepository.CreateCountry(country);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Create",country);
        }

        public IActionResult Details(int id)
        {
            List<Country> country;
            country = _countryRepository.GetCountryDetails(id);
            return View(country);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Country> country=null;
           
                country = _countryRepository.GetCountryDetails(id);
                return View(country);
           
          
        }

        [HttpPost]
        public IActionResult Delete(Country c)
        {
            List<Country> country = null;
            try {
                country = _countryRepository.GetCountryDetails(c.Id);
                bool result = _countryRepository.DeleteCountry(c.Id);
            return RedirectToAction("Index");
             }  catch (Exception ex)
            {
              
                ModelState.AddModelError("", ex.InnerException.Message);
                return View(country);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Country country;
            country = _countryRepository.GetCountryDetailsObj(id);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(int id, Country _c)
        {
            if (ModelState.IsValid && _c != null)
            {
                _countryRepository.EditCountry(_c);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public IActionResult CreateModalForm()
        {
            Country country = new Country();
            return PartialView("_CreateModalForm",country);
        }

        [HttpPost]
        public IActionResult CreateModalForm(Country country)
        {
           _context.Add(country);
            _context.SaveChanges();
            return NoContent();
        }

        public JsonResult GetCountries()
        {
            var lstCountries = new List<SelectListItem>();

            List<Country> Countries = _context.Countries.ToList();

            lstCountries = Countries.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.Name
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            lstCountries.Insert(0, defItem);

            return Json(lstCountries);
        }

    }
}
