using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RazorPagesAJAXNamedHandlersDemo.Repositories;

namespace RazorPagesAJAXNamedHandlersDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILocationRepository _locationRepo;

        public IndexModel(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        public List<SelectListItem> Continents { get; set; }
        public string SelectedContinent { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public string SelectedCountry { get; set; }

        public void OnGet()
        {
            Continents = _locationRepo.GetContinents()
                                      .Select(x=>new SelectListItem() { Value = x, Text = x })
                                      .ToList();
            SelectedContinent = Continents.First().Value;

            Countries = _locationRepo.GetCountries(SelectedContinent)
                                     .Select(x => new SelectListItem() { Value = x, Text = x })
                                     .ToList();
            SelectedCountry = Countries.First().Value;
        }

        public JsonResult OnGetCountriesFilter(string continent)
        {
            return new JsonResult(_locationRepo.GetCountries(continent));
        }
    }
}
