﻿using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsAdderService
    {
        /// <summary>
        /// Adds a new Person into the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns> Returns the same person details, along with newly generated PersonID</returns>
        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);
    }
}
