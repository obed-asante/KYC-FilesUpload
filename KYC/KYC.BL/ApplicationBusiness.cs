using KYC.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using KYC.BL.Interface;
using Microsoft.Extensions.Options;

namespace KYC.BL
{
    public class ApplicationBusiness : IApplicationBusiness
    {
        private readonly FilePaths _filePaths;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePaths"></param>
        public ApplicationBusiness(IOptions<FilePaths> filePaths)
        {
            _filePaths = filePaths.Value;
        }

        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <param name="docs">list of uploaded files</param>
        /// <returns></returns>
        public async Task<ResponseDto> BulkUploadFiles(Guid loanRequestId, List<IFormFile> docs)
        {
            var response = new ResponseDto();
            if (loanRequestId == null || loanRequestId == Guid.Empty)
            {
                response.Message = "Invalid loan request";
                return response;
            }

            if (docs == null || docs.Count == 0)
            {
                response.Message = "No files to upload";
                return response;
            }

            if (!Directory.Exists(_filePaths.Uploads))
            {
                response.Message = "Upload path does not exist";
                return response;
            }

            try
            {
                string filePath = string.Empty;
                foreach (var doc in docs)
                {
                    if (doc.Length > 0)
                    {
                        filePath = Path.Combine(_filePaths.Uploads, loanRequestId.ToString() + "_" + doc.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await doc.CopyToAsync(fileStream);
                        }
                    }
                }

                response.Success = true;
                response.Message = $"{docs.Count} files Uploaded";
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Gets a list of files from UI and save to server
        /// </summary>
        /// <param name="loanRequestId">The loan request ID</param>
        /// <returns></returns>
        public async Task<ApplicationDocsResponseDto> GetApplicationDocs(Guid loanRequestId)
        {
            var response = new ApplicationDocsResponseDto();
            if (loanRequestId == null || loanRequestId == Guid.Empty)
            {
                response.Message = "Invalid loan request";
                return response;
            }

            if (!Directory.Exists(_filePaths.Uploads))
            {
                response.Message = "Files path does not exist";
                return response;
            }

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_filePaths.Uploads);
                FileInfo[] fileInfo = directoryInfo.GetFiles("*" + loanRequestId.ToString() + "*.*"); //the filename should contain the loanid

                if (fileInfo != null)
                {
                    foreach (var file in fileInfo)
                    {
                        if (file.Length > 0)
                            response.ApplicationDocs.Add(file.FullName);
                    }
                    response.Success = true;
                    response.Message = fileInfo.Length == 0 ? "No file found" : $"{fileInfo.Length} file(s) returned";
                }
                else
                {
                    response.Message = "No file found";
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
