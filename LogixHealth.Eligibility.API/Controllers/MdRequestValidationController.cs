using LogixHealth.Eligibility.BusinessFunctions.Interfaces;
using LogixHealth.Eligibility.DataAccess;
using LogixHealth.Eligibility.DataAccess.Common;
using LogixHealth.Eligibility.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.API.Controllers
{
    public class MdRequestValidationController : ApiController
    {
        private IAuditTrailService _auditTrailService;
        private IMdRequestValidationService _mdRequestValidationService;
        

        public MdRequestValidationController(IMdRequestValidationService mdRequestValidationService,
            IAuditTrailService auditTrailService)
        {
            _mdRequestValidationService = mdRequestValidationService;
            _auditTrailService = auditTrailService;
            
        }

        // GET: api/MdRequestValidation/getall
        [HttpGet("getAll")]
        public IActionResult Get()
        {
            var allRequestValidations = _mdRequestValidationService.GetAll();

            return new OkObjectResult(allRequestValidations);
        }

        // GET: api/MdRequestValidation/getAuditTrailList
        [HttpGet("getAuditTrailsByValidation/{validationCode}")]
        public IActionResult Get(string validationCode)
        {
            if (!string.IsNullOrEmpty(validationCode))
            {
                var response = _auditTrailService.GetAll(validationCode);
                return new OkObjectResult(response);
            }

            return new OkObjectResult("");
        }

        // GET: api/MdRequestValidation/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var responseSingleValidation = _mdRequestValidationService.GetById(id);

            return new OkObjectResult(responseSingleValidation);
        }

        [HttpGet("sp_GetValidationCodeKeys")]
        public async Task<IActionResult> SP_GetListValidationCode()
        {
            try
            {
                var response = _mdRequestValidationService.GetValidationCodeList();

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(null);
            }
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody]MdRequestValidationViewModel mdRequestValidation)
        {
            var request = mdRequestValidation;

            var result = _mdRequestValidationService.Add(request);
            if (result != null)
            {
                var requestAuditTrail = MapAuditTrailWithValidation(mdRequestValidation,null, UserActionConstants.ADD_VALIDATION_CODE, result.Id);

                var resultAuditTrail = _auditTrailService.Add(requestAuditTrail);
            }
            return new OkObjectResult(result);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            var result = "";
        }

        // PUT: api/MdRequestValidation/put
        [HttpPost("put")]
        public IActionResult Put([FromBody] MdRequestValidationViewModel mdRequestValidation)
        {
            try
            {
                var id = mdRequestValidation.Id;
                var oldAuditTrail = _mdRequestValidationService.GetById(id);
                var requestAuditTrail = MapAuditTrailWithValidation(mdRequestValidation,oldAuditTrail, UserActionConstants.EDIT_VALIDATION_CODE);

                var resultAuditTrail = _auditTrailService.Add(requestAuditTrail);
                _mdRequestValidationService.Update(mdRequestValidation);
                return new OkObjectResult(mdRequestValidation);
            }
            catch (Exception e)
            {
                Console.WriteLine("error put vali " + e);
                return new OkObjectResult(null);
            }
        }

        // DEL: api/MdRequestValidation/delete
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] MdRequestValidationViewModel mdRequestValidation)
        {
            try
            {
                var response = _mdRequestValidationService.Delete(mdRequestValidation);
                if(response == null) return new OkObjectResult(null);
                
                var getAuditTrailList = _auditTrailService.GetAllByField(response.ValidationCode);

                bool isDeletedMulti = _auditTrailService.DeleteMulti(getAuditTrailList);

                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error delete request validation ", e);
                return new OkObjectResult(null);
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private AuditTrailViewModel MapAuditTrailWithValidation(MdRequestValidationViewModel mdRequestValidation,MdRequestValidationViewModel oldValidation, string userAction, int? id = null)
        {
            StringBuilder oldValue = new StringBuilder();
            StringBuilder newValue = new StringBuilder();
            if (oldValidation != null)
                oldValue.Append("Description: " + oldValidation.Description + "  |  selfPay: " + oldValidation.IsSelfPay + "  |  isInsurance: " + oldValidation.IsInsurance);
            else
                oldValue.Append(" ");
            newValue.Append("Description: " + mdRequestValidation.Description + "  |  selfPay: " + mdRequestValidation.IsSelfPay + "  |  isInsurance: " + mdRequestValidation.IsInsurance);
            int IdValidation = mdRequestValidation.Id;
            if (id.HasValue)
            {
                IdValidation = id.Value;
            }
            var requestAuditTrail = new AuditTrailViewModel()
            {
                TableName = TableNameConstants.VALIDATION_CODE,
                RecordId = IdValidation,
                Field = mdRequestValidation.ValidationCode,
                OldValue = oldValue.ToString(),
                NewValue = newValue.ToString(),
                ChangeDateTime = DateTime.Now,
                UserAction = userAction,
                UserId = new Guid(UserActionConstants.USER_ID)
            };
            return requestAuditTrail;
        }
    }
}