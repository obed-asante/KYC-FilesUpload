using KYC.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KYC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationBusiness _applicationBusiness;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="applicationBusiness"></param>
        public ApplicationController(IApplicationBusiness applicationBusiness)
        {
            _applicationBusiness = applicationBusiness;
        }

        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <param name="docs">list of uploaded files</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BulkUploadFiles(Guid loanRequestId, List<IFormFile> docs)
        {
            var result = await _applicationBusiness.BulkUploadFiles(loanRequestId, docs);
            return Ok(result);
        }

        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetApplicationDocs(Guid loanRequestId)
        {
            var result = await _applicationBusiness.GetApplicationDocs(loanRequestId);
            return Ok(result);
        }
    }
}
