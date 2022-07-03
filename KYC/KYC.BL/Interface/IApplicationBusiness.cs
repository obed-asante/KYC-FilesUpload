using KYC.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYC.BL.Interface
{
    public interface IApplicationBusiness
    {
        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <param name="docs">list of uploaded files</param>
        /// <returns></returns>
        Task<ResponseDto> BulkUploadFiles(Guid loanRequestId, List<IFormFile> docs);

        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <returns></returns>
        Task<ApplicationDocsResponseDto> GetApplicationDocs(Guid loanRequestId);
    }
}
