using CRUD_Operations_Core.Data;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Core.Models;

namespace CRUD_Operations_Core.Models.DB_Operations
{
    public class CountryRepository
    {
        private readonly AppDbContext _context;
      //  public AppDbContext _context = new AppDbContext();
        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Country> GetCountries()
        {
            List<Country> countryListst;
           return  countryListst = _context.Countries.ToList();
        }

        public List<Country> GetCountries(string SortColumn , string Iconclass , int PageNo )
        {
            #region Sort Logic

            List<Country> country = null;

            if (SortColumn == "Id")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    country = _context.Countries.OrderBy(temp => temp.Id).ToList();
                }
                else
                {
                    country = _context.Countries.OrderByDescending(temp => temp.Id).ToList();
                }
            }

            if (SortColumn == "Name")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    country = _context.Countries.OrderBy(temp => temp.Name).ToList();
                }
                else
                {
                    country = _context.Countries.OrderByDescending(temp => temp.Name).ToList();
                }
            }

            if (SortColumn == "Code")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    country = _context.Countries.OrderBy(temp => temp.Code).ToList();
                }
                else
                {
                    country = _context.Countries.OrderByDescending(temp => temp.Code).ToList();
                }
            }

            if (SortColumn == "Currency")
            {
                if (Iconclass == "fa-sort-asc")
                {
                    country = _context.Countries.OrderBy(temp => temp.Currency).ToList();
                }
                else
                {
                    country = _context.Countries.OrderByDescending(temp => temp.Currency).ToList();
                }
            }

            return country;

            #endregion
           
        }

        public List<Country> SearchCountry(string search="")
        {
            List<Country> country;
            return country = _context.Countries.Where(temp => temp.Name.Contains(search)).ToList();
        }

        public bool CreateCountry(Country c)
        {
                _context.Countries.Add(c);  
                _context.SaveChanges();
                return true;
         }

        public List<Country> GetCountryDetails(int id)
        {
            List<Country> countryList;
            countryList = _context.Countries.Where(temp => temp.Id == id).ToList();
            return countryList;
        }

        public Country GetCountryDetailsObj(int id)
        {
            Country country;
            country = _context.Countries.Where(temp => temp.Id == id).FirstOrDefault();
            return country;
        }

        public bool DeleteCountry(int id)
        {
            Country countryList;
            countryList = _context.Countries.Where(temp => temp.Id == id).FirstOrDefault();
            if(countryList != null)
            {
                _context.Remove(countryList);
                _context.SaveChanges();
                return true;
            }
             return false;
        }

        public bool EditCountry(Country c)
        {
            if (c != null)
            {
                Country country = _context.Countries.Where(temp => temp.Id == c.Id).FirstOrDefault();
                country.Id = c.Id;
                country.Name = c.Name;
                country.Code = c.Code;
                country.Currency = c.Currency;
                _context.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
