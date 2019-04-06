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
    public class ValidationCodeController : Controller
    {
        private IHttpProviderService<MdRequestValidationViewModel> _setupHttp;
        private IHttpProviderService<ValidationList> _valitionListHttp;

        public ValidationCodeController(IHttpProviderService<MdRequestValidationViewModel> setupHttp,
            IHttpProviderService<ValidationList> validationList)
        {
            _setupHttp = setupHttp;
            _valitionListHttp = validationList;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> ValidationCodes()
        {
            var response = await _setupHttp.GetAsync("MdRequestValidation/getall");
            var message = TempData[NotificationConstants.MESSAGE];
            var messageError = TempData[NotificationConstants.MESSAGE_ERROR];
            var responseData = TempData[NotificationConstants.RESPONSEDATA];
            ViewBag.Message = message;
            ViewBag.MessageError = messageError;
            ViewBag.responseData = responseData;

            return View(response);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetDetails(int id, bool? isDelete = false)
        {
            var Uri = "MdRequestValidation/" + id;
            var model = await _setupHttp.GetAsync(Uri, id);
            if (isDelete.Value)
                return PartialView("~/Views/ValidationCode/_DeleteValidationCode.cshtml", model);
            else
                return PartialView("~/Views/ValidationCode/_EditValidationCode.cshtml", model);
        }

        [HttpGet]
        public async Task<string> ExistValidationCode(string validationCode)
        {
            bool isExist = false;
            var getValidationCodeList = await _valitionListHttp.GetIEnumerableByValueAsync("MdRequestValidation/sp_GetValidationCodeKeys");
            if (getValidationCodeList.Any())
            {
                isExist = getValidationCodeList.Where(x => Equals(validationCode, x.ValidationCode)).Any();
            }
            return JsonConvert.SerializeObject(isExist);
        }

        [HttpPost]
        public async Task<IActionResult> ValidationCodes(MdRequestValidationViewModel data)
        {
            if (!ModelState.IsValid) return View(data);
            data.CreatedDateTime = DateTime.Now;
            data.ModifiedDateTime = DateTime.Now;
            data.ModifiedBy = new Guid("BF189BE2-D457-4424-A27E-61EA87395439");
            var response = await _setupHttp.PostAsync("MdRequestValidation/post", data);
            if (response.Id == 0)
            {
                TempData[NotificationConstants.MESSAGE_ERROR] = "The validation code has been failed.";
                return RedirectToAction("ValidationCodes");
            }
            TempData[NotificationConstants.MESSAGE] = "The validation Code " + response.ValidationCode + " has been created.";
            TempData[NotificationConstants.RESPONSEDATA] = response.Id;
            return RedirectToAction("ValidationCodes");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateValidationCode(MdRequestValidationViewModel data)
        {
            data.ModifiedDateTime = DateTime.Now;
            await _setupHttp.PostAsync("MdRequestValidation/put", data);
            TempData[NotificationConstants.MESSAGE] = "The validation Code " + data.ValidationCode + " has been updated.";
            TempData[NotificationConstants.RESPONSEDATA] = data.Id;
            return RedirectToAction("ValidationCodes");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteValidationCode(MdRequestValidationViewModel data)
        {
            if (!ModelState.IsValid) return View(data);

            var response = await _setupHttp.PostAsync("MdRequestValidation/delete", data);
            if (response == null)
            {
                TempData[NotificationConstants.MESSAGE_ERROR] = "The validation code has been deleted failed.";
                return RedirectToAction("ValidationCodes");
            }
            TempData[NotificationConstants.MESSAGE] = "The validation Code " + response.ValidationCode + " has been deleted successfully.";
            TempData[NotificationConstants.RESPONSEDATA] = response.Id;
            return RedirectToAction("ValidationCodes");
        }

        [HttpGet]
        public bool ExportValidationCodeList()
        {
            DataTable dt = new DataTable();

            //Add Datacolumn
            DataColumn workCol = dt.Columns.Add("FirstName", typeof(String));

            dt.Columns.Add("LastName", typeof(String));
            dt.Columns.Add("Blog", typeof(String));
            dt.Columns.Add("City", typeof(String));
            dt.Columns.Add("Country", typeof(String));

            //Add in the datarow
            DataRow newRow = dt.NewRow();

            newRow["firstname"] = "Arun";
            newRow["lastname"] = "Prakash";
            newRow["Blog"] = "http://royalarun.blogspot.com/";
            newRow["city"] = "Coimbatore";
            newRow["country"] = "India";

            dt.Rows.Add(newRow);

            //open file
            StreamWriter wr = new StreamWriter(@"D:\\Book1.xls");

            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                }

                wr.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (dt.Rows.Count); i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    //go to next line
                    wr.WriteLine();
                }
                //close file
                wr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}