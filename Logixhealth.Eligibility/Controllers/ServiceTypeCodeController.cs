using LogixHealth.Eligibility.Models.Entities;
using LogixHealth.Eligibility.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogixHealth.Eligibility.Core.Https;

namespace LogixHealth.Eligibility.Controllers
{
    public class ServiceTypeCodeController : Controller
    {
        private IHttpProviderService<MdRequestValidationViewModel> _setupHttp;
        private IHttpProviderService<ValidationList> _valitionListHttp;

        [HttpGet]
        public async Task<PartialViewResult> GetServiceDetails(int id, bool? isDelete = false)
        {
            if (isDelete.Value)
                return PartialView("~/Views/ServiceTypeCode/_DeleteServiceTypeCode.cshtml");
            else
                return PartialView("~/Views/ServiceTypeCode/_EditServiceTypeCode.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> PostServiceCodes(MdRequestValidationViewModel data)
        {
            if (!ModelState.IsValid) return View(data);
            data.CreatedDateTime = DateTime.Now;
            data.ModifiedDateTime = DateTime.Now;
            data.ModifiedBy = new Guid("BF189BE2-D457-4424-A27E-61EA87395439");
            var response = await _setupHttp.PostAsync(ControllerAPIConstants.SERVICE_TYPE_CODE + "/post", data);
            if (response.Id == 0)
            {
                TempData[NotificationConstants.MESSAGE_ERROR] = "The service type code has been failed.";
                return RedirectToAction("ValidationCodes", "ValidationCode");
            }
            TempData[NotificationConstants.MESSAGE] = "The Service Type Code " + response.ValidationCode + " has been created.";
            TempData[NotificationConstants.RESPONSEDATA] = response.Id;
            return RedirectToAction("ValidationCodes", "ValidationCode");
        }

        [HttpGet]
        public async Task<ViewResult> ServiceTypeCodes()
        {
            //ViewData[CrossActionKeys.SubTabs] = SubMenuConstants.ServiceTypeCodes;
            return View();
        }
    }
}