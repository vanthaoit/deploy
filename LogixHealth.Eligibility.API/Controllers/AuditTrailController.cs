using LogixHealth.Eligibility.BusinessFunctions.Interfaces;
using LogixHealth.Eligibility.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.API.Controllers
{
    public class AuditTrailController : ApiController
    {
        private IAuditTrailService _auditTrailService;

        public AuditTrailController(IAuditTrailService auditTrailService)
        {
            _auditTrailService = auditTrailService;
        }

        [HttpGet("sp_GetIEnumerableAuditTrailById/{Id}")]
        public async Task<IActionResult> GetAsync(string Id)
        {
            return new OkObjectResult(null);
        }

        // Delete: api/delete
        [HttpPut("delete")]
        public IActionResult Delete([FromBody] AuditTrailViewModel auditTrailViewModel)
        {
            var response = _auditTrailService.Delete(auditTrailViewModel);
            _auditTrailService.Save();
            return new OkObjectResult(response);
        }
    }
}