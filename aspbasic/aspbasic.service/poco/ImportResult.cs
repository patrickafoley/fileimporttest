using System;
namespace aspbasic.service.poco
{
    public class ImportResult
    {
        public int imported { get; set; }
        public int skipped { get; set; }
        public string type { get; set; }
        public bool isFatalError { get; set; }
        public string errorText { get; set;  }
    }
}
