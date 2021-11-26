using System.Collections.Generic;

namespace EnterpriseApp.Core.Responses
{
    public class ErrorApiResponse
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public Errors Errors { get; set; }
    }

    public class Errors
    {
        public IEnumerable<string> Messages { get; set; }
    }
}
