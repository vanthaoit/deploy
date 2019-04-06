using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace LogixHealth.Eligibility.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiController : Controller
    {
        public ApiController()
        {
        }

        public HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateConcurrencyException exEntity)
            {
                foreach (var eve in exEntity.Entries)
                {
                    Trace.WriteLine($"Entity of type \"{eve.GetType().Name}\" in state \"{eve.State}\" has the following validation error at DB ENTITY VALIDATION EXCEPTION.");
                }

                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (DbUpdateException dbEx)
            {
                Trace.WriteLine($"Entity of type \"{dbEx.InnerException.Message}\"");
                //LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }
    }
}