using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using LogixHealth.Eligibility.Core.Https;
using Microsoft.AspNetCore.Mvc;

namespace LogixHealth.Eligibility.Controllers
{
    public class AuditTrailController : Controller
    {
        private IHttpProviderService<AuditTrailViewModel> _auditTrailHttp;
        public AuditTrailController(IHttpProviderService<AuditTrailViewModel> auditTrailHttp)
        {
            _auditTrailHttp = auditTrailHttp;
        }
        [HttpGet]
        public async Task<PartialViewResult> GetDetails(string id, string description)
        {

            var getAuditTrailList = await _auditTrailHttp.GetIEnumerableByValueAsync("MdRequestValidation/getAuditTrailsByValidation/"+id);
            var test = getAuditTrailList.Where(x=>x.NewValue.Contains("Thao Tran") 
            || x.OldValue.Contains("Thao Tran")
            || x.Field.Contains("Thao Tran")
            || x.UserAction.Contains("Thao Tran"));
            ViewBag.descriptionAuditTrail = description;

            return PartialView("~/Views/AuditTrail/_AuditTrails.cshtml", getAuditTrailList);
        }

    }
}