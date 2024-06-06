using CRUD_Operations_Core.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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

        public List<PersonDetail> GetPersons(string SortColumn, string Iconclass, int PageNo)
        {
            #region Sort Logic

            List<PersonDetail> person = null;

            if (SortColumn != null)
            {
                
                if (Iconclass == "fa-sort-asc")
                {
                    person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "ASC").ToList();
                }
                else
                {
                    person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "DESC").ToList();
                }
            }

            //if (SortColumn == "FirstName")
            //{
            //    if (Iconclass == "fa-sort-asc")
            //    {
            //         person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "ASC").ToList();
            //    }
            //    else
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "DESC").ToList();
            //    }
            //}

            //if (SortColumn == "LastName")
            //{
            //    if (Iconclass == "fa-sort-asc")
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "ASC").ToList();
            //    }
            //    else
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "DESC").ToList();
            //    }
            //}

            //if (SortColumn == "Email")
            //{
            //    if (Iconclass == "fa-sort-asc")
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "ASC").ToList();
            //    }
            //    else
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "DESC").ToList();

            //    }
            //}
            //if (SortColumn == "City")
            //{
            //    if (Iconclass == "fa-sort-asc")
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "ASC").ToList();
            //    }
            //    else
            //    {
            //        person = _context.PersonDetails.FromSqlRaw<PersonDetail>("GetPersonDetailsWithRowCount {0} ,{1}", SortColumn, "DESC").ToList();

            //    }
            //}

            return person;

            #endregion

        }

        public List<PersonDetail> SearchPerson(string search = "")
        {
            List<PersonDetail> person;
            //return person = _context.Person.Where(temp => temp.FirstName.Contains(search) || temp.LastName.Contains(search)).Include(obj => obj.City).Include(con => con.City.Country).ToList();
            return person = _context.PersonDetails.FromSqlRaw<PersonDetail>("SearchPersonData {0}", search).ToList();
        }

        public bool CreatePerson(Person p)
        {
          
                //_context.Person.Add(p);
                //_context.SaveChanges();
                _context.Database.ExecuteSqlRaw("InsertPersonData {0},{1},{2},{3},{4},{5},{6},{7}",
                    p.FirstName,
                    p.LastName,
                    p.EmailId,
                    p.CityId,
                    p.DOB,
                    p.Gender,
                    p.Active,
                    p.PhotoUrl);
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


        public bool EditPerson(Person p)
        {
            //Person person=new Person();
            if (p != null)
            {
                // Person person = _context.Person.Where(temp => temp.Id ==c.Id).FirstOrDefault();
                //person.Id = c.Id;
                //person.FirstName = c.FirstName;
                //person.LastName = c.LastName;
                //person.EmailId = c.EmailId;
                //person.CityId = c.CityId;
                //person.DOB = c.DOB;
                //person.Gender = c.Gender;
                //person.Active = c.Active;
                //person.PhotoUrl = c.PhotoUrl;
                //_context.SaveChanges();
                _context.Database.ExecuteSqlRaw("UpdatePersonData {0},{1},{2},{3},{4},{5},{6},{7},{8}",
            
               p.Id,
               p.FirstName,
               p.LastName,
               p.EmailId,
               p.CityId,
               p.DOB,
               p.Gender,
               p.Active,
               p.PhotoUrl);
                return true;
            }
            return false;

        }

        public enum Operation
        {
            Insert = 1 ,
           Update = 2
        }

    }
}
