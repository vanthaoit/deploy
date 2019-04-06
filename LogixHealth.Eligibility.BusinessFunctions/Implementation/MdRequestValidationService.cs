using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogixHealth.Eligibility.BusinessFunctions.Interfaces;
using LogixHealth.Eligibility.DataAccess.Infrastructure.Implements;
using LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogixHealth.Eligibility.BusinessFunctions.Implementation
{
    public class MdRequestValidationService : IMdRequestValidationService
    {
        
        private IMdRequestValidationRepository _validationCodeRepository;
        private IValidationListRepository _validationListRepository;

        public MdRequestValidationService(IMdRequestValidationRepository validationCodeRepository,
            IValidationListRepository validationListRepository)
        {
            
            _validationCodeRepository = validationCodeRepository;
            _validationListRepository = validationListRepository;
        }

        public MdRequestValidationViewModel Add(MdRequestValidationViewModel requestValidation)
        {
            var dataMap = Mapper.Map<MdRequestValidationViewModel, MdRequestValidation>(requestValidation);
            var response = _validationCodeRepository.Add(dataMap);
            var responseValidation = Mapper.Map<MdRequestValidation, MdRequestValidationViewModel>(response);
            return responseValidation;
        }

        public MdRequestValidationViewModel Delete(MdRequestValidationViewModel requestValidation)
        {
            var dataMap = Mapper.Map<MdRequestValidationViewModel, MdRequestValidation>(requestValidation);
            var response = _validationCodeRepository.Remove(dataMap);
            var responseValidation = Mapper.Map<MdRequestValidation, MdRequestValidationViewModel>(response);
            return responseValidation;
        }

        public List<MdRequestValidationViewModel> GetAll()
        {
            var response = _validationCodeRepository
                .FindAll()
                .OrderByDescending(x => x.CreatedDateTime)
                .ThenBy(x => x.ModifiedDateTime)
                .ProjectTo<MdRequestValidationViewModel>()
                .ToList();
            return response;
        }

        public List<MdRequestValidationViewModel> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public MdRequestValidationViewModel GetById(int id)
        {
            var responseValidation = _validationCodeRepository.FindById(id);
            var responseMap = Mapper.Map<MdRequestValidationViewModel>(responseValidation);
            return responseMap;
        }

        public void Save()
        {
            
        }

        public void Update(MdRequestValidationViewModel requestValidation)
        {
            var validationMap = Mapper.Map<MdRequestValidation>(requestValidation);
            _validationCodeRepository.Update(validationMap);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<ValidationList> GetValidationCodeList()
        {
            var responseListValidation = _validationListRepository.GetAll();
            return responseListValidation;
        }
    }
}