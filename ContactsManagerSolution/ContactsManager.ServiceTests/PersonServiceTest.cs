using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using Services;
using ContactsManager.Core.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using EntityFrameworkCoreMock;
using ContactsManager.Core.Domain.RepositoryContracts;
using Moq;
using System.Linq.Expressions;

namespace CRUDTest
{
    public class PersonServiceTest
    {
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly IPersonsDeleterService _personsDeleterService;

        private readonly ICountriesGetterService _countryService;
        private readonly IPersonRepository _personRepository;
        private readonly Mock<IPersonRepository> _personRepositoryMock;

        public PersonServiceTest()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personRepository = _personRepositoryMock.Object;
            /*  var countriesInitialData = new List<Country>() { };
          var personsInitialData = new List<Person>() { };

          //Create Mock for DBContext
      DbContextMock<ApplicationDBContext> dbContextMock = new DbContextMock<ApplicationDBContext>(
              new DbContextOptionsBuilder<ApplicationDBContext>().Options);

          //Access Mock DBContext object
          ApplicationDBContext dbContext = dbContextMock.Object;

          //Create Mock for DBSets
          dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
          dbContextMock.CreateDbSetMock(temp => temp.Persons, personsInitialData);*/
            _personsAdderService = new PersonsAdderService(_personRepository);
            _personsGetterService = new PersonsGetterService(_personRepository);
            _personsUpdaterService = new PersonsUpdaterService(_personRepository);
            _personsDeleterService = new PersonsDeleterService(_personRepository);
        }

