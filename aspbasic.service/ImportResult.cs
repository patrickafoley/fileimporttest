using System;
namespace aspbasic.service
{
    public class ImportResult
    {
        public int imported { get; set; }
        public int skipped { get; set; }
        public string type { get; set; }
        public bool isError { get; set; }
        public string errorText { get; set;  }
    }
}
