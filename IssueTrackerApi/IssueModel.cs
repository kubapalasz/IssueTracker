using System;

namespace IssueTrackerApi
{
    public class IssueModel
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Number { get; set; }
        public string ProjectName { get; set; }
    }
}
