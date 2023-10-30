using CRUDLearning.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;

namespace CRUDLearning.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsGetterService _personsGetterService;
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ICountriesAdderService _countriesAdderService;
        private readonly ILogger<PersonsController> _logger;

        //constructor 
        public PersonsController(IPersonsAdderService personsAdderService,
            IPersonsGetterService personsGetterService,ICountriesGetterService countriesGetterService,ICountriesAdderService countriesAdderService,
            ILogger<PersonsController> logger)
        {
            _personsAdderService = personsAdderService;
            _personsGetterService = personsGetterService;
            _countriesGetterService = countriesGetterService;
            _countriesAdderService = countriesAdderService;
            _logger = logger;
        }






        [Route("[action]")]
        [Route("/")]
        [TypeFilter(typeof(PersonsListActionFilter))]
        public async Task<IActionResult> Index(string searchBy,string? searchString)
        {
            _logger.LogInformation("Index action method of person controller");

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonAddRequest.PersonName),"Person Name" },
                {nameof(PersonAddRequest.Email),"Email" },
                {nameof(PersonAddRequest.Address),"Address" },
                {nameof(PersonAddRequest.Gender),"Gender" },
                {nameof(PersonAddRequest.CountryID),"Country ID" },
            };
            List<PersonResponse> persons =await _personsGetterService.GetFilteredPersons(searchBy,searchString);
            ViewBag.CurrentSearchBy = searchBy; ViewBag.CurrentSearchString = searchString;
            return View(persons); //Views/Persons/Index.cshtml
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString()});
            return View();
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(temp=>temp.Errors).Select(e=>e.ErrorMessage).ToList();
                return View();
            }
            //call the service method
            PersonResponse personResponse = await _personsAdderService.AddPerson(personAddRequest);
            //navigate to Index() action method ( it makes another get request to "persons/index"
            return RedirectToAction("Index","Persons");
        }

        [HttpGet]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Edit(Guid personID)  //e.g. /persons/edit/1
        {
            PersonResponse? personResponse = await _personsGetterService.GetPersonByPersonID(personID);
            if(personResponse == null)
            {
                return RedirectToAction("Index");
            }
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
            return View(personUpdateRequest);
         }


    }
}
