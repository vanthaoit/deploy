using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using System.Collections.Generic;

namespace LogixHealth.Eligibility.BusinessFunctions.Interfaces
{
    public interface IMdRequestValidationService
    {
        List<MdRequestValidationViewModel> GetAll();

        List<MdRequestValidationViewModel> GetAll(string keyword);

        IEnumerable<ValidationList> GetValidationCodeList();

        MdRequestValidationViewModel GetById(int id);

        MdRequestValidationViewModel Add(MdRequestValidationViewModel requestValidation);

        void Update(MdRequestValidationViewModel requestValidation);

        MdRequestValidationViewModel Delete(MdRequestValidationViewModel requestValidation);

        void Save();
    }
}