using System;
namespace aspbasic.service
{
    public class AspBasicException : Exception
    {
        public string fileType { get; set; }
    }
}
