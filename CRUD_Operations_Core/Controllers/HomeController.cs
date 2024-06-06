using CRUD_Operations_Core.Data;
using CRUD_Operations_Core.Models;
using CRUD_Operations_Core.Models.DB_Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CRUD_Operations_Core.Controllers
{
    public class HomeController : Controller
    {
        public readonly PersonRepository _personRepository = null;
        private readonly AppDbContext _context;
        
        public HomeController(AppDbContext context)
        {
            _context = context;
            _personRepository = new PersonRepository(_context);
        }

        public IActionResult Index(string SortColumn = "Id", string Iconclass = "fa-sort-asc", int PageNo = 1)
        {
            if (SortColumn == null) { SortColumn = "Id"; }
            ViewBag.SortColumn = SortColumn;
            ViewBag.Iconclass = Iconclass;
            List<PersonDetail> person = _personRepository.GetPersons(SortColumn, Iconclass, PageNo);
          
            
            //#region Paging Logic
            //int NoOfRecords = 4;
            //int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(country.Count) / Convert.ToDouble(NoOfRecords)));
            //int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecords;

            //ViewBag.NoOfRecords = NoOfRecords;
            //ViewBag.NoOfPage = NoOfPage;
            //ViewBag.PageNo = PageNo;

            //country = country.Skip(NoOfRecordsToSkip).Take(NoOfRecords).ToList();
            //#endregion
            return View(person);
        }

        public IActionResult Search(string search = "", int PageNo = 1, string SortColumn = "Id")
        {

            if (search == null) { search = ""; }
            ViewBag.value = search;
            List<PersonDetail> person = _personRepository.SearchPerson(search);
            return View("Index", person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Person person = new Person();
            ViewBag.country = _personRepository.GetCountries();
          //  ViewBag.city = _personRepository.GetCities();
            return View(person);
        }

        [HttpPost]
        public IActionResult Create(Person p)
        {
            
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count >= 1)
                {
                    var file = Request.Form.Files[0];
                    byte[] ImgByte = new byte[file.Length];
                    
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        ImgByte = memoryStream.ToArray();
                    }
                   
                    var base64string = Convert.ToBase64String(ImgByte, 0, ImgByte.Length);
                    p.PhotoUrl = base64string;
                }
                else
                {
                    p.PhotoUrl = null;
                }
               
                ViewBag.country = _personRepository.GetCountries();
               // ViewBag.city = _personRepository.GetCities();
                _personRepository.CreatePerson(p);
            }
            else
            {
                
                return View("Create", p);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetCitiesByCountry(int countryId)
        {

            List<SelectListItem> cities = _context.Cities
              .Where(c => c.CountryId == countryId)
              .OrderBy(n => n.Name)
              .Select(n =>
              new SelectListItem
              {
                  Value = n.Id.ToString(),
                  Text = n.Name
              }).ToList();

            return Json(cities);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Person> person;
            person = _personRepository.GetPersonDetails(id);
            return View(person);
        }

        [HttpPost]
        public IActionResult Delete(Person p)
        {
            List<Person> person = null;
            try
            {
                person = _personRepository.GetPersonDetails(p.Id);
                bool result = _personRepository.DeletePerson(p.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
                return View(p);
            }

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Person person;
            person = _personRepository.GetPersonDetailsObj(id);
            person.CountryId = person.City.CountryId;
            ViewBag.city = _personRepository.GetCities(person.CountryId);
            ViewBag.country = _personRepository.GetCountries();
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(int id, Person _c)
        {

            if (ModelState.IsValid)
            {

                if (Request.Form.Files.Count >= 1)
                {
                    var file = Request.Form.Files[0];
                    byte[] ImgByte = new byte[file.Length];

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        ImgByte = memoryStream.ToArray();
                    }

                    var base64string = Convert.ToBase64String(ImgByte, 0, ImgByte.Length);

                    _c.PhotoUrl = base64string;
                }
                

                _personRepository.EditPerson(_c);
                
                return RedirectToAction("Index");
            }
            ViewBag.city = _personRepository.GetCities();
            ViewBag.country = _personRepository.GetCountries();
            return View("Edit",_c);
        }

        public IActionResult Details(int id)
        {
            Person person;
            person = _personRepository.GetPersonDetailsObj(id);
           
            return View(person);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}