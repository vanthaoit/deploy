using LogixHealth.Eligibility.DataAccess.Common;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using System.Collections.Generic;

namespace LogixHealth.Eligibility.DataAccess.Infrastructure.Implements
{
    public interface IValidationListRepository
    {
        IEnumerable<ValidationList> GetAll();
    }

    public class ValidationListRepository : IValidationListRepository
    {
        private IRepositoryFactory _repositoryFactory;
        private IRepositoryBase<ValidationList> _registerValidationList;

        public ValidationListRepository(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _registerValidationList = _repositoryFactory.Create<ValidationList>();
        }

        public IEnumerable<ValidationList> GetAll()
        {
            //IRepositoryBase<ValidationList> validationListBase = _repositoryFactory.Create<ValidationList>();

            var result = _registerValidationList.Query().GoStoreProcedure(SchemaDatabaseCosntants.SCHEMA_DATABASE + ".sp_GetValidationCodeKeys");

            return result;
        }
    }
}