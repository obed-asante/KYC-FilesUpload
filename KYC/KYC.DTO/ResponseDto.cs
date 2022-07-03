using System;

namespace KYC.DTO
{
    public class ResponseDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
