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
    public class AuditTrailService : IAuditTrailService
    {
       
        
        private IAuditTrailRepository _auditRepository;

        public AuditTrailService(IAuditTrailRepository auditRepository)
        {
            
            
            _auditRepository = auditRepository;
        }

        public AuditTrailViewModel Add(AuditTrailViewModel request)
        {
            var dataMap = Mapper.Map<AuditTrailViewModel, AuditTrails>(request);
            var response = _auditRepository.Add(dataMap);
            var responseData = Mapper.Map<AuditTrails, AuditTrailViewModel>(response);
            return responseData;
        }

        public AuditTrailViewModel Delete(AuditTrailViewModel request)
        {
            var dataMap = Mapper.Map<AuditTrailViewModel, AuditTrails>(request);
            var response = _auditRepository.Remove(dataMap);
            var responseData = Mapper.Map<AuditTrails, AuditTrailViewModel>(response);
            return responseData;
        }

        public bool DeleteMulti(IEnumerable<AuditTrailViewModel> request)
        {
            var dataMap = Mapper.Map<IEnumerable<AuditTrailViewModel>, IEnumerable<AuditTrails>>(request);
            try
            {
                //foreach(var item in dataMap)
                //{
                //    _auditTrailRepository.Remove(item);
                //}
                _auditRepository.RemoveMultiple(dataMap);
            }
            catch (Exception e)
            {
                Console.WriteLine("error delete multi "+e);
                return false;
            }
            
            return true;
        }

        public List<AuditTrailViewModel> GetAll(string keyword)
        {
            var response = _auditRepository
                .FindAll().Where(x=>x.Field.Equals(keyword))
                .OrderByDescending(x => x.ChangeDateTime)
                .ProjectTo<AuditTrailViewModel>()
                .ToList();
            return response;
        }

        public IEnumerable<AuditTrailViewModel> GetAllByField(string field)
        {
            var response = _auditRepository
                 .FindAll().Where(x => x.Field.Equals(field))
                 .OrderByDescending(x => x.ChangeDateTime)
                 .ProjectTo<AuditTrailViewModel>()
                 .ToList();
            return response;
        }

        public AuditTrailViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            
        }

        public void Update(AuditTrailViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}