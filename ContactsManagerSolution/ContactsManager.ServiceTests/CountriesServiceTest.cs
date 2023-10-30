//using ServiceContracts;
//using ServiceContracts.DTO;
//using Services;
//using Entities;
//using Microsoft.EntityFrameworkCore;
//using EntityFrameworkCoreMock;

//namespace CRUDTest
//{
//    public class CountriesServiceTest
//    {
//        private readonly ICountriesGetterService _countriesService;

//        public CountriesServiceTest()
//        {
//            var countriesInitialData = new List<Country>() { };
//            DbContextMock<ApplicationDBContext> dbContextMock = new DbContextMock<ApplicationDBContext>(
//                new DbContextOptionsBuilder<ApplicationDBContext>().Options);
//            ApplicationDBContext dbContext = dbContextMock.Object;
//            _countriesService = new CountriesService(null);
//            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
//        }


//        #region AddCountry
//        [Fact]
//        public async Task AddCountry_NullCountry()
//        {
//            //Arrange
//            CountryAddRequest? countryAddRequest = null;

//            //Assert
//            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
//            {
//                //Act
//                await _countriesService.AddCountry(countryAddRequest);
//            });
//        }

//        [Fact]
//        public async Task AddCountry_CountryNameISNull()
//        {
//            //Arrange
//            CountryAddRequest? countryAddRequest = new CountryAddRequest()
//            {
//                CountryName = null
//            };

//            //Assert
//            await Assert.ThrowsAsync<ArgumentException>(async () =>
//            {
//                //Act
//                await _countriesService.AddCountry(countryAddRequest);
//            });
//        }

//        [Fact]
//        public async Task AddCountry_DuplicateCountryName()
//        {
//            //Arrange
//            CountryAddRequest countryAddRequest = new CountryAddRequest()
//            {
//                CountryName = "USA"
//            };
//            CountryAddRequest countryAddRequest2 = new CountryAddRequest()
//            {
//                CountryName = "USA"
//            };
//            //Assert
//            await Assert.ThrowsAsync<ArgumentException>(async () =>
//            {
//                //Act
//                await _countriesService.AddCountry(countryAddRequest);
//                await _countriesService.AddCountry(countryAddRequest);
//            });
//        }
//        [Fact]
//        public async Task AddCountry_ProperCountryDetails()
//        {
//            //Arrange
//            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
//            {
//                CountryName = "Jaopan"
//            };
//            //Act
//            CountryResponse respones = await _countriesService.AddCountry(countryAddRequest1);
//            //Assert
//            Assert.True(respones.CountryID != Guid.Empty);
//        }
//        #endregion

//        #region GetAllCountries

//        [Fact]
//        public async Task GetAllCountries_EmptyList()
//        {
//            //Arrange - nothing to arrange

//            //Act
//            List<CountryResponse> actualCountryResponseList = await _countriesService.GetAllCountries();

//            //Assert
//            Assert.Empty(actualCountryResponseList);
//        }

//        /*[Fact]
//        public void GetAllCountries_AddFewCountries()
//        {
//            //Arrange

//            //Act

//            //Assert
//        }*/


//        #endregion

//        #region GetCountryByCountryID


//        //if we supply null country id it should return null as countryResponse
//        [Fact]
//        public async Task GetCountryByCountryID_NullCountryID()
//        {
//            //Arrange
//            Guid? countryID = null;
//            //Act
//            CountryResponse? countryResponse = await _countriesService.GetCountryByCountryID(countryID);
//            //Assert
//            Assert.Null(countryResponse);
//        }

//        [Fact]
//        //if we supplya a valid country id, it should return the matching country detail

//        public async Task GetCountryByCountryID_ValidCountryID()
//        {
//            //Arrange
//            CountryResponse countryResponseFromAdd = await _countriesService.AddCountry(new CountryAddRequest()
//            {
//                CountryName = "Japan"
//            });
//            Guid countryID = countryResponseFromAdd.CountryID;
//            //Act
//            CountryResponse? countryResponesFromGet = await _countriesService.GetCountryByCountryID(countryID);
//            //Assert
//            Assert.Equal(countryResponseFromAdd, countryResponesFromGet);
//        }
//        #endregion

//    }
//}