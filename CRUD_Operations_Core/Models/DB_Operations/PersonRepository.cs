using CRUD_Operations_Core.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations_Core.Models.DB_Operations
{
    public class PersonRepository
    {
        private readonly AppDbContext _context;
       
        public PersonRepository(AppDbContext context)
        {
            _context = context;
         
        }

        public List<Person> GetPersons()
        {
            List<Person> personListst;
            return personListst = _context.Person.ToList();
        }

        public List<Person> GetPersons(string SortColumn, string Iconclass, int PageNo)
        {
            #region Sort Logic

            List<Person> person = null;

            if (SortColumn == "Id")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.Person.OrderBy(temp => temp.Id).Include(obj => obj.City).Include(con=>con.City.Country).ToList();
                }
                else
                {
                    person = _context.Person.OrderByDescending(temp => temp.Id).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
            }

            if (SortColumn == "FirstName")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.Person.OrderBy(temp => temp.FirstName).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
                else
                {
                    person = _context.Person.OrderByDescending(temp => temp.FirstName).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
            }

            if (SortColumn == "LastName")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.Person.OrderBy(temp => temp.LastName).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
                else
                {
                    person = _context.Person.OrderByDescending(temp => temp.LastName).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
            }

            if (SortColumn == "Email")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.Person.OrderBy(temp => temp.EmailId).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
                else
                {
                    person = _context.Person.OrderByDescending(temp => temp.EmailId).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
            }
            if (SortColumn == "City")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.Person.OrderBy(temp => temp.City.Name).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
                else
                {
                    person = _context.Person.OrderByDescending(temp => temp.City.Name).Include(obj => obj.City).Include(con => con.City.Country).ToList();
                }
            }

            return person;

            #endregion

        }

        public List<Person> SearchPerson(string search = "")
        {
            List<Person> person;
            return person = _context.Person.Where(temp => temp.FirstName.Contains(search) || temp.LastName.Contains(search)).Include(obj => obj.City).Include(con => con.City.Country).ToList();
        }

        public bool CreatePerson(Person p)
        {
            _context.Person.Add(p);
            _context.SaveChanges();
            return true;
        }

        public List<SelectListItem> GetCountries()
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

            return lstCountries;
        }

        public List<SelectListItem> GetCities()
        {
            var lstCities = new List<SelectListItem>();

            List<City> City = _context.Cities.ToList();

            lstCities = City.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.Name
            }).ToList();


            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };

            lstCities.Insert(0, defItem);

            return lstCities;
        }

        public List<SelectListItem> GetCities(int countryId)
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

            return cities;
        }

        public List<Person> GetPersonDetails(int id)
        {
            List<Person> personList;
            personList = _context.Person.Where(temp => temp.Id == id).ToList();
            return personList;
        }

        public Person GetPersonDetailsObj(int id)
        {
            Person person;
            person = _context.Person.Include(co=>co.City).Include(con => con.City.Country).Where(temp => temp.Id == id).FirstOrDefault();
            return person;
        }

        public bool DeletePerson(int id)
        {
            Person personList;
            personList = _context.Person.Where(temp => temp.Id == id).FirstOrDefault();
            if (personList != null)
            {
                _context.Remove(personList);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public Person GePersonDetailsObj(int id)
        {
            Person person;
            person = _context.Person.Where(temp => temp.Id == id).FirstOrDefault();
            return person;
        }


        public bool EditPerson(Person c)
        {
            //Person person=new Person();
            if (c != null)
            {
                Person person = _context.Person.Where(temp => temp.Id ==c.Id).FirstOrDefault();
                person.Id = c.Id;
                person.FirstName = c.FirstName;
                person.LastName = c.LastName;
                person.EmailId = c.EmailId;
                person.CityId = c.CityId;
                person.DOB = c.DOB;
                person.Gender = c.Gender;
                person.Active = c.Active;
                _context.SaveChanges();
                return true;
            }
            return false;

        }

    }
}