        #region AddPerson
        [Fact]
        public async Task AddPerson_NullPerson_ToBeArgumentNullException()
        {


            //Arrange
            PersonAddRequest? personAddRequest = null;
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _personsAdderService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public async Task AddPerson_PersonNameISNull_ToBeArgumentException()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _personsAdderService.AddPerson(personAddRequest);
            });
        }
        //we can add multiple test case for null email, null phonenumber etc.

        [Fact]
        public async Task AddPerson_FullPersonDetails_ToBeSuccessfull()
        {
            //Arrange 
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                CountryID = Guid.NewGuid(),
                Gender = GenderEnum.Male,
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            };


            Person person = personAddRequest.ToPerson();
            //If we supply any argument value to the AddPerson method, it should return the same return value - Mocking code
            _personRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>()))
                .ReturnsAsync(person);


            PersonResponse personResponseExpected = person.ToPersonResponse();
            //Act
            PersonResponse personResponseFromAdd = await _personsAdderService.AddPerson(personAddRequest);
            personResponseExpected.PersonID = personResponseFromAdd.PersonID;

            //Assert
            Assert.True(personResponseFromAdd.PersonID != Guid.Empty);
            Assert.Equal(personResponseExpected, personResponseFromAdd);
        }

        #endregion

        #region GetPersonByPersonID

        //if we supply null as personid, it should return as null as PersonResponse
        [Fact]
        public async Task GetPersonByPersonID_NullPersonID_ToBeNull()
        {
            //Arrange
            Guid? personID = null;

            //Act 
            PersonResponse? personResponseFromGet = await _personsGetterService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(personResponseFromGet);
        }

        //if we supply  a valid person id, it should return the valid person details as PersonResponse Object
        [Fact]

        public async Task GetPersonByPersonID_ValidPersonID_ToBeSuccessFull()
        {
            //Arrange
            Person person = new Person()
            {
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                //CountryID = countryResponse.CountryID,
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            };
            _personRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>())).ReturnsAsync(person);


            PersonResponse personResponseExpected = person.ToPersonResponse();
            //Act
            PersonResponse? personResponseFromGet = await _personsGetterService.GetPersonByPersonID(person.PersonID);

            //Assert
            Assert.Equal(personResponseExpected, personResponseFromGet);


        }


        #endregion

        #region GetAllPersons
        //the GetAllPerson() should return an empty list by default
        [Fact]
        public async Task GetAllPersons_EmptyList()
        {
            //Arrange
            List<Person> persons = new List<Person>();
            _personRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);

            //Act
            List<PersonResponse> personResponseFromGet = await _personsGetterService.GetAllPersons();

            //Assert
            Assert.Empty(personResponseFromGet);
        }

        //
        [Fact]
        public async Task GetAllPersons_AddFewPerson()
        {
            //Arrange
            List<Person> persons = new List<Person>()
            {
            new Person(){
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            },
            new Person(){
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            }
            };
            List<PersonResponse> personResponseListExpected = persons.Select(temp => temp.ToPersonResponse()).ToList();

            //Mocking the GetAllPersons() Method
            _personRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);
            //Act
            List<PersonResponse> personResponsesListFromGet = await _personsGetterService.GetAllPersons();

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListExpected)
            {
                Assert.Contains(personResponseFromAdd, personResponsesListFromGet);
            }

        }
        #endregion


        #region GetFilteredPersons

        //If the search text is empty and search by is "Person Name", it should return all person
        [Fact]
        public async Task GetFilteredPerson_EmptySearchText()
        {
            //Arrange
            List<Person> persons = new List<Person>()
            {
                new Person(){
                    PersonName = "karan",
                    Email = "karan@gmail.com",
                    Address = "dummy Address",
                    Gender = GenderEnum.Male.ToString(),
                    DateOfBirth = DateTime.Parse("2000-01-11"),
                    ReceiveNewsLetters = true,
                },
                new Person(){
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            }
            };
            List<PersonResponse> personResponseListExpected = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personRepositoryMock.Setup(temp => temp
            .GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>()))
            .ReturnsAsync(persons);
            //Act
            List<PersonResponse> personResponsesListFromSearch = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), "");

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListExpected)
            {
                Assert.Contains(personResponseFromAdd, personResponsesListFromSearch);
            }
        }

        //First we will add few persons, and then we will search based on person name with some search string. Ti should return the matching persons
        [Fact]
        public async Task GetFilteredPerson_SearchByPersonName()
        {
            //Arrange
            List<Person> persons = new List<Person>()
            {
                new Person(){
                    PersonName = "taran",
                    Email = "karan@gmail.com",
                    Address = "dummy Address",
                    Gender = GenderEnum.Male.ToString(),
                    DateOfBirth = DateTime.Parse("2000-01-11"),
                    ReceiveNewsLetters = true,
                },
                new Person(){
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            }
            };
            List<PersonResponse> personResponseListExpected = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personRepositoryMock.Setup(temp => temp
            .GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>()))
            .ReturnsAsync(persons);
            //Act
            List<PersonResponse> personResponsesListFromSearch = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), "sa");

            //Assert
            foreach (PersonResponse personResponseFromAdd in personResponseListExpected)
            {
                Assert.Contains(personResponseFromAdd, personResponsesListFromSearch);
            }
        }





        #endregion


        #region UpdatePerson
        [Fact]
        public async Task UpdatePerson_NullPerson_ToBeArgumentNullException()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = null;
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });
        }
        [Fact]
        public async Task UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid()
            };
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public async Task UpdatePerson_PersonNameIsNull_ToBeArgumentException()
        {
            //Arrange

            Person person = new Person()
            {
                PersonName = null,
                Email = "karan@gmail.com",
                Gender = GenderEnum.Male.ToString(),

            };
            PersonResponse personResponseFromAdd = person.ToPersonResponse();
            PersonUpdateRequest personUpdateRequest = personResponseFromAdd.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            });
        }

        [Fact]
        public async Task UpdatePerson_PersonFullDetailsUpdation_ToBeSuccessFull()
        {
            //Arrange
            Person person = new Person()
            {
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                CountryID = Guid.NewGuid(),
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            };
            _personRepositoryMock.Setup(temp => temp.UpdatePerson(It.IsAny<Person>())).ReturnsAsync(person);
            _personRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>())).ReturnsAsync(person);

            PersonResponse personResponseExpected = person.ToPersonResponse();
            PersonUpdateRequest personUpdateRequest = personResponseExpected.ToPersonUpdateRequest();

            //Act
            PersonResponse personResponseFromUpdate = await _personsUpdaterService.UpdatePerson(personUpdateRequest);

            //Assert
            Assert.Equal(personResponseExpected, personResponseExpected);



        }
        #endregion

        #region DeletePerson

        //if you supply a invalid personid it should return true
        [Fact]
        public async Task DeletePerson_ValidPersonID_ToBeSuccessFull()
        {
            //Arrange
            Person person = new Person()
            {
                PersonName = "karan",
                Email = "karan@gmail.com",
                Address = "dummy Address",
                CountryID = Guid.NewGuid(),
                Gender = GenderEnum.Male.ToString(),
                DateOfBirth = DateTime.Parse("2000-01-11"),
                ReceiveNewsLetters = true,
            };
            PersonResponse personResponseExpected = person.ToPersonResponse();


            _personRepositoryMock.Setup(temp => temp.DeletePersonByPersonID(It.IsAny<Guid>())).ReturnsAsync(true);

            _personRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>())).ReturnsAsync(person);
            //Act
            bool isDeleted = await _personsDeleterService.DeletePerson(person.PersonID);
            //Assert
            Assert.True(isDeleted);
        }

        //if you supply a invalid personid it should return false
        [Fact]
        public async Task DeletePerson_InValidPersonID()
        {
            //Act
            bool isDeleted = await _personsDeleterService.DeletePerson(Guid.NewGuid());
            //Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}