using LogixHealth.Eligibility.Models.ViewModels;
using System.Collections.Generic;

namespace LogixHealth.Eligibility.BusinessFunctions.Interfaces
{
    public interface IAuditTrailService
    {
        List<AuditTrailViewModel> GetAll(string keyword);

        AuditTrailViewModel GetById(int id);

        IEnumerable<AuditTrailViewModel> GetAllByField(string field);

        AuditTrailViewModel Add(AuditTrailViewModel request);

        void Update(AuditTrailViewModel request);

        AuditTrailViewModel Delete(AuditTrailViewModel request);

        bool DeleteMulti(IEnumerable<AuditTrailViewModel> request);

        void Save();
    }
}