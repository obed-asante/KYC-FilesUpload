using System;
using System.Collections.Generic;
using System.Text;

namespace KYC.DTO
{
    public class ApplicationDocsResponseDto : ResponseDto
    {
        public List<string> ApplicationDocs { get; set; } = new List<string>();
    }
}
