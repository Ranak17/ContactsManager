namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsDeleterService
    {
        /// <summary>
        /// Deletes a pesrson based onthe given person id
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>Return true if deletion is successfull </returns>
        Task<bool> DeletePerson(Guid? personID);
    }
}
