using CRUD_Operations_Core.Data;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations_Core.Models.DB_Operations
{
    public class CityRepository
    {
        private readonly AppDbContext _context;
      //  public AppDbContext _context = new AppDbContext();
        public CityRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<City> GetCities()
        {
            List<City> cityList;
           return cityList = _context.Cities.ToList();
        }

       



        public List<City> GetCities(string SortColumn , string Iconclass , int PageNo )
        {
            #region Sort Logic

            List<City> city = null;

            if (SortColumn == "Id")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    city = _context.Cities.OrderBy(temp => temp.Id).Include(obj=>obj.Country).ToList(); //.include method here used for eager loading to get data from
                                                                                                                                                                     // another table which have foreign key relation ship
                }
                else
                {
                    city = _context.Cities.OrderByDescending(temp => temp.Id).Include(obj => obj.Country).ToList();
                }
            }

            if (SortColumn == "Name")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    city = _context.Cities.OrderBy(temp => temp.Name).Include(obj => obj.Country).ToList();
                }
                else
                {
                    city = _context.Cities.OrderByDescending(temp => temp.Name).Include(obj => obj.Country).ToList();
                }
            }

            if (SortColumn == "Code")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    city = _context.Cities.OrderBy(temp => temp.Code).Include(obj => obj.Country).ToList();
                }
                else
                {
                    city = _context.Cities.OrderByDescending(temp => temp.Code).Include(obj => obj.Country).ToList();
                }
            }

            if (SortColumn == "Country")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    city = _context.Cities.OrderBy(temp => temp.Country.Name).Include(obj => obj.Country).ToList();
                }
                else
                {
                    city = _context.Cities.OrderByDescending(temp => temp.Country.Name).Include(obj => obj.Country).ToList();
                }
            }

            return city;

            #endregion
           
        }

        public List<City> SearchCity(string search="")
        {
            List<City> city;
            return city = _context.Cities.Where(temp => temp.Name.Contains(search)||temp.Code.Contains(search)).ToList();
        }

        public bool CreateCity(City c)
        {
                _context.Cities.Add(c);  
                _context.SaveChanges();
                return true;
         }

        public List<City> GetCityDetails(int id)
        {
            List<City> cityList;
            cityList = _context.Cities.Where(temp => temp.Id == id).ToList();
            return cityList;
        }

        public City GetCityDetailsObj(int id)
        {
            City city;
            city = _context.Cities.Where(temp => temp.Id == id).FirstOrDefault();
            return city;
        }

        public bool DeleteCity(int id)
        {
            City cityList;
            cityList = _context.Cities.Where(temp => temp.Id == id).FirstOrDefault();
            if(cityList != null)
            {
                _context.Remove(cityList);
                _context.SaveChanges();
                return true;
            }
             return false;
        }

        public bool EditCity(City c)
        {
            if (c != null)
            {
                City city = _context.Cities.Where(temp => temp.Id == c.Id).FirstOrDefault();
                city.Id = c.Id;
                city.Name = c.Name;
                city.Code = c.Code;
                city.CountryId = c.CountryId;
                _context.SaveChanges();
                return true;
            }
            return false;

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
                Text = "----Select Country----"
            };

            lstCountries.Insert(0, defItem);

            return lstCountries;
        }
    }
}
